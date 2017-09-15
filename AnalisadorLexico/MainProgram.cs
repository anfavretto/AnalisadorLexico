using System;
using System.IO;
using System.Collections.Generic;

namespace AnalizadorLexico
{
    public class MainProgram
    {
        const char equal = '=';
        const char greaterThan = '>';
        const char lessThan = '<';
        const char exclamation = '!';
        const char leftParentheses = '(';
        const char rightParentheses = ')';
        const char semicolon = ';';
        const char quotationMark = '"';


        private static List<Token> tokenList;
        private static List<string> reservedWords = new List<string>()
        { "if", "while", "for", "function", "do", "else", "switch", "return", "null",
          "int", "float", "bool", "string", "double"
        };

        public static void Main(string[] args)
        {
            tokenList = new List<Token>();

            var lines = ReadFile("C:\\Users\\Aline\\Documents\\Unisinos\\Tradutores\\AnalisadorLexico\\code.txt");

            for (int indexLine = 0; indexLine < lines.Count; indexLine++)
            {
                string currentLine = lines[indexLine];

                if (!string.IsNullOrEmpty(currentLine))
                {
                    char[] characters = currentLine.ToCharArray();

                    //while (index < characters.Length && index != lastIndex)
                    for (int indexCharacter = 0; indexCharacter < characters.Length; indexCharacter++)
                    {
                        char currentCharacter = characters[indexCharacter];

                        if (Char.IsWhiteSpace(currentCharacter))
                        { /// todo tab and new line?
                            continue;
                        }

                        if (currentCharacter.Equals(quotationMark))
                        {
                            string stringContent = string.Empty;
                            do
                            {
                                stringContent += currentCharacter;

                                currentCharacter = characters[++indexCharacter];
                            } while (!currentCharacter.Equals(quotationMark));

                            tokenList.Add(new StringLiteral(stringContent));
                            continue;

                        }

                        #region Arithmetic Operator
                        if (currentCharacter.Equals(greaterThan))
                        {
                            if (characters[indexCharacter + 1].Equals(equal))
                            {
                                tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.GreaterOrEqualThan));
                                indexCharacter++;
                                continue;
                            }
                            if (Char.IsWhiteSpace(characters[indexCharacter + 1]))
                            {
                                tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.GreaterThan));
                                indexCharacter++;
                                continue;
                            }
                        }

                        if (currentCharacter.Equals(lessThan))
                        {
                            if (characters[indexCharacter + 1].Equals(equal))
                            {
                                tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.LessOrEqualThan));
                                indexCharacter++;
                                continue;
                            }
                            if (Char.IsWhiteSpace(characters[indexCharacter + 1]))
                            {
                                tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.LessThan));
                                indexCharacter++;
                                continue;
                            }
                        }

                        if (currentCharacter.Equals(equal))
                        {
                            if (characters[indexCharacter + 1].Equals(equal))
                            {
                                tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.Equal));
                                indexCharacter++;
                                continue;
                            }
                            #region Equal Identification
                            if (Char.IsWhiteSpace(characters[indexCharacter + 1]))
                            {
                                tokenList.Add(new Equal());
                                indexCharacter++;
                                continue;
                            }
                            #endregion
                        }

                        if (currentCharacter.Equals(exclamation))
                        {
                            if (characters[indexCharacter + 1].Equals(equal))
                            {
                                tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.DifferentOf));
                                indexCharacter++;
                                continue;
                            }
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
                            tokenList.Add(new LParent());
                            continue;
                        }

                        #endregion

                        #region Right Parentheses Identification 

                        if (currentCharacter.Equals(rightParentheses))
                        {
                            tokenList.Add(new RParent());
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

                                currentCharacter = characters[++indexCharacter];
                            }
                            while (Char.IsDigit(currentCharacter));

                            if (Char.IsPunctuation(currentCharacter))
                            {
                                doubleValue = value;
                                do
                                {
                                    doubleValue += Char.GetNumericValue(currentCharacter);

                                    currentCharacter = characters[++indexCharacter];
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
                            string stringLiteral = string.Empty;
                            #region Read word 
                            do
                            {
                                stringLiteral += currentCharacter;

                                currentCharacter = characters[++indexCharacter];
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
                               characters[indexCharacter + 1].Equals(leftParentheses))
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


                        // considerar codigo com comentario. final é final da linha 
                    }
                }
            }

        }

        private static List<string> ReadFile(string fileName)
        {
            List<string> lines = new List<string>();
            StreamReader reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }
            reader.Close();
            return lines;
        }
    }
}
