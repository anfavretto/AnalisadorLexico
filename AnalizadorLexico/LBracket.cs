﻿namespace AnalizadorLexico
{
    public class LBracket : Token
    {
        public LBracket() : base(TokenType.L_braket)
        {

        }
        public override string GetValue()
        {
            return "{";
        }
    }
}
