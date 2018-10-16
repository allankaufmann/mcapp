import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { McappWebSharedModule } from 'app/shared';
import {
    BildAntwortComponent,
    BildAntwortDetailComponent,
    BildAntwortUpdateComponent,
    BildAntwortDeletePopupComponent,
    BildAntwortDeleteDialogComponent,
    bildAntwortRoute,
    bildAntwortPopupRoute
} from './';

const ENTITY_STATES = [...bildAntwortRoute, ...bildAntwortPopupRoute];

@NgModule({
    imports: [McappWebSharedModule, RouterModule.forChild(ENTITY_STATES)],
    declarations: [
        BildAntwortComponent,
        BildAntwortDetailComponent,
        BildAntwortUpdateComponent,
        BildAntwortDeleteDialogComponent,
        BildAntwortDeletePopupComponent
    ],
    entryComponents: [BildAntwortComponent, BildAntwortUpdateComponent, BildAntwortDeleteDialogComponent, BildAntwortDeletePopupComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebBildAntwortModule {}
