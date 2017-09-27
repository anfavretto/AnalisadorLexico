using System;
using System.IO;
using System.Linq;
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
        const char comma = ',';
        const char leftBracket = '{';
        const char rightBracket = '}';
        const char andSymbol = '&';
        const char orSymbol = '|';
        const char point = '.';
        const char plus = '+';
        const char minus = '-';
        const char asterisk = '*';
        const char forwardSlash = '/';
        const char percent = '%';


        private static List<Token> tokenList;
        private static List<string> reservedWords = new List<string>()
        { "if", "while", "for", "function", "do", "else", "switch", "return", "null",
          "int", "float", "bool", "string", "double", "void"
        };

        public static void Main(string[] args)
        {
            tokenList = new List<Token>();
            bool readingCommentBlock = false;

            var lines = ReadFile("C:\\Users\\Aline\\Documents\\Unisinos\\Tradutores\\AnalisadorLexico\\code.txt");

            for (int indexLine = 0; indexLine < lines.Count; indexLine++)
            {
                string currentLine = lines[indexLine];

                if (!string.IsNullOrEmpty(currentLine))
                {
                    char[] characters = currentLine.ToCharArray();

                    for (int indexCharacter = 0; indexCharacter < characters.Length; indexCharacter++)
                    {
                        char currentCharacter = characters[indexCharacter];

                        if (Char.IsWhiteSpace(currentCharacter))
                        {
                            continue;
                        }

                        if (currentCharacter.Equals(forwardSlash) && characters[indexCharacter + 1].Equals(forwardSlash))
                        {
                            indexCharacter = characters.Length - 1;
                            continue;
                        }

                        #region Comment Block Identification

                        if (currentCharacter.Equals(forwardSlash) && characters[indexCharacter + 1].Equals(asterisk))
                        {
                            ++indexCharacter; // Pula asterisco
                            currentCharacter = characters[++indexCharacter];
                            readingCommentBlock = true;
                        }

                        if (readingCommentBlock)
                        {
                            if (currentCharacter.Equals(asterisk) && characters[indexCharacter + 1].Equals(forwardSlash))
                            {
                                readingCommentBlock = false;
                                currentCharacter = characters[++indexCharacter];
                                continue;
                            }
                            continue;
                        }

                        #endregion

                        #region String Literal
                        if (currentCharacter.Equals(quotationMark))
                        {
                            string stringContent = string.Empty;
                            currentCharacter = characters[++indexCharacter];
                            while (!currentCharacter.Equals(quotationMark))
                            {
                                stringContent += currentCharacter;

                                currentCharacter = characters[++indexCharacter];
                            }

                            tokenList.Add(new StringLiteral(stringContent));
                            continue;

                        }
                        #endregion

                        #region Relational Operator
                        if (currentCharacter.Equals(greaterThan))
                        {
                            if (characters[indexCharacter + 1].Equals(equal))
                            {
                                tokenList.Add(new RelationalOp(RelationalOperatorType.GreaterOrEqualThan));
                                indexCharacter++;
                                continue;
                            }

                            tokenList.Add(new RelationalOp(RelationalOperatorType.GreaterThan));
                            indexCharacter++;
                            continue;

                        }

                        if (currentCharacter.Equals(lessThan))
                        {
                            if (characters[indexCharacter + 1].Equals(equal))
                            {
                                tokenList.Add(new RelationalOp(RelationalOperatorType.LessOrEqualThan));
                                indexCharacter++;
                                continue;
                            }

                            tokenList.Add(new RelationalOp(RelationalOperatorType.LessThan));
                            indexCharacter++;
                            continue;

                        }

                        if (currentCharacter.Equals(equal))
                        {
                            if (characters[indexCharacter + 1].Equals(equal))
                            {
                                tokenList.Add(new RelationalOp(RelationalOperatorType.Equal));
                                indexCharacter++;
                                continue;
                            }
                            #region Equal Identification

                            tokenList.Add(new Equal());
                            continue;

                            #endregion
                        }

                        if (currentCharacter.Equals(exclamation))
                        {
                            if (characters[indexCharacter + 1].Equals(equal))
                            {
                                tokenList.Add(new RelationalOp(RelationalOperatorType.DifferentOf));
                                indexCharacter++;
                                continue;
                            }
                        }
                        #endregion

                        #region Logic Operator

                        if (currentCharacter.Equals(andSymbol) && characters[indexCharacter + 1].Equals(andSymbol))
                        {
                            tokenList.Add(new LogicOp(LogicOperatorType.And));
                            indexCharacter++;
                            continue;
                        }

                        if (currentCharacter.Equals(orSymbol) && characters[indexCharacter + 1].Equals(orSymbol))
                        {
                            tokenList.Add(new LogicOp(LogicOperatorType.Or));
                            indexCharacter++;
                            continue;
                        }

                        #endregion

                        #region Arithmetic Operator

                        if (currentCharacter.Equals(plus))
                        {
                            tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.Plus));
                            continue;
                        }

                        if (currentCharacter.Equals(minus))
                        {
                            tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.Minus));
                            continue;
                        }

                        if (currentCharacter.Equals(percent))
                        {
                            tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.Percent));
                            continue;
                        }

                        if (currentCharacter.Equals(asterisk))
                        {
                            tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.Asterisk));
                            continue;
                        }

                        if (currentCharacter.Equals(forwardSlash))
                        {
                            tokenList.Add(new ArithmeticOp(ArithmeticOperatorType.ForwardSlash));
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

                        #region Comma Identification 

                        if (currentCharacter.Equals(comma))
                        {
                            tokenList.Add(new Comma());
                            continue;
                        }

                        #endregion

                        #region Parentheses Identification 

                        if (currentCharacter.Equals(leftParentheses))
                        {
                            tokenList.Add(new LParent());
                            continue;
                        }

                        if (currentCharacter.Equals(rightParentheses))
                        {
                            tokenList.Add(new RParent());
                            continue;
                        }

                        #endregion

                        #region Bracket Identification 

                        if (currentCharacter.Equals(leftBracket))
                        {
                            tokenList.Add(new LBracket());
                            continue;
                        }

                        if (currentCharacter.Equals(rightBracket))
                        {
                            tokenList.Add(new RBracket());
                            continue;
                        }

                        #endregion

                        #region Numeric Identification 
                        if (Char.IsDigit(currentCharacter))
                        {
                            int value = 0;
                            double doubleValue = 0;

                            while (Char.IsDigit(currentCharacter))
                            {
                                value = value * 10 + (int)Char.GetNumericValue(currentCharacter);

                                currentCharacter = characters[++indexCharacter];
                            }
                            currentCharacter = characters[--indexCharacter];

                            if (characters[indexCharacter + 1].Equals(point))
                            {
                                currentCharacter = characters[++indexCharacter];
                                currentCharacter = characters[++indexCharacter];
                                while (Char.IsDigit(currentCharacter))
                                {
                                    doubleValue = doubleValue * 10 + (int)Char.GetNumericValue(currentCharacter);
                                    currentCharacter = characters[++indexCharacter];
                                }
                                currentCharacter = characters[--indexCharacter];
                                doubleValue = value + doubleValue / 100;
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
                            while (Char.IsLetter(currentCharacter) || Char.IsDigit(currentCharacter))
                            {
                                stringLiteral += currentCharacter;

                                currentCharacter = characters[++indexCharacter];
                            }
                            currentCharacter = characters[--indexCharacter];
                            #endregion

                            #region Reserved word
                            if (reservedWords.Contains(stringLiteral))
                            {
                                tokenList.Add(new Reserved_Word(stringLiteral));
                                continue;
                            }
                            #endregion

                            #region Function and Id identification
                                                       
                            currentCharacter = characters[++indexCharacter];
                            if (Char.IsWhiteSpace(currentCharacter))
                            {
                                while (Char.IsWhiteSpace(currentCharacter))
                                {
                                    currentCharacter = characters[++indexCharacter];
                                    continue;
                                }
                            }
                            if (currentCharacter.Equals(leftParentheses))
                            {
                                tokenList.Add(new Function(stringLiteral));
                                currentCharacter = characters[--indexCharacter];
                                continue;
                            }

                            tokenList.Add(new Id(stringLiteral));
                            currentCharacter = characters[--indexCharacter];
                            continue;

                            #endregion


                            continue;
                        }
                        #endregion 
                    }
                }
            }
            
            foreach (var token in tokenList)
            {
                Console.Write("[{0},{1}] ", token.Type.ToString(), token.GetValue());
            }
            Console.ReadKey();
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
