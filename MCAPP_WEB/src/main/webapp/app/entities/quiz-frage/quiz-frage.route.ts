import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from '@angular/router';
import { UserRouteAccessService } from 'app/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { QuizFrage } from 'app/shared/model/quiz-frage.model';
import { QuizFrageService } from './quiz-frage.service';
import { QuizFrageComponent } from './quiz-frage.component';
import { QuizFrageDetailComponent } from './quiz-frage-detail.component';
import { QuizFrageUpdateComponent } from './quiz-frage-update.component';
import { QuizFrageDeletePopupComponent } from './quiz-frage-delete-dialog.component';
import { IQuizFrage } from 'app/shared/model/quiz-frage.model';

@Injectable({ providedIn: 'root' })
export class QuizFrageResolve implements Resolve<IQuizFrage> {
    constructor(private service: QuizFrageService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const id = route.params['id'] ? route.params['id'] : null;
        if (id) {
            return this.service.find(id).pipe(map((quizFrage: HttpResponse<QuizFrage>) => quizFrage.body));
        }
        return of(new QuizFrage());
    }
}

export const quizFrageRoute: Routes = [
    {
        path: 'quiz-frage',
        component: QuizFrageComponent,
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.quizFrage.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'quiz-frage/:id/view',
        component: QuizFrageDetailComponent,
        resolve: {
            quizFrage: QuizFrageResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.quizFrage.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'quiz-frage/new',
        component: QuizFrageUpdateComponent,
        resolve: {
            quizFrage: QuizFrageResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.quizFrage.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'quiz-frage/:id/edit',
        component: QuizFrageUpdateComponent,
        resolve: {
            quizFrage: QuizFrageResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.quizFrage.home.title'
        },
        canActivate: [UserRouteAccessService]
    }
];

export const quizFragePopupRoute: Routes = [
    {
        path: 'quiz-frage/:id/delete',
        component: QuizFrageDeletePopupComponent,
        resolve: {
            quizFrage: QuizFrageResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.quizFrage.home.title'
        },
        canActivate: [UserRouteAccessService],
        outlet: 'popup'
    }
];
