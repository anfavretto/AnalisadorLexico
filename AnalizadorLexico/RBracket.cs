namespace AnalizadorLexico
{
    public class RBracket : Token
    {
        public RBracket() : base(TokenType.R_braket)
        {

        }
        public override string GetValue()
        {
            return "}";
        }
    }
}
