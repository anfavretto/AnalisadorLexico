namespace AnalizadorLexico
{
    public abstract class Token
    {
        public TokenType Type { get; set; }

        public Token(TokenType type)
        {
            this.Type = type;
        }
    }
}
