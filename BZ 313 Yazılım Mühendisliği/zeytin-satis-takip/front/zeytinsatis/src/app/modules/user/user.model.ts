
export namespace User {

    export interface RegisterUser{

        fullName: string,
        email: string,
        password: string,
        company: string

    }

    export interface LoginUser{

        email: string,
        password: string

    }

    export interface LoggedUser{

        fullName: string,
        email: string,
        password: string,
        company: string

    }

    

} 