import http from "../http/http-common";
import { ITokenCredentials } from "./interfaces/TokenCredentials";

const updateToken = (data: ITokenCredentials) => {
    const config = {
        headers: {
            "Authorization": `Bearer ${data.headers.accessToken}`
        }
    }
    
    return http.post<ITokenCredentials>("/account/refresh-token" ,null,  config);
}

const refrToken = (data: ITokenCredentials) => {
    const config = {
headers: {
    "Authorization": `Bearer ${data.headers.accessToken}`
}
    }
    return http.post<ITokenCredentials>("/account/refresh-token", data, config);
}

const TokenService = {
    refrToken

};

export default TokenService;