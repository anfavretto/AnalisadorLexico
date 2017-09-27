namespace AnalizadorLexico
{
    public class Id : Token
    {
        public string Value { get; set; }

        public Id(string value):base(TokenType.Id)
        {
            this.Value = value;
        }
        public override string GetValue()
        {
            return Value;
        }
    }
}
