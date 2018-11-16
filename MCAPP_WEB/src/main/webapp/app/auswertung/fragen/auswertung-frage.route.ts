import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from '@angular/router';
import { UserRouteAccessService } from 'app/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Quiz } from 'app/shared/model/quiz.model';
import { AuswertungFrageService } from './auswertung-frage.service';
import { AuswertungFrageComponent } from './auswertung-frage.component';
import { IQuizFrage, QuizFrage } from 'app/shared/model/quiz-frage.model';
import { Route } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class NutzungResolve implements Resolve<IQuizFrage> {
    constructor(private service: AuswertungFrageService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const id = route.params['id'] ? route.params['id'] : null;
        if (id) {
            return this.service.find(id).pipe(map((quiz: HttpResponse<Quiz>) => quiz.body));
        }
        return of(new QuizFrage());
    }
}

export const auswertungfrageRoute: Route = {
    path: 'auswertung-frage',
    component: AuswertungFrageComponent,
    data: {
        authorities: ['ROLE_USER'],
        pageTitle: 'mcappWebApp.quiz.home.title'
    },
    canActivate: [UserRouteAccessService]
};
