import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { McappWebSharedModule } from 'app/shared';
import {
    QuizFrageComponent,
    QuizFrageDetailComponent,
    QuizFrageUpdateComponent,
    QuizFrageDeletePopupComponent,
    QuizFrageDeleteDialogComponent,
    quizFrageRoute,
    quizFragePopupRoute
} from './';

const ENTITY_STATES = [...quizFrageRoute, ...quizFragePopupRoute];

@NgModule({
    imports: [McappWebSharedModule, RouterModule.forChild(ENTITY_STATES)],
    declarations: [
        QuizFrageComponent,
        QuizFrageDetailComponent,
        QuizFrageUpdateComponent,
        QuizFrageDeleteDialogComponent,
        QuizFrageDeletePopupComponent
    ],
    entryComponents: [QuizFrageComponent, QuizFrageUpdateComponent, QuizFrageDeleteDialogComponent, QuizFrageDeletePopupComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebQuizFrageModule {}
