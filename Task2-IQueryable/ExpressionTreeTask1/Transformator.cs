using System.Collections.Generic;
using System.Linq.Expressions;

namespace TaskExpressionTree
{
    public class LambdaTransform : ExpressionVisitor
    {
        private Dictionary<string,string> _parameters;
        public LambdaTransform(Dictionary<string,string> parameters = null)
        {
            _parameters = parameters;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add || node.NodeType == ExpressionType.Subtract)
            {
                ParameterExpression param = null;
                ConstantExpression constant = null;
                if (node.Left.NodeType == ExpressionType.Parameter)
                {
                    param = (ParameterExpression)node.Left;
                }
                else if (node.Left.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Left;
                }

                if (node.Right.NodeType == ExpressionType.Parameter)
                {
                    param = (ParameterExpression)node.Right;
                }
                else if (node.Right.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Right;
                }

                if (param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                {
                    if (node.NodeType == ExpressionType.Add)
                    {
                        return Expression.Increment(param);
                    }
                    else
                    {
                        return Expression.Decrement(param);
                    }
                }

            }

            return base.VisitBinary(node);
        }
    }
}