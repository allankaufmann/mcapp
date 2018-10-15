import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from '@angular/router';
import { UserRouteAccessService } from 'app/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Thema } from 'app/shared/model/thema.model';
import { ThemaService } from './thema.service';
import { ThemaComponent } from './thema.component';
import { ThemaDetailComponent } from './thema-detail.component';
import { ThemaUpdateComponent } from './thema-update.component';
import { ThemaDeletePopupComponent } from './thema-delete-dialog.component';
import { IThema } from 'app/shared/model/thema.model';

@Injectable({ providedIn: 'root' })
export class ThemaResolve implements Resolve<IThema> {
    constructor(private service: ThemaService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const id = route.params['id'] ? route.params['id'] : null;
        if (id) {
            return this.service.find(id).pipe(map((thema: HttpResponse<Thema>) => thema.body));
        }
        return of(new Thema());
    }
}

export const themaRoute: Routes = [
    {
        path: 'thema',
        component: ThemaComponent,
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.thema.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'thema/:id/view',
        component: ThemaDetailComponent,
        resolve: {
            thema: ThemaResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.thema.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'thema/new',
        component: ThemaUpdateComponent,
        resolve: {
            thema: ThemaResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.thema.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'thema/:id/edit',
        component: ThemaUpdateComponent,
        resolve: {
            thema: ThemaResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.thema.home.title'
        },
        canActivate: [UserRouteAccessService]
    }
];

export const themaPopupRoute: Routes = [
    {
        path: 'thema/:id/delete',
        component: ThemaDeletePopupComponent,
        resolve: {
            thema: ThemaResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.thema.home.title'
        },
        canActivate: [UserRouteAccessService],
        outlet: 'popup'
    }
];
