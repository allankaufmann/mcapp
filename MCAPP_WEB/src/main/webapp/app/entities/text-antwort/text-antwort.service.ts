import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SERVER_API_URL } from 'app/app.constants';
import { createRequestOption } from 'app/shared';
import { ITextAntwort } from 'app/shared/model/text-antwort.model';

type EntityResponseType = HttpResponse<ITextAntwort>;
type EntityArrayResponseType = HttpResponse<ITextAntwort[]>;

@Injectable({ providedIn: 'root' })
export class TextAntwortService {
    private resourceUrl = SERVER_API_URL + 'api/text-antworts';
    private resourceSearchUrl = SERVER_API_URL + 'api/_search/text-antworts';

    constructor(private http: HttpClient) {}

    create(textAntwort: ITextAntwort): Observable<EntityResponseType> {
        return this.http.post<ITextAntwort>(this.resourceUrl, textAntwort, { observe: 'response' });
    }

    update(textAntwort: ITextAntwort): Observable<EntityResponseType> {
        return this.http.put<ITextAntwort>(this.resourceUrl, textAntwort, { observe: 'response' });
    }

    find(id: number): Observable<EntityResponseType> {
        return this.http.get<ITextAntwort>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    query(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<ITextAntwort[]>(this.resourceUrl, { params: options, observe: 'response' });
    }

    delete(id: number): Observable<HttpResponse<any>> {
        return this.http.delete<any>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    search(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<ITextAntwort[]>(this.resourceSearchUrl, { params: options, observe: 'response' });
    }
}
