namespace AnalizadorLexico
{
    public class Numeric : Token
    {
        public int? IntValue { get; set; }
        public double? DoubleValue { get; set; }

        public Numeric(int value) : base(TokenType.Num)
        {
            this.IntValue = value;
        }

        public Numeric(double value) : base(TokenType.Num)
        {
            this.DoubleValue = value;
        }
    }
}
