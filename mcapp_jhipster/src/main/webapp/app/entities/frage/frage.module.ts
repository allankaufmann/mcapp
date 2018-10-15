import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { McappWebSharedModule } from 'app/shared';
import {
    FrageComponent,
    FrageDetailComponent,
    FrageUpdateComponent,
    FrageDeletePopupComponent,
    FrageDeleteDialogComponent,
    frageRoute,
    fragePopupRoute
} from './';

const ENTITY_STATES = [...frageRoute, ...fragePopupRoute];

@NgModule({
    imports: [McappWebSharedModule, RouterModule.forChild(ENTITY_STATES)],
    declarations: [FrageComponent, FrageDetailComponent, FrageUpdateComponent, FrageDeleteDialogComponent, FrageDeletePopupComponent],
    entryComponents: [FrageComponent, FrageUpdateComponent, FrageDeleteDialogComponent, FrageDeletePopupComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebFrageModule {}
