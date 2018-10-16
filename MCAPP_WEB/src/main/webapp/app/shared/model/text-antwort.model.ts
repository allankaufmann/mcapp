import { IFrage } from 'app/shared/model//frage.model';

export interface ITextAntwort {
    id?: number;
    position?: number;
    wahr?: boolean;
    text?: string;
    frageIDS?: IFrage[];
}

export class TextAntwort implements ITextAntwort {
    constructor(public id?: number, public position?: number, public wahr?: boolean, public text?: string, public frageIDS?: IFrage[]) {
        this.wahr = this.wahr || false;
    }
}
