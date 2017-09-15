namespace AnalizadorLexico
{
    public class StringLiteral : Token
    {
        public string Content { get; set; }

        public StringLiteral(string content) : base(TokenType.String_Literal)
        {

        }
    }
}
