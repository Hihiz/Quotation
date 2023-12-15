export interface ISignUpCredentials {
    email: string;
    password: string;
    passwordConfirm: string;
}

export interface ISignInCredentials {
    email: string;
    password: string;
}

export interface ILogoutCredentials {
    headers: {
        accessToken: string;
    }
}