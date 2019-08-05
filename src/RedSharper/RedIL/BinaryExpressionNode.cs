using RedSharper.RedIL.Enums;

namespace RedSharper.RedIL
{
    class BinaryExpressionNode : RedILNode
    {
        public BinaryExpressionOperator Operator { get; set; }

        public RedILNode Left { get; set; }

        public RedILNode Right { get; set; }

        public BinaryExpressionNode() : base(RedILNodeType.BinaryExpression) { }

        public BinaryExpressionNode(
            BinaryExpressionOperator op,
            RedILNode left,
            RedILNode right)
            : base(RedILNodeType.BinaryExpression)
        {
            Operator = op;
            Left = left;
            Right = right;
        }

        public override void AcceptVisitor<TState>(IRedILVisitor<TState> visitor, TState state)
        {
            throw new System.NotImplementedException();
        }
    }
}