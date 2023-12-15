interface ILabelProps {
    title: string;
}

export const Label: React.FunctionComponent<ILabelProps> = ({ title }) => (
    <label>{title}</label>
);