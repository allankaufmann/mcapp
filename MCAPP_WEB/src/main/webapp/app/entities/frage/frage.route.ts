import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from '@angular/router';
import { UserRouteAccessService } from 'app/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Frage } from 'app/shared/model/frage.model';
import { FrageService } from './frage.service';
import { FrageComponent } from './frage.component';
import { FrageDetailComponent } from './frage-detail.component';
import { FrageUpdateComponent } from './frage-update.component';
import { FrageDeletePopupComponent } from './frage-delete-dialog.component';
import { IFrage } from 'app/shared/model/frage.model';

@Injectable({ providedIn: 'root' })
export class FrageResolve implements Resolve<IFrage> {
    constructor(private service: FrageService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const id = route.params['id'] ? route.params['id'] : null;
        if (id) {
            return this.service.find(id).pipe(map((frage: HttpResponse<Frage>) => frage.body));
        }
        return of(new Frage());
    }
}

export const frageRoute: Routes = [
    {
        path: 'frage',
        component: FrageComponent,
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.frage.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'frage/:id/view',
        component: FrageDetailComponent,
        resolve: {
            frage: FrageResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.frage.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'frage/new',
        component: FrageUpdateComponent,
        resolve: {
            frage: FrageResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.frage.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'frage/:id/edit',
        component: FrageUpdateComponent,
        resolve: {
            frage: FrageResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.frage.home.title'
        },
        canActivate: [UserRouteAccessService]
    }
];

export const fragePopupRoute: Routes = [
    {
        path: 'frage/:id/delete',
        component: FrageDeletePopupComponent,
        resolve: {
            frage: FrageResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.frage.home.title'
        },
        canActivate: [UserRouteAccessService],
        outlet: 'popup'
    }
];
