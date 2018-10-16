import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SERVER_API_URL } from 'app/app.constants';
import { createRequestOption } from 'app/shared';
import { IBildAntwort } from 'app/shared/model/bild-antwort.model';

type EntityResponseType = HttpResponse<IBildAntwort>;
type EntityArrayResponseType = HttpResponse<IBildAntwort[]>;

@Injectable({ providedIn: 'root' })
export class BildAntwortService {
    private resourceUrl = SERVER_API_URL + 'api/bild-antworts';
    private resourceSearchUrl = SERVER_API_URL + 'api/_search/bild-antworts';

    constructor(private http: HttpClient) {}

    create(bildAntwort: IBildAntwort): Observable<EntityResponseType> {
        return this.http.post<IBildAntwort>(this.resourceUrl, bildAntwort, { observe: 'response' });
    }

    update(bildAntwort: IBildAntwort): Observable<EntityResponseType> {
        return this.http.put<IBildAntwort>(this.resourceUrl, bildAntwort, { observe: 'response' });
    }

    find(id: number): Observable<EntityResponseType> {
        return this.http.get<IBildAntwort>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    query(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<IBildAntwort[]>(this.resourceUrl, { params: options, observe: 'response' });
    }

    delete(id: number): Observable<HttpResponse<any>> {
        return this.http.delete<any>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    search(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<IBildAntwort[]>(this.resourceSearchUrl, { params: options, observe: 'response' });
    }
}
