import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  currLang: string;

  constructor(private translate: TranslateService) {
    this.getLanguage();
  }

  getLanguage = () => {
    let lang = localStorage.getItem('smartlock_lang');
    if (lang) {
      this.currLang = lang;
    } else {
      this.currLang = 'en';
    }
    this.translate.use(this.currLang);
  }

  setLanguage = (lang: string) => {
    this.currLang = lang;
    localStorage.setItem('smartlock_lang', lang);
    this.translate.use(this.currLang);
  }

  getLocked = () => {
    switch (this.currLang) {
      case "en" : 
        return "Locked";
      case "ua" :
        return "Зачинено";
    }
  }

  getOpened = () => {
    switch (this.currLang) {
      case "en" : 
        return "Opened";
      case "ua" :
        return "Відчинено";
    }
  }

  getNotification = () => {
    switch (this.currLang) {
      case "en" : 
        return "Notification";
      case "ua" :
        return "Повiдомлення";
    }
  }

  getLockWithId = () => {
    switch (this.currLang) {
      case "en" : 
        return "Lock with id ";
      case "ua" :
        return "Замок з номером ";
    }
  }

  getHasBeen = () => {
    switch (this.currLang) {
      case "en" : 
        return " has been ";
      case "ua" :
        return " ";
    }
  }

  getFailed = () => {
    switch (this.currLang) {
      case "en" : 
        return "Lock addition failed";
      case "ua" :
        return "Не вдалося додати замок";
    }
  }

  getError = () => {
    switch (this.currLang) {
      case "en" : 
        return "Error";
      case "ua" :
        return "Помилка";
    }
  }

  getHello = () => {
    switch (this.currLang) {
      case "en" : 
        return "Hello, ";
      case "ua" :
        return "Привiт, ";
    }
  }

  getGreetings = () => {
    switch (this.currLang) {
      case "en" : 
        return "Greetings";
      case "ua" :
        return "Привiтання";
    }
  }
}
