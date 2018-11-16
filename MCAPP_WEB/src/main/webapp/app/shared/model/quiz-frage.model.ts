import { IFrage } from 'app/shared/model//frage.model';
import { IQuiz } from 'app/shared/model//quiz.model';

export interface IQuizFrage {
    id?: number;
    richtig?: boolean;
    frage?: IFrage;
    quiz?: IQuiz;
    anzGesamt?: number;
    anzRichtig?: number;
    anzFalsch?: number;
}

export class QuizFrage implements IQuizFrage {
    constructor(public id?: number, public richtig?: boolean, public frage?: IFrage, public quiz?: IQuiz, public  anzGesamt?: number, public anzRichtig?:number, public anzFalsch?:number) {
        this.richtig = this.richtig || false;
        this.anzGesamt=anzGesamt;
        this.anzFalsch=anzFalsch;
        this.anzRichtig=anzRichtig;
    }
}
