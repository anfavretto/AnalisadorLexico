namespace AnalizadorLexico
{
    public class Logic_Op : Token
    {
        public LogicOperatorType LogicOperatorType { get; set; }

        public Logic_Op(LogicOperatorType logicOperatorType) : base(TokenType.Logic_Op)
        {
            this.LogicOperatorType = logicOperatorType;
        }
    }
}
