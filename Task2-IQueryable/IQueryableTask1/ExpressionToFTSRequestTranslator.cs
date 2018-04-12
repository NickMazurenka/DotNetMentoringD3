using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IQueryableTask1
{
    public class ExpressionToFTSRequestTranslator : ExpressionVisitor
    {
        private StringBuilder _resultString;

        public string Translate(Expression exp)
        {
            _resultString = new StringBuilder();
            Visit(exp);

            return _resultString.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if (node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType == ExpressionType.Constant)
                    {
                        Visit(node.Left);
                        _resultString.Append("(");
                        Visit(node.Right);
                        _resultString.Append(")");
                    }
                    else if (node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.MemberAccess)
                    {
                        Visit(node.Right);
                        _resultString.Append("(");
                        Visit(node.Left);
                        _resultString.Append(")");
                    }
                    else
                    {
                        throw new NotSupportedException($"Operand types combination {node.Left.NodeType} + {node.Right.NodeType} not supported");
                    }
                    break;

                default:
                    throw new NotSupportedException(string.Format($"Operation {node.NodeType} is not supported"));
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultString.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultString.Append(node.Value);

            return node;
        }
    }
}
