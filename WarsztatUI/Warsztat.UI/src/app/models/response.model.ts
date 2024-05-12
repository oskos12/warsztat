import { Result } from '../models/result.model';
export interface Response{
        version: string;
        statusCode: number;
        message: string;
        isError: string;
        responseException: string;
        result: Result;
}