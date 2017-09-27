namespace AnalizadorLexico
{
    public class Equal : Token
    {
        public Equal() : base(TokenType.Equal)
        {

        }
        public override string GetValue()
        {
            return "=";
        }
    }
}
