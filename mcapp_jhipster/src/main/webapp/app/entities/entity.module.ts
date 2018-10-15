import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { McappWebThemaModule } from './thema/thema.module';
import { McappWebFrageModule } from './frage/frage.module';
import { McappWebAntwortModule } from './antwort/antwort.module';
import { McappWebQuizModule } from './quiz/quiz.module';
import { McappWebQuizFrageModule } from './quiz-frage/quiz-frage.module';
/* jhipster-needle-add-entity-module-import - JHipster will add entity modules imports here */

@NgModule({
    // prettier-ignore
    imports: [
        McappWebThemaModule,
        McappWebFrageModule,
        McappWebAntwortModule,
        McappWebQuizModule,
        McappWebQuizFrageModule,
        /* jhipster-needle-add-entity-module - JHipster will add entity modules here */
    ],
    declarations: [],
    entryComponents: [],
    providers: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebEntityModule {}
