import { IFrage } from 'app/shared/model//frage.model';

export interface IThema {
    id?: number;
    themaText?: string;
    frageIDS?: IFrage[];
}

export class Thema implements IThema {
    constructor(public id?: number, public themaText?: string, public frageIDS?: IFrage[]) {}
}
