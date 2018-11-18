import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Routes } from '@angular/router';
import { UserRouteAccessService } from 'app/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { TextAntwort } from 'app/shared/model/text-antwort.model';
import { Frage, Fragetyp } from 'app/shared/model/frage.model';
import { TextAntwortService } from './text-antwort.service';
import { TextAntwortComponent } from './text-antwort.component';
import { TextAntwortDetailComponent } from './text-antwort-detail.component';
import { TextAntwortUpdateComponent } from './text-antwort-update.component';
import { TextAntwortDeletePopupComponent } from './text-antwort-delete-dialog.component';
import { ITextAntwort } from 'app/shared/model/text-antwort.model';
import { FrageService } from 'app/entities/frage';

@Injectable({ providedIn: 'root' })
export class TextAntwortResolve implements Resolve<ITextAntwort> {
    constructor(private service: TextAntwortService, private frageservice: FrageService) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const id = route.params['id'] ? route.params['id'] : null;
        if (id) {
            return this.service.find(id).pipe(map((textAntwort: HttpResponse<TextAntwort>) => textAntwort.body));
        }

        /*
         * Wenn für eine bestimmte Frage eine Textantwort erstellt werden soll, dann sollte Frage nicht explizit gesetzt werden
         * müssen. Aus diesem Grund wird die frageID als queryParameter übergeben.
         * Da an dieser Stelle ein neues TextAntwort-Objekt erzeugt wird, wird ein Frageobjekt mit der passendne ID
         * in das Textantwort-Objekt gesetzt. Dabei ist die ID der Frage ausreichend.
         */

        const frageid = route.queryParams['frageid'] ? route.queryParams['frageid'] : null;
        const t = new TextAntwort();

        // Prüfen, ob frageID übergeben wurde und numerisch ist
        if (frageid && !isNaN(frageid)) {
            let f = new Frage();
            f.id = Number(frageid);
            t.frage = f;
        }
        return of(t);
    }
}

export const textAntwortRoute: Routes = [
    {
        path: 'text-antwort',
        component: TextAntwortComponent,
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.textAntwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'text-antwort/:id/view',
        component: TextAntwortDetailComponent,
        resolve: {
            textAntwort: TextAntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.textAntwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'text-antwort/new',
        component: TextAntwortUpdateComponent,
        resolve: {
            textAntwort: TextAntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.textAntwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    },
    {
        path: 'text-antwort/:id/edit',
        component: TextAntwortUpdateComponent,
        resolve: {
            textAntwort: TextAntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.textAntwort.home.title'
        },
        canActivate: [UserRouteAccessService]
    }
];

export const textAntwortPopupRoute: Routes = [
    {
        path: 'text-antwort/:id/delete',
        component: TextAntwortDeletePopupComponent,
        resolve: {
            textAntwort: TextAntwortResolve
        },
        data: {
            authorities: ['ROLE_USER'],
            pageTitle: 'mcappWebApp.textAntwort.home.title'
        },
        canActivate: [UserRouteAccessService],
        outlet: 'popup'
    }
];
