interface IInputProps {
    type: string;
    name?: string;
    disabled?: boolean;
    onChange: (e: React.FormEvent<HTMLInputElement>) => void;
}

export const Input: React.FunctionComponent<IInputProps> = ({ type, name, disabled, onChange }) => (
    <input type={type} name={name} disabled={disabled} onChange={onChange} />
);