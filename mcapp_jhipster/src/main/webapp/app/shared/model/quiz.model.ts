import { Moment } from 'moment';
import { IQuizFrage } from 'app/shared/model//quiz-frage.model';

export interface IQuiz {
    id?: number;
    quizID?: number;
    datum?: Moment;
    quizFrage?: IQuizFrage;
}

export class Quiz implements IQuiz {
    constructor(public id?: number, public quizID?: number, public datum?: Moment, public quizFrage?: IQuizFrage) {}
}
