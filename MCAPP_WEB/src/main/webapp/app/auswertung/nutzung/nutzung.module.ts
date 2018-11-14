import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { McappWebSharedModule } from 'app/shared';
import { nutzungRoute, NutzungComponent } from 'app/auswertung/nutzung';

@NgModule({
    imports: [McappWebSharedModule, RouterModule.forChild([nutzungRoute])],
    declarations: [NutzungComponent],
    entryComponents: [NutzungComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebNutzungModule {}
