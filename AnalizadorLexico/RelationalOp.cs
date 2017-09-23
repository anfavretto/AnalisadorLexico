namespace AnalizadorLexico
{
    public class RelationalOp : Token 
    {
        public RelationalOperatorType RelationalOperatorType { get; set; }

        public RelationalOp(RelationalOperatorType relationalOperatorType) : base(TokenType.Arithmetic_Op)
        {
            this.RelationalOperatorType = relationalOperatorType;
        }
    }
}
