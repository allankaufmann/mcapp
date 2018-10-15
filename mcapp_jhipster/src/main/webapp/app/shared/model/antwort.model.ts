import { IFrage } from 'app/shared/model//frage.model';

export interface IAntwort {
    id?: number;
    antwortID?: number;
    position?: number;
    wahr?: boolean;
    text?: string;
    bildContentType?: string;
    bild?: any;
    frageIDS?: IFrage[];
}

export class Antwort implements IAntwort {
    constructor(
        public id?: number,
        public antwortID?: number,
        public position?: number,
        public wahr?: boolean,
        public text?: string,
        public bildContentType?: string,
        public bild?: any,
        public frageIDS?: IFrage[]
    ) {
        this.wahr = this.wahr || false;
    }
}
