namespace AnalizadorLexico
{
    public class RParent : Token
    {
        public RParent() : base(TokenType.R_parent)
        {

        }
        public override string GetValue()
        {
            return ")";
        }
    }
}
