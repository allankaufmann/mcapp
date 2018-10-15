import { IQuiz } from 'app/shared/model//quiz.model';
import { IFrage } from 'app/shared/model//frage.model';

export interface IQuizFrage {
    id?: number;
    richtig?: boolean;
    quizIDS?: IQuiz[];
    frageIDS?: IFrage[];
}

export class QuizFrage implements IQuizFrage {
    constructor(public id?: number, public richtig?: boolean, public quizIDS?: IQuiz[], public frageIDS?: IFrage[]) {
        this.richtig = this.richtig || false;
    }
}
