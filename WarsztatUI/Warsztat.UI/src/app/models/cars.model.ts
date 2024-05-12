import { Client } from "./clients.model";

export interface Car{
    id: number;
    brand: string;
    model: string;
    registration: string;
    productionYear: string;
    capacity : number;
    active: boolean;
    owner: Client;
    dictionaryType_Id: string;
    dictionaryEngine_Id: string;
    dictionaryStatus_Id: string;
}