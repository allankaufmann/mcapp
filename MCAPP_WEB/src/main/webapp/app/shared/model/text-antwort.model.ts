import { IFrage } from 'app/shared/model//frage.model';

export interface ITextAntwort {
    id?: number;
    position?: number;
    wahr?: boolean;
    text?: string;
    frage?: IFrage;
}

export class TextAntwort implements ITextAntwort {
    constructor(public id?: number, public position?: number, public wahr?: boolean, public text?: string, public frage?: IFrage) {
        this.wahr = this.wahr || false;
    }
}
