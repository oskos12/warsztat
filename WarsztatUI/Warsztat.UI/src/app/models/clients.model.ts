import { User } from "./users.model";

export interface Client{
    id: number,
    name: string,
    surname: string,
    phoneNumber: string,
    city: string,
    post: string,
    address: string,
    image: string,
    active: boolean,
    user: User
}