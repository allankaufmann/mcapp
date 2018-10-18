import { IFrage } from 'app/shared/model//frage.model';
import { IQuiz } from 'app/shared/model//quiz.model';

export interface IQuizFrage {
    id?: number;
    richtig?: boolean;
    frage?: IFrage;
    quiz?: IQuiz;
}

export class QuizFrage implements IQuizFrage {
    constructor(public id?: number, public richtig?: boolean, public frage?: IFrage, public quiz?: IQuiz) {
        this.richtig = this.richtig || false;
    }
}
