import { IFrage } from 'app/shared/model//frage.model';

export interface IThema {
    id?: number;
    themaText?: string;
    frage?: IFrage;
}

export class Thema implements IThema {
    constructor(public id?: number, public themaText?: string, public frage?: IFrage) {}
}
