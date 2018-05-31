using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraph.Parser
{
    public class Builder
    {
        Token[] _tokens;
        int _index;

        private bool IsEOF
            => _index == _tokens.Length;

        private Token Peek(int n = 0)
            => _tokens[_index + n];

        private TokenType TopType
            => Peek().Type;

        private Token Pop()
            => _tokens[_index++];

        private string Top
            => Peek().Code;

        private Builder(string code) : this(Lexer.Lex(code))
        {

        }

        private Builder(Token[] tokens)
        {
            _tokens = tokens;
        }

        public static Node Parse(string code)
            => new Builder(code).Parse();

        private void Error()
        {
            throw new Exception();
        }

        private void Eat(TokenType tok)
        {
            if (Pop().Type != tok)
                Error();
        }

        private Node Parse()
        {
            var left = ParseTerm();
            if (IsEOF) return left;
            if (Top == "+" || Top == "-")
            {
                var op = Pop();
                var right = Parse();
                return new BinaryNode(op, left, right);
            }
            return left;
        }

        private Node ParseTerm()
        {
            var left = ParseFactor();
            if (IsEOF) return left;
            if (Top == "*" || Top == "/" || Top == "%")
            {
                var op = Pop();
                var right = ParseTerm();
                return new BinaryNode(op, left, right);
            }
            return left;
        }

        private Node ParseFactor()
        {
            var left = ParseAtom();
            if (IsEOF) return left;
            if (Top == "^")
            {
                var op = Pop();
                var right = ParseFactor();
                return new BinaryNode(op, left, right);
            }
            return left;
        }

        private Node ParseAtom()
        {
            if (Top == "+" || Top == "-")
            {
                var op = Pop();
                var expr = ParseAtom();
                return new UnaryNode(op, expr);
            }
            
            if (TopType == TokenType.LBracket)
            {
                Eat(TokenType.LBracket);
                var expr = Parse();
                Eat(TokenType.RBracket);
                return expr;
            }
            if (TopType == TokenType.Identifier)
                return new IdentifierNode(Pop());
            if (TopType == TokenType.Numeric)
                return new NumericNode(Pop());

            Error();
            return null;
        }
    }
}
