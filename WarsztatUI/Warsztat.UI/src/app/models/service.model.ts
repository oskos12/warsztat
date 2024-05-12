import { Worker } from "./workers.model";

export interface Service{
    id: number;
    registration: string;
    count: number;
    hours: number;
    dateAdd: string;
    dateFinish: string;
    costSum: number;
    dictValue: string;
    dictDescription: string;
    description: string;
    name: string;
    surname: string;
    cars_Id: number;
    type_Id: number;
    workers_Id: number;
    clients_Id: number;
}

export interface WorkerService{
    id: number;
    workers: any;
}

export interface ServicesWithWorkers{
    service: Service;
    workers: Worker[];
}