namespace AnalizadorLexico
{
    public class LogicOp : Token
    {
        public LogicOperatorType LogicOperatorType { get; set; }

        public LogicOp(LogicOperatorType logicOperatorType) : base(TokenType.Logic_Op)
        {
            this.LogicOperatorType = logicOperatorType;
        }
        public override string GetValue()
        {
            return LogicOperatorType.ToString();
        }
    }
}
