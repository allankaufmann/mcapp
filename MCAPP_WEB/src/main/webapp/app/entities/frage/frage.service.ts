import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SERVER_API_URL } from 'app/app.constants';
import { createRequestOption } from 'app/shared';
import { IFrage } from 'app/shared/model/frage.model';

type EntityResponseType = HttpResponse<IFrage>;
type EntityArrayResponseType = HttpResponse<IFrage[]>;

@Injectable({ providedIn: 'root' })
export class FrageService {
    private resourceUrl = SERVER_API_URL + 'api/frages';
    private resourceSearchUrl = SERVER_API_URL + 'api/_search/frages';

    constructor(private http: HttpClient) {}

    create(frage: IFrage): Observable<EntityResponseType> {
        return this.http.post<IFrage>(this.resourceUrl, frage, { observe: 'response' });
    }

    update(frage: IFrage): Observable<EntityResponseType> {
        return this.http.put<IFrage>(this.resourceUrl, frage, { observe: 'response' });
    }

    find(id: number): Observable<EntityResponseType> {
        return this.http.get<IFrage>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    query(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<IFrage[]>(this.resourceUrl, { params: options, observe: 'response' });
    }

    delete(id: number): Observable<HttpResponse<any>> {
        return this.http.delete<any>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    search(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<IFrage[]>(this.resourceSearchUrl, { params: options, observe: 'response' });
    }
}
