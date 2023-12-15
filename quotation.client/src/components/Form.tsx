interface IFormProps {
    children?: React.ReactNode;
}

export const Form: React.FunctionComponent<IFormProps> = ({ children }) => (
    <form>{children}</form>
)