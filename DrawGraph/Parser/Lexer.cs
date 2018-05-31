using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraph.Parser
{
    public sealed class Lexer
    {
        private string _code;
        private int _index;
        private char Peek => _code[_index];
        private char Pop() => _code[_index++];
        private bool IsEOF => _code.Length == _index || IsWhitespacesOnlyLeft();

        private Lexer(string code)
        {
            _code = code;
        }

        public static Token[] Lex(string expression)
        {
            return new Lexer(expression).Lex().ToArray();
        }

        private bool IsWhitespacesOnlyLeft()
        {
            if (_code.Length == _index) return true;
            for (int idx = _index; idx < _code.Length; idx++)
            {
                if (!char.IsWhiteSpace(_code, idx))
                    return false;
            }
            return true;
        }

        private void Error(string msg = null)
        {
            throw new Exception(msg);
        }

        private IEnumerable<Token> Lex()
        {
            while (!IsEOF)
            {
                yield return LexOne();
            }

            Token LexOne()
            {
                switch (Peek)
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '%':
                    case '^':
                        return new Token(Pop().ToString(), TokenType.Operator);
                    case '(':
                    case '{':
                    case '[':
                        return new Token(Pop().ToString(), TokenType.LBracket);
                    case ')':
                    case '}':
                    case ']':
                        return new Token(Pop().ToString(), TokenType.RBracket);
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        int start = _index;
                        bool dot = false;
                        while (!IsEOF)
                        {
                            if (Peek == '.')
                            {
                                if (dot)
                                    Error("실수 파싱 문제");
                                dot = true;
                            }
                            else if (char.IsDigit(Peek)) ;
                            else
                                break;
                            Pop();
                        }
                        return new Token(_code.Substring(start, _index - start), TokenType.Numeric);
                    default:
                        if (char.IsLetter(Peek))
                            return new Token(Pop().ToString(), TokenType.Identifier);
                        break;
                }

                Error(); return null;
            }
        }
    }
}
