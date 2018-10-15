import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { McappWebSharedModule } from 'app/shared';
import {
    AntwortComponent,
    AntwortDetailComponent,
    AntwortUpdateComponent,
    AntwortDeletePopupComponent,
    AntwortDeleteDialogComponent,
    antwortRoute,
    antwortPopupRoute
} from './';

const ENTITY_STATES = [...antwortRoute, ...antwortPopupRoute];

@NgModule({
    imports: [McappWebSharedModule, RouterModule.forChild(ENTITY_STATES)],
    declarations: [
        AntwortComponent,
        AntwortDetailComponent,
        AntwortUpdateComponent,
        AntwortDeleteDialogComponent,
        AntwortDeletePopupComponent
    ],
    entryComponents: [AntwortComponent, AntwortUpdateComponent, AntwortDeleteDialogComponent, AntwortDeletePopupComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebAntwortModule {}
