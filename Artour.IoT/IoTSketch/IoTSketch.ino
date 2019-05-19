#include <Arduino.h>
#include <LiquidCrystal.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <ArduinoJson.h>

#define btnRIGHT  0
#define btnUP     1
#define btnDOWN   2
#define btnLEFT   3
#define btnNONE   4

#define ENTER_ID 0
#define ENTER_CODE 1
#define CHOOSE_TOUR 2
#define TOUR_IN_PROGRESS 3

int system_status = 0;
int lcd_key     = 5;
int adc_key_in  = 0;
int userId = 0;
int confirmation_code = 0;
int tourId = 0;
int currentSight = 0;

ESP8266WiFiMulti WiFiMulti;
LiquidCrystal lcd(D8, D9, D4, D5, D6, D7);
bool requestSent = false;
String token, code, visitId;
DynamicJsonDocument tours(1024);

unsigned long lastTimePrinted = 0;

void set_status(int statusId) {
  system_status = statusId;
  lcd.clear();
  lcd.setCursor(0,0);
  switch(system_status){
    case ENTER_ID:
    {
      confirmation_code = 0;
      userId = 0;
      tourId = 0;
      token = String();
      code = String();
      currentSight = 0;
      visitId = String();
      lcd.print("Enter your ID:");
      print_number(userId);
      break;
    }
    case ENTER_CODE:
    {
      lcd.print("Enter conf code:");
      break;
    }
    case CHOOSE_TOUR:
    {
      lcd.print("Enter tour ID:");
      break;
    }
    case TOUR_IN_PROGRESS:
    {
      sendStartVisitRequest();
      print_title();
      break;
    }
  }
}

int read_LCD_buttons()
{
 adc_key_in = analogRead(0);
 if (adc_key_in > 1500) return btnNONE; 
 if (adc_key_in < 50)   return btnRIGHT;  
 if (adc_key_in < 250)  return btnUP; 
 if (adc_key_in < 500)  return btnDOWN; 
 if (adc_key_in < 700)  return btnLEFT;   
 return btnNONE;
}

void setup() {
  Serial.begin(115200);
  lcd.begin(16, 2);
  set_status(ENTER_ID);
  // Serial.setDebugOutput(true);

  Serial.println();
  Serial.println();
  Serial.println();

  for (uint8_t t = 4; t > 0; t--) {
    Serial.printf("[SETUP] WAIT %d...\n", t);
    Serial.flush();
    delay(1000);
  }

  WiFi.mode(WIFI_STA);
  WiFiMulti.addAP("prolink", "vfhbyf95");
  while (WiFiMulti.run() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to ");
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());
  sendToursRequest();
}

String sendGetRequest(String address) {
    WiFiClient client;
    HTTPClient http;
    if (http.begin(client, address)) {  // HTTP
      String payload = "";
      Serial.print("[HTTP] GET...\n");
      // start connection and send HTTP header
      int httpCode = http.GET();

      // httpCode will be negative on error
      if (httpCode > 0) {
        // HTTP header has been send and Server response header has been handled
        Serial.printf("[HTTP] GET... code: %d\n", httpCode);

        // file found at server
        if (httpCode == HTTP_CODE_OK || httpCode == HTTP_CODE_MOVED_PERMANENTLY) {
          payload = http.getString();
          requestSent = true;
        }
      } else {
        Serial.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
      }

      http.end();
      return payload;
    } else {
      Serial.printf("[HTTP} Unable to connect\n");
    }
}

String sendPostRequest(String address) {
    WiFiClient client;
    HTTPClient http;
    if (http.begin(client, address)) {  // HTTP
      String payload;
      Serial.print("[HTTP] POST...\n");
      http.addHeader("Authorization", "Bearer " + token);
      http.addHeader("Content-Length", "0");
      // start connection and send HTTP header
      int httpCode = http.POST("");

      // httpCode will be negative on error
      if (httpCode > 0) {
        // HTTP header has been send and Server response header has been handled
        Serial.printf("[HTTP] POST... code: %d\n", httpCode);

        // file found at server
        if (httpCode == HTTP_CODE_OK || httpCode == HTTP_CODE_MOVED_PERMANENTLY) {
          payload = http.getString();
          requestSent = true;
        }
      } else {
        Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
      }

      http.end();
      return payload;
    } else {
      Serial.printf("[HTTP} Unable to connect\n");
    }
}

