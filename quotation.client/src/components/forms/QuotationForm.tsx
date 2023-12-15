import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

import Constants from "../../common/constants";
import Cookies from "universal-cookie";

import QuotationService from "../../services/QuotationService";
import AccountStore from "../../stores/AccountStore";

import { Input } from "../Input";
import { Button } from "../Button"
import { Form } from "../Form";

interface IQuotationFormProps {
    accountStore: AccountStore
}

const QuotationForm: React.FunctionComponent<IQuotationFormProps> = (props) => {
    const cookies = new Cookies();
    const accountStore = props.accountStore;
    const accessToken = accountStore.data.accessToken || cookies.get(Constants.Common.AccessToken);

    const [date, setDate] = useState("");
    const [isDisabled, setIsDisabled] = useState(false);

    useEffect(() => {
        setIsDisabled(!!accessToken);
    }, [accountStore.data.accessToken])

    function onDateChange(e: React.FormEvent<HTMLInputElement>) {
        setDate(e.currentTarget.value);
    }

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        QuotationService.get({
            date: date,
            headers: {
                accessToken: accountStore.data.accessToken || cookies.get(Constants.Common.AccessToken)
            }
        })
            .then((response: any) => {
                alert(Constants.SuccessMessages.QuotationSuccess);
                console.log(response);
            })
            .catch((e: any) => {
                alert(`${e?.response?.statusText} - please, check console logs`);
                console.log(e?.response);
            });
    }

    return (
        <Form>
            <Input
                type={Constants.Common.Date}
                name={Constants.Common.Date}
                disabled={!isDisabled}
                onChange={onDateChange} />
            <br />
            <Button
                name={Constants.HtmlNamesDeclarations.Quotations}
                disabled={!isDisabled}
                onClick={onSubmit} />
        </Form>
    )
}

export default observer(QuotationForm);