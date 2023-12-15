import { useState } from "react";

import Constants from "../../common/constants";
import AccountService from "../../services/AccountService";

import { FormGroup } from "../FormGroup"
import { Button } from "../Button"
import { Form } from "../Form";

export const SignUpForm: React.FunctionComponent = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [passwordConfirm, setComfirmPassword] = useState("");

    function onChangeEmail(e: React.FormEvent<HTMLInputElement>) {
        setEmail(e.currentTarget.value);
    }

    function onChangePassword(e: React.FormEvent<HTMLInputElement>) {
        setPassword(e.currentTarget.value);
    }

    function onChangeConfirmPassword(e: React.FormEvent<HTMLInputElement>) {
        setComfirmPassword(e.currentTarget.value);
    }

    function isDataValidation() {
        if (email.length === 0 || password.length === 0 || passwordConfirm.length === 0) {
            return false;
        }

        if (password !== passwordConfirm) {
            return false;
        }

        return true;
    }

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        if (!isDataValidation()) {
            alert(Constants.ValidationErrors.DataInvalid);
            return;
        }

        AccountService.signUp({
            email: email,
            password: password,
            passwordConfirm: passwordConfirm
        })
            .then((response: any) => {
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
            <FormGroup
                type={Constants.HtmlTypesDeclarations.PasswordType}
                name={Constants.HtmlNamesDeclarations.ConfirmPasswordName}
                title={Constants.HtmlTitlesDeclarations.ConfirmPasswordTitle}
                onChange={onChangeConfirmPassword} />
            <br />
            <Button
                name={Constants.HtmlNamesDeclarations.Register}
                onClick={onSubmit} />
        </Form>
    )
}