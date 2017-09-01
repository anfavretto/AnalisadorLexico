namespace AnalizadorLexico
{
    public class Reserved_Word : Token
    {
        public string Word { get; set; }

        public Reserved_Word(string word) : base(TokenType.Reserved_word)
        {
            this.Word = word;
        }
    }
}