void sendCodeRequest(int userId) {
  String response = sendGetRequest("http://953ac8f2.ngrok.io/api/users/" + String(userId) + "/send-code");
  if (response.length() != 0) {
    DynamicJsonDocument doc(1024);
    deserializeJson(doc, response);
    const char * tokenR = doc["token"];
    const char * codeR = doc["code"];
    token = String(tokenR);
    code = String(codeR);
  }
}

void sendToursRequest() {
  String response = sendGetRequest("http://953ac8f2.ngrok.io/api/tours/light");
  if (response.length() != 0) {
    deserializeJson(tours, response);
  }
}

void sendStartVisitRequest() {
  String response = sendPostRequest("http://953ac8f2.ngrok.io/api/visits/start?tourId=" + String(tourId) + "&userId=" + String(userId));
  visitId = response.substring(1, response.length() - 1);
}

void sendSightSeenRequest(int sightId) {
  String response = sendPostRequest("http://953ac8f2.ngrok.io/api/sight-seens?sightId=" + String(sightId) + "&visitId=" + visitId);
}

void sendFinishVisitRequest() {
  String response = sendPostRequest("http://953ac8f2.ngrok.io/api/visits/" + visitId + "/finish");
}

void print_number(int number) {
  lcd.setCursor(0, 1);
  lcd.print("                ");
  lcd.setCursor(0, 1);
  lcd.print(number);
}

void print_title() {
  lcd.clear();
  const char * title = tours[tourId - 1]["sights"][currentSight]["title"];
  String toPrint(title);
  if (toPrint.length() > 16) {
    String firstRow = toPrint.substring(0, 16);
    String secondRow = toPrint.substring(16, toPrint.length());
    lcd.setCursor(0,0);
    lcd.print(firstRow);
    lcd.setCursor(0,1);
    lcd.print(secondRow);
  } else {
    lcd.setCursor(0,0);
    lcd.print(toPrint);
  }
}

void processKey() {
  if (lcd_key == btnLEFT) {
    set_status(ENTER_ID);
    return;
  }
  switch(system_status){
    case ENTER_ID:
    {
      if (lcd_key == btnUP) userId++;
      if (lcd_key == btnDOWN) userId--;
      print_number(userId);
      if (userId && lcd_key == btnRIGHT) {
        sendCodeRequest(userId);
        set_status(ENTER_CODE);
      }
      break;
    }
    case ENTER_CODE:
    {
      if (lcd_key == btnUP) confirmation_code++;
      if (lcd_key == btnDOWN) confirmation_code--;
      if (lcd_key == btnRIGHT) {
        if (confirmation_code < 100000) confirmation_code *= 10;
        else if (String(confirmation_code).equals(code)) {
          set_status(CHOOSE_TOUR);
          return;
        }
        else confirmation_code = 0;
      }
      print_number(confirmation_code);
      break;
    }
    case CHOOSE_TOUR:
    {
      if (lcd_key == btnUP) tourId++;
      if (lcd_key == btnDOWN) tourId--;
      print_number(tourId);
      if (tourId && lcd_key == btnRIGHT) {
        set_status(TOUR_IN_PROGRESS);
      }
      break;
    }
    case TOUR_IN_PROGRESS:
    {
      if (lcd_key == btnRIGHT) {
        int sightId = tours[tourId - 1]["sights"][currentSight++]["sightId"];
        sendSightSeenRequest(sightId);
        if (currentSight < tours[tourId - 1]["sights"].size()) {
          print_title();
        } else {
          sendFinishVisitRequest();
          set_status(ENTER_ID);
        }
      }
      break;
    }
  }
}

void loop() {
  lcd_key = read_LCD_buttons();
  if (lcd_key != btnNONE) {
    processKey();
  }
  delay(120);
}
