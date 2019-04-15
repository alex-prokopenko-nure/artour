import { HttpRequest, HttpInterceptor, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {

    const authToken = localStorage.getItem("artour_token");

    if (authToken) {
      const cloned = req.clone({
        headers: req.headers.set("Authorization",
          "Bearer " + authToken)
      });
      return next.handle(cloned);
    } else {
      return next.handle(req);
    }
  }
}
