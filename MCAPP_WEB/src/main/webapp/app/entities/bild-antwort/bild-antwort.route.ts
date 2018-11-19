import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from '@angular/router';
import { UserRouteAccessService } from 'app/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { BildAntwort } from 'app/shared/model/bild-antwort.model';
import { BildAntwortService } from './bild-antwort.service';
import { BildAntwortComponent } from './bild-antwort.component';
import { BildAntwortDetailComponent } from './bild-antwort-detail.component';
import { BildAntwortUpdateComponent } from './bild-antwort-update.component';
import { BildAntwortDeletePopupComponent } from './bild-antwort-delete-dialog.component';
import { IBildAntwort } from 'app/shared/model/bild-antwort.model';
import { TextAntwort } from 'app/shared/model/text-antwort.model';
import { Frage } from 'app/shared/model/frage.model';

@Injectable({ providedIn: 'root' })
export class BildAntwortResolve implements Resolve<IBildAntwort> {
    constructor(private service: BildAntwortService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const id = route.params['id'] ? route.params['id'] : null;
        if (id) {
            return this.service.find(id).pipe(map((bildAntwort: HttpResponse<BildAntwort>) => bildAntwort.body));
        }
        const frageid = route.queryParams['frageid'] ? route.queryParams['frageid'] : null;
        var t = new BildAntwort();
        if (frageid && !isNaN(frageid)) {
            let f = new Frage();
            f.id = Number(frageid);
            t.frage = f;
        }

        return of(t);
    }
}

export const bildAntwortRoute: Routes = [
    {
        path: 'bild-antwort',
        component: BildAntwortComponent,
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.bildAntwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'bild-antwort/:id/view',
        component: BildAntwortDetailComponent,
        resolve: {
            bildAntwort: BildAntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.bildAntwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'bild-antwort/new',
        component: BildAntwortUpdateComponent,
        resolve: {
            bildAntwort: BildAntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.bildAntwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'bild-antwort/:id/edit',
        component: BildAntwortUpdateComponent,
        resolve: {
            bildAntwort: BildAntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.bildAntwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    }
];

export const bildAntwortPopupRoute: Routes = [
    {
        path: 'bild-antwort/:id/delete',
        component: BildAntwortDeletePopupComponent,
        resolve: {
            bildAntwort: BildAntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.bildAntwort.home.title'
        },
        canActivate: [UserRouteAccessService],
        outlet: 'popup'
    }
];
