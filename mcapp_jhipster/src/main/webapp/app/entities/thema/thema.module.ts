import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { McappWebSharedModule } from 'app/shared';
import {
    ThemaComponent,
    ThemaDetailComponent,
    ThemaUpdateComponent,
    ThemaDeletePopupComponent,
    ThemaDeleteDialogComponent,
    themaRoute,
    themaPopupRoute
} from './';

const ENTITY_STATES = [...themaRoute, ...themaPopupRoute];

@NgModule({
    imports: [McappWebSharedModule, RouterModule.forChild(ENTITY_STATES)],
    declarations: [ThemaComponent, ThemaDetailComponent, ThemaUpdateComponent, ThemaDeleteDialogComponent, ThemaDeletePopupComponent],
    entryComponents: [ThemaComponent, ThemaUpdateComponent, ThemaDeleteDialogComponent, ThemaDeletePopupComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebThemaModule {}
