namespace AnalizadorLexico
{
    public class ArithmeticOp : Token
    {
        public ArithmeticOperatorType ArithmeticOperatorType { get; set; }

        public ArithmeticOp(ArithmeticOperatorType type) : base(TokenType.Arithmetic_Op)
        {
            ArithmeticOperatorType = type;
        }
    }
}
