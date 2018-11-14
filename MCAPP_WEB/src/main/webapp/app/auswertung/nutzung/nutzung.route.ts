import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from '@angular/router';
import { UserRouteAccessService } from 'app/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Quiz } from 'app/shared/model/quiz.model';
import { NutzungService } from './nutzung.service';
import { NutzungComponent } from './nutzung.component';
import { IQuiz } from 'app/shared/model/quiz.model';
import { Route } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class NutzungResolve implements Resolve<IQuiz> {
    constructor(private service: NutzungService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const id = route.params['id'] ? route.params['id'] : null;
        if (id) {
            return this.service.find(id).pipe(map((quiz: HttpResponse<Quiz>) => quiz.body));
        }
        return of(new Quiz());
    }
}

export const nutzungRoute: Route = {
    path: 'nutzung',
    component: NutzungComponent,
    data: {
        authorities: ['ROLE_USER'],
        pageTitle: 'mcappWebApp.quiz.home.title'
    },
    canActivate: [UserRouteAccessService]
};
