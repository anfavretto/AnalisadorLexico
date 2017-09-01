namespace AnalizadorLexico
{
    public class Arithmetic_Op : Token 
    {
        public ArithmeticOperatorType ArithmeticOperatorType { get; set; }

        public Arithmetic_Op(ArithmeticOperatorType arithmeticOperatorType) : base(TokenType.Arithmetic_Op)
        {
            this.ArithmeticOperatorType = arithmeticOperatorType;
        }
    }
}
