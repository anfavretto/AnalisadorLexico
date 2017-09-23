namespace AnalizadorLexico
{
    public enum TokenType
    {
        Id = 0,
        Num = 1,
        Reserved_word = 2,
        Relational_Op = 3,
        Function = 4,
        Equal = 5,
        Logic_Op = 6,
        L_parent = 7,
        R_parent = 8,
        L_braket = 9,
        R_braket = 10,
        Comma = 11,
        Semicolon = 12,
        Arithmetic_Op = 13,
        String_Literal = 14,
    }
}
