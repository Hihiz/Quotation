class Common {
    static readonly Email = "Email";
    static readonly Password = "Password";
    static readonly AccessToken = "AccessToken";
    static readonly RefreshToken  = "RefreshToken"; 
    static readonly Date = "date";
}

class HtmlTypesDeclarations {
    static readonly EmailType = "email";
    static readonly PasswordType = "password";
}

class HtmlNamesDeclarations {
    static readonly ConfirmPasswordName = "PasswordConfirm";
    static readonly Register = "Register";
    static readonly LogIn = "Log In";
    static readonly LogOut = "Log Out";
    static readonly Quotations = "Get quotations";
    static readonly UpdateToken = "Update token";
}

class HtmlTitlesDeclarations {
    static readonly ConfirmPasswordTitle = "PasswordConfirm";
}

class ValidationErrors {
    static readonly DataInvalid = "Data is invalid.";
}

class SuccessMessages {
    static readonly QuotationSuccess = "Quotations data retrieved. Please, check console logs";
}

const Constants = {
    Common,
    HtmlTypesDeclarations,
    HtmlNamesDeclarations,
    HtmlTitlesDeclarations,
    ValidationErrors,
    SuccessMessages
}

export default Constants;