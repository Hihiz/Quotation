import http from "../http/http-common";
import { ISignUpCredentials, ISignInCredentials, ILogoutCredentials } from "./interfaces/AccountCredentials";

const signUp = (data: ISignUpCredentials) => {
    return http.post<ISignUpCredentials>("/account/register/", data);
}

const signIn = (data: ISignInCredentials) => {
    return http.post<ISignInCredentials>("/account/login/", data);
}

const logout = (data: ILogoutCredentials) => {
    const config = {
        headers: {
            "Authorization": `Bearer ${data.headers.accessToken}`
        }
    }

    return http.post<ILogoutCredentials>("/account/logout", null, config);
}

const AccountService = {
    signUp,
    signIn,
    logout
};

export default AccountService;