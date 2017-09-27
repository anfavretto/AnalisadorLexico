namespace AnalizadorLexico
{
    public class LParent : Token
    {
        public LParent() : base(TokenType.L_parent)
        {

        }
        public override string GetValue()
        {
            return "(";
        }
    }
}
