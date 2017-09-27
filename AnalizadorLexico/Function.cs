namespace AnalizadorLexico
{
    public class Function : Token
    {
        public string Name { get; set; }

        public Function(string name) : base(TokenType.Function)
        {
            Name = name;
        }
        public override string GetValue()
        {
            return Name;
        }
    }
}
