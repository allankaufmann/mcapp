import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { McappWebSharedModule } from 'app/shared';
import {
    TextAntwortComponent,
    TextAntwortDetailComponent,
    TextAntwortUpdateComponent,
    TextAntwortDeletePopupComponent,
    TextAntwortDeleteDialogComponent,
    textAntwortRoute,
    textAntwortPopupRoute
} from './';

const ENTITY_STATES = [...textAntwortRoute, ...textAntwortPopupRoute];

@NgModule({
    imports: [McappWebSharedModule, RouterModule.forChild(ENTITY_STATES)],
    declarations: [
        TextAntwortComponent,
        TextAntwortDetailComponent,
        TextAntwortUpdateComponent,
        TextAntwortDeleteDialogComponent,
        TextAntwortDeletePopupComponent
    ],
    entryComponents: [TextAntwortComponent, TextAntwortUpdateComponent, TextAntwortDeleteDialogComponent, TextAntwortDeletePopupComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebTextAntwortModule {}
