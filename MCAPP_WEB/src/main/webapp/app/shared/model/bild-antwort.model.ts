import { IFrage } from 'app/shared/model//frage.model';

export interface IBildAntwort {
    id?: number;
    position?: number;
    wahr?: boolean;
    bildContentType?: string;
    bild?: any;
    frage?: IFrage;
}

export class BildAntwort implements IBildAntwort {
    constructor(
        public id?: number,
        public position?: number,
        public wahr?: boolean,
        public bildContentType?: string,
        public bild?: any,
        public frage?: IFrage
    ) {
        this.wahr = this.wahr || false;
    }
}
