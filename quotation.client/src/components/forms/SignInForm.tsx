import { observer } from 'mobx-react-lite';
import { useState } from "react";

import Constants from '../../common/constants';
import Cookies from 'universal-cookie';

import AccountService from "../../services/AccountService";
import AccountStore from "../../stores/AccountStore";

import { FormGroup } from "../FormGroup"
import { Button } from "../Button"
import { Form } from '../Form';
import { referenceEnhancer } from 'mobx/dist/internal';

interface ISignInFormProps {
    accountStore: AccountStore
}

const SignInForm: React.FunctionComponent<ISignInFormProps> = (props) => {
    const accountStore = props.accountStore;

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    function onChangeEmail(e: React.FormEvent<HTMLInputElement>) {
        setEmail(e.currentTarget.value);
    }

    function onChangePassword(e: React.FormEvent<HTMLInputElement>) {
        setPassword(e.currentTarget.value);
    }

    function isDataValidation() {
        return email.length !== 0 && password.length !== 0
    }

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        if (!isDataValidation()) {
            alert(Constants.ValidationErrors.DataInvalid);
            return;
        }

        AccountService.signIn({
            email: email,
            password: password
        })
            .then((response: any) => {
                const cookies = new Cookies();
                const accessToken = response?.data?.accessToken;
                const refreshToken = response?.data?.refreshToken;

                const accountStore = props.accountStore;
                cookies.set(Constants.Common.AccessToken, accessToken);
                cookies.set(Constants.Common.RefreshToken, refreshToken);
                accountStore.updateAccessToken(accessToken, refreshToken);

                alert(response?.data?.message);
            })
            .catch((e: any) => {
                alert(`${e?.response?.statusText} - please, check console logs`);
                console.log(e?.response);
            });
    }

    return (
        <Form>
            <FormGroup
                type={Constants.HtmlTypesDeclarations.EmailType}
                name={Constants.Common.Email}
                title={Constants.Common.Email}
                onChange={onChangeEmail} />
            <FormGroup
                type={Constants.HtmlTypesDeclarations.PasswordType}
                name={Constants.Common.Password}
                title={Constants.Common.Password}
                onChange={onChangePassword} />
            <br />
            <Button
                name={Constants.HtmlNamesDeclarations.LogIn}
                onClick={onSubmit} />
        </Form>
    )
}

export default observer(SignInForm);