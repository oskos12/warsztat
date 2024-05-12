export interface Application{
    id: number;
    content: string;
    contact: string;
    name: string;
    surname: string;
    value: string;
    clients_Id: number;
    dictionary_Id: number;
}

export interface ApplicationStatus{
    idApplication: number;
    status: string;
}