import { Moment } from 'moment';
import { IQuizFrage } from 'app/shared/model//quiz-frage.model';

export interface IQuiz {
    id?: number;
    datum?: Moment;
    quizFrageIDS?: IQuizFrage[];
}

export class Quiz implements IQuiz {
    constructor(public id?: number, public datum?: Moment, public quizFrageIDS?: IQuizFrage[]) {}
}
