namespace AnalizadorLexico
{
    public class Semicolon : Token
    {
        public Semicolon() : base(TokenType.Semicolon)
        {

        }
        public override string GetValue()
        {
            return ";";
        }
    }
}
