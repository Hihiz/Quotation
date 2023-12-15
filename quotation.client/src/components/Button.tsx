interface IButtonProps {
    name: string;
    disabled?: boolean;
    onClick: (e: any) => void;
}

export const Button: React.FunctionComponent<IButtonProps> = ({ name, disabled, onClick }) => (
    <button disabled={disabled} onClick={onClick}>{name}</button>
)