interface ITokenInfoProps {
    accessToken?: string;
}

export const TokenInfo: React.FunctionComponent<ITokenInfoProps> = ({ accessToken }) => (
    <p style={{ wordWrap: "break-word" }}>{accessToken}</p>
)