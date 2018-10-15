import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import * as moment from 'moment';
import { DATE_FORMAT } from 'app/shared/constants/input.constants';
import { map } from 'rxjs/operators';

import { SERVER_API_URL } from 'app/app.constants';
import { createRequestOption } from 'app/shared';
import { IQuiz } from 'app/shared/model/quiz.model';

type EntityResponseType = HttpResponse<IQuiz>;
type EntityArrayResponseType = HttpResponse<IQuiz[]>;

@Injectable({ providedIn: 'root' })
export class QuizService {
    private resourceUrl = SERVER_API_URL + 'api/quizzes';
    private resourceSearchUrl = SERVER_API_URL + 'api/_search/quizzes';

    constructor(private http: HttpClient) {}

    create(quiz: IQuiz): Observable<EntityResponseType> {
        const copy = this.convertDateFromClient(quiz);
        return this.http
            .post<IQuiz>(this.resourceUrl, copy, { observe: 'response' })
            .pipe(map((res: EntityResponseType) => this.convertDateFromServer(res)));
    }

    update(quiz: IQuiz): Observable<EntityResponseType> {
        const copy = this.convertDateFromClient(quiz);
        return this.http
            .put<IQuiz>(this.resourceUrl, copy, { observe: 'response' })
            .pipe(map((res: EntityResponseType) => this.convertDateFromServer(res)));
    }

    find(id: number): Observable<EntityResponseType> {
        return this.http
            .get<IQuiz>(`${this.resourceUrl}/${id}`, { observe: 'response' })
            .pipe(map((res: EntityResponseType) => this.convertDateFromServer(res)));
    }

    query(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http
            .get<IQuiz[]>(this.resourceUrl, { params: options, observe: 'response' })
            .pipe(map((res: EntityArrayResponseType) => this.convertDateArrayFromServer(res)));
    }

    delete(id: number): Observable<HttpResponse<any>> {
        return this.http.delete<any>(`${this.resourceUrl}/${id}`, { observe: 'response' });
    }

    search(req?: any): Observable<EntityArrayResponseType> {
        const options = createRequestOption(req);
        return this.http
            .get<IQuiz[]>(this.resourceSearchUrl, { params: options, observe: 'response' })
            .pipe(map((res: EntityArrayResponseType) => this.convertDateArrayFromServer(res)));
    }

    private convertDateFromClient(quiz: IQuiz): IQuiz {
        const copy: IQuiz = Object.assign({}, quiz, {
            datum: quiz.datum != null && quiz.datum.isValid() ? quiz.datum.format(DATE_FORMAT) : null
        });
        return copy;
    }

    private convertDateFromServer(res: EntityResponseType): EntityResponseType {
        res.body.datum = res.body.datum != null ? moment(res.body.datum) : null;
        return res;
    }

    private convertDateArrayFromServer(res: EntityArrayResponseType): EntityArrayResponseType {
        res.body.forEach((quiz: IQuiz) => {
            quiz.datum = quiz.datum != null ? moment(quiz.datum) : null;
        });
        return res;
    }
}
