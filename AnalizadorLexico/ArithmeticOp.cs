namespace AnalizadorLexico
{
    public class ArithmeticOp : Token 
    {
        public ArithmeticOperatorType ArithmeticOperatorType { get; set; }

        public ArithmeticOp(ArithmeticOperatorType arithmeticOperatorType) : base(TokenType.Arithmetic_Op)
        {
            this.ArithmeticOperatorType = arithmeticOperatorType;
        }
    }
}
