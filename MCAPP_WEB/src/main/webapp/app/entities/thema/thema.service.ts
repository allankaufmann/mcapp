import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SERVER_API_URL } from 'app/app.constants';
import { createRequestOption } from 'app/shared';
import { IThema } from 'app/shared/model/thema.model';

type EntityResponseType = HttpResponse<IThema>;
type EntityArrayResponseType = HttpResponse<IThema[]>;

@Injectable({ providedIn: 'root' })
export class ThemaService {
    private resourceUrl = SERVER_API_URL + 'api/themas';
    private resourceSearchUrl = SERVER_API_URL + 'api/_search/themas';

    constructor(private http: HttpClient) {}

    create(thema: IThema): Observable<EntityResponseType> {
        return this.http.post<IThema>(this.resourceUrl, thema, { observe: 'response' });
    }

    update(thema: IThema): Observable<EntityResponseType> {
        return this.http.put<IThema>(this.resourceUrl, thema, { observe: 'response' });
    }

    find(id: number): Observable<EntityResponseType> {
        return this.http.get<IThema>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    query(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<IThema[]>(this.resourceUrl, { params: options, observe: 'response' });
    }

    delete(id: number): Observable<HttpResponse<any>> {
        return this.http.delete<any>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    search(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<IThema[]>(this.resourceSearchUrl, { params: options, observe: 'response' });
    }
}
