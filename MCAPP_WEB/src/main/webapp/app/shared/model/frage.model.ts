import { IThema } from 'app/shared/model//thema.model';
import { ITextAntwort } from 'app/shared/model//text-antwort.model';
import { IBildAntwort } from 'app/shared/model//bild-antwort.model';
import { IQuizFrage } from 'app/shared/model//quiz-frage.model';

export const enum Fragetyp {
    TEXT = 'TEXT',
    BILD = 'BILD'
}

export interface IFrage {
    id?: number;
    frageText?: string;
    frageTyp?: Fragetyp;
    thema?: IThema;
    textAntwortIDS?: ITextAntwort[];
    bildAntwortIDS?: IBildAntwort[];
    quizFrageIDS?: IQuizFrage[];
}

export class Frage implements IFrage {
    constructor(
        public id?: number,
        public frageText?: string,
        public frageTyp?: Fragetyp,
        public thema?: IThema,
        public textAntwortIDS?: ITextAntwort[],
        public bildAntwortIDS?: IBildAntwort[],
        public quizFrageIDS?: IQuizFrage[]
    ) {}
}
