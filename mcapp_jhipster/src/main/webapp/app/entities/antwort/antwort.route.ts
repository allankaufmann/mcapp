import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from '@angular/router';
import { UserRouteAccessService } from 'app/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Antwort } from 'app/shared/model/antwort.model';
import { AntwortService } from './antwort.service';
import { AntwortComponent } from './antwort.component';
import { AntwortDetailComponent } from './antwort-detail.component';
import { AntwortUpdateComponent } from './antwort-update.component';
import { AntwortDeletePopupComponent } from './antwort-delete-dialog.component';
import { IAntwort } from 'app/shared/model/antwort.model';

@Injectable({ providedIn: 'root' })
export class AntwortResolve implements Resolve<IAntwort> {
    constructor(private service: AntwortService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const id = route.params['id'] ? route.params['id'] : null;
        if (id) {
            return this.service.find(id).pipe(map((antwort: HttpResponse<Antwort>) => antwort.body));
        }
        return of(new Antwort());
    }
}

export const antwortRoute: Routes = [
    {
        path: 'antwort',
        component: AntwortComponent,
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.antwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'antwort/:id/view',
        component: AntwortDetailComponent,
        resolve: {
            antwort: AntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.antwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'antwort/new',
        component: AntwortUpdateComponent,
        resolve: {
            antwort: AntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.antwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'antwort/:id/edit',
        component: AntwortUpdateComponent,
        resolve: {
            antwort: AntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.antwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    }
];

export const antwortPopupRoute: Routes = [
    {
        path: 'antwort/:id/delete',
        component: AntwortDeletePopupComponent,
        resolve: {
            antwort: AntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.antwort.home.title'
        },
        canActivate: [UserRouteAccessService],
        outlet: 'popup'
    }
];
