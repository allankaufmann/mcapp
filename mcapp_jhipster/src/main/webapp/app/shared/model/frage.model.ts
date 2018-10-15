import { IThema } from 'app/shared/model//thema.model';
import { IAntwort } from 'app/shared/model//antwort.model';
import { IQuizFrage } from 'app/shared/model//quiz-frage.model';

export const enum Fragetyp {
    TEXT = 'TEXT',
    BILD = 'BILD'
}

export interface IFrage {
    id?: number;
    frageID?: number;
    frageTyp?: Fragetyp;
    themaID?: number;
    themaIDS?: IThema[];
    antwort?: IAntwort;
    quizFrage?: IQuizFrage;
}

export class Frage implements IFrage {
    constructor(
        public id?: number,
        public frageID?: number,
        public frageTyp?: Fragetyp,
        public themaID?: number,
        public themaIDS?: IThema[],
        public antwort?: IAntwort,
        public quizFrage?: IQuizFrage
    ) {}
}
