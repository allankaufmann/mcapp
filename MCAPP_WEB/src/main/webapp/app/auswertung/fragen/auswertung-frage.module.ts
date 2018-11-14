import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { McappWebSharedModule } from 'app/shared';
import { auswertungfrageRoute, AuswertungFrageComponent } from 'app/auswertung/fragen';

@NgModule({
    imports: [McappWebSharedModule, RouterModule.forChild([auswertungfrageRoute])],
    declarations: [AuswertungFrageComponent],
    entryComponents: [AuswertungFrageComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class McappWebAuswertungFragenModule {}
