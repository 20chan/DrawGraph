namespace DrawGraph.Parser
{
    public abstract class Node
    {
    }

    public class UnaryNode : Node
    {
        public Token Operator;
        public Node Expression;
        public UnaryNode(Token op, Node expr)
        {
            Operator = op;
            Expression = expr;
        }

        public override bool Equals(object obj)
        {
            if (obj is UnaryNode node)
                return node.Operator.Equals(Operator)
                    && node.Expression.Equals(Expression);
            return false;
        }

        public override int GetHashCode()
        {
            return Operator.GetHashCode() ^ Expression.GetHashCode();
        }
    }

    public class BinaryNode : Node
    {
        public Token Operator;
        public Node Left, Right;
        public BinaryNode(Token op, Node left, Node right)
        {
            Operator = op;
            Left = left;
            Right = right;
        }

        public override bool Equals(object obj)
        {
            if (obj is BinaryNode node)
                return node.Operator.Equals(Operator)
                    && node.Left.Equals(Left)
                    && node.Right.Equals(Right);
            return false;
        }

        public override int GetHashCode()
        {
            return Operator.GetHashCode()
                ^ Left.GetHashCode()
                ^ Right.GetHashCode();
        }
    }

    public class TokenNode : Node
    {
        public Token Value;
        public TokenNode(Token value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is TokenNode node)
                return node.Value.Equals(Value);
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    public class NumericNode : TokenNode
    {
        public NumericNode(Token tok) : base(tok) { }
    }

    public class IdentifierNode : TokenNode
    {
        public IdentifierNode(Token tok) : base(tok) { }
    }
}
