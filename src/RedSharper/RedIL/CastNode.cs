﻿using RedSharper.RedIL.Enums;

namespace RedSharper.RedIL
{
    class CastNode : ExpressionNode
    {
        public ExpressionNode Argument { get; set; }

        public CastNode()
            : base(RedILNodeType.Cast)
        { }

        public CastNode(DataValueType toType, ExpressionNode arg)
            : base(RedILNodeType.Cast, toType)
        {
            Argument = arg;
        }

        public override TReturn AcceptVisitor<TReturn, TState>(IRedILVisitor<TReturn, TState> visitor, TState state)
            => visitor.VisitCastNode(this, state);
    }
}