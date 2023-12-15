import { Input } from "./Input";
import { Label } from "./Label";

interface IFormGroupProps {
    type: string;
    name?: string;
    title: string;
    onChange: (e: React.FormEvent<HTMLInputElement>) => void;
}

export const FormGroup: React.FunctionComponent<IFormGroupProps> = ({ type, name, title, onChange }) => (
    <div>
        <Label title={title} />
        <br />
        <Input type={type} name={name} onChange={onChange} />
    </div>
);