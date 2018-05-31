namespace DrawGraph.Parser
{
    public class Token
    {
        public string Code;
        public TokenType Type;

        public Token(string code, TokenType type)
        {
            Code = code;
            Type = type;
        }
    }

    public enum TokenType
    {
        Numeric,
        LBracket,
        RBracket,
        Operator,
        Identifier
    }
}