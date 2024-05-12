import { User } from "./users.model";

export interface Worker{
    id: number,
    name: string,
    surname: string,
    phoneNumber: string,
    city: string,
    post: string,
    address: string,
    active: boolean,
    user: User
}