import http from "../http/http-common";
import { IQuotationCredentials } from "./interfaces/QuotationCredentials";

const get = (data: IQuotationCredentials) => {
    const config = {
        headers: {
            "Authorization": `Bearer ${data.headers.accessToken}`
        }
    }

    return http.get<IQuotationCredentials>(`/quotation/${data.date}`, config);
}

const QuotationService = {
    get
};

export default QuotationService;