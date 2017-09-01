using System;

using System.Collections.Generic;

namespace AnalizadorLexico
{
    public class MainProgram
    {
        const char equalOperator = '=';
        const char leftParentheses = '(';
        const char rightParentheses = ')';
        const char semicolon = ';';

        private static List<Token> tokenList;
        private static List<string> reservedWords = new List<string>()
        { "if", "while", "for", "function", "do", "else", "switch", "return", "null", 
          "int", "float", "bool", "string", "double"
        };
        
        public static void Main(string[] args)
        {
            tokenList = new List<Token>();

            string currentLine = Console.ReadLine();

            if (!string.IsNullOrEmpty(currentLine))
            {
                char[] characters = currentLine.ToCharArray();
                int index = 0;
                int lastIndex = -1;

                while (index < characters.Length && index != lastIndex)
                {
                    char currentCharacter = characters[index];

                    if (Char.IsWhiteSpace(currentCharacter))
                    { /// todo tab and new line?
                        lastIndex = index;
                        index++;
                        continue;
                    }

                    #region Equal Identification
                    if (currentCharacter.Equals(equalOperator))
                    {
                        tokenList.Add(new Equal());
                        continue;
                    }
                    #endregion

                    #region Semicolon Identification 

                    if (currentCharacter.Equals(semicolon))
                    {
                        tokenList.Add(new Semicolon());
                        continue;
                    }

                    #endregion

                    #region Left Parentheses Identification 

                    if (currentCharacter.Equals(leftParentheses))
                    {
                        tokenList.Add(new L_parent());
                        continue;
                    }

                    #endregion

                    #region Right Parentheses Identification 

                    if (currentCharacter.Equals(rightParentheses))
                    {
                        tokenList.Add(new R_parent());
                        continue;
                    }

                    #endregion

                    #region Numeric Identification 
                    if (Char.IsDigit(currentCharacter))
                    {
                        int value = 0;
                        double doubleValue = 0;

                        do
                        {
                            value = value * 10 + (int)Char.GetNumericValue(currentCharacter);
                            lastIndex = index;

                            if (index + 1 >= characters.Length)
                                break;
                            
                            currentCharacter = characters[++index];
                        }
                        while (Char.IsDigit(currentCharacter));

                        if (Char.IsPunctuation(currentCharacter))
                        {
                            doubleValue = value;
                            do
                            {
                                doubleValue += Char.GetNumericValue(currentCharacter);
                                lastIndex = index;

                                if (index + 1 >= characters.Length)
                                    break;

                                currentCharacter = characters[++index];
                            }
                            while (Char.IsDigit(currentCharacter));
                        }

                        if (doubleValue > value)
                            tokenList.Add(new Numeric(doubleValue));
                        else
                            tokenList.Add(new Numeric(value));
                        continue;
                    }
                    #endregion

                    #region Reserved word, Id and Function Idenfication 
                    if (Char.IsLetter(currentCharacter))
                    {
                        #region Read word 
                        string stringLiteral = string.Empty;
                        do
                        {
                            stringLiteral += currentCharacter;
                            lastIndex = index;
                            if (index + 1 >= characters.Length)
                                break;
                                                        
                            currentCharacter = characters[++index];
                        } while (Char.IsLetter(currentCharacter));
                        #endregion

                        #region Reserved word
                        if (reservedWords.Contains(stringLiteral))
                        {
                            tokenList.Add(new Reserved_Word(stringLiteral));
                            continue;
                        }
                        #endregion

                        #region Function and Id identification

                        if (tokenList[tokenList.Count - 1].Type == TokenType.Reserved_word &&
                           characters[index + 1].Equals(leftParentheses))
                        {
                            tokenList.Add(new Function(stringLiteral));
                        }
                        else
                        {
                            tokenList.Add(new Id(stringLiteral));
                        }

                        #endregion

                        
                        continue;
                    }
                    #endregion

                    
                    lastIndex = index;
                    
                    // increase index
                    // considerar codigo com comentario. final é final da linha 
                }
            }


        }
    }
}
