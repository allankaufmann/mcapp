import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SERVER_API_URL } from 'app/app.constants';
import { createRequestOption } from 'app/shared';
import { IQuizFrage } from 'app/shared/model/quiz-frage.model';

type EntityResponseType = HttpResponse<IQuizFrage>;
type EntityArrayResponseType = HttpResponse<IQuizFrage[]>;

@Injectable({ providedIn: 'root' })
export class QuizFrageService {
    private resourceUrl = SERVER_API_URL + 'api/quiz-frages';
    private resourceSearchUrl = SERVER_API_URL + 'api/_search/quiz-frages';

    constructor(private http: HttpClient) {}

    create(quizFrage: IQuizFrage): Observable<EntityResponseType> {
        return this.http.post<IQuizFrage>(this.resourceUrl, quizFrage, { observe: 'response' });
    }

    update(quizFrage: IQuizFrage): Observable<EntityResponseType> {
        return this.http.put<IQuizFrage>(this.resourceUrl, quizFrage, { observe: 'response' });
    }

    find(id: number): Observable<EntityResponseType> {
        return this.http.get<IQuizFrage>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    query(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<IQuizFrage[]>(this.resourceUrl, { params: options, observe: 'response' });
    }

    delete(id: number): Observable<HttpResponse<any>> {
        return this.http.delete<any>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    search(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http.get<IQuizFrage[]>(this.resourceSearchUrl, { params: options, observe: 'response' });
    }
}
