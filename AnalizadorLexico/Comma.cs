namespace AnalizadorLexico
{
    public class Comma : Token
    {
        public Comma() : base(TokenType.Comma)
        {
            
        }

        public override string GetValue()
        {
            return ",";
        }
    }
}
