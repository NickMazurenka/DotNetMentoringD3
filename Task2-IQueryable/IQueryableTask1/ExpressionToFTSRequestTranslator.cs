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
		StringBuilder _resultString;

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

		    if (node.Method.DeclaringType == typeof(string))
		    {
		        var left = node.Object;
		        var value = ((ConstantExpression)node.Arguments[0]).Value;
		        Expression right;

		        switch (node.Method.Name)
		        {
		            case "StartsWith":
		                right = Expression.Constant($"{value}*");
		                break;

		            case "EndsWith":
		                right = Expression.Constant($"*{value}");
		                break;

		            case "Contains":
		                right = Expression.Constant($"*{value}*");
		                break;

		            default:
		                throw new NotSupportedException($"Operation {node.NodeType} is not supported");
		        }

		        var predicate = Expression.Equal(left ?? throw new InvalidOperationException(), right);
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

	                if (!(node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType == ExpressionType.Constant) &&
	                    !(node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.MemberAccess))
	                    throw new NotSupportedException("Incorrect operands");

	                if (node.Left.NodeType == ExpressionType.MemberAccess)
	                {
	                    Visit(node.Left);
	                    _resultString.Append("(");
	                    Visit(node.Right);
	                    _resultString.Append(")");
	                }
	                else
	                {
	                    Visit(node.Right);
	                    _resultString.Append("(");
	                    Visit(node.Left);
	                    _resultString.Append(")");
	                }
	                break;

	            case ExpressionType.AndAlso:
	                _resultString.Append("(");
	                Visit(node.Left);
	                _resultString.Append(") AND (");
	                Visit(node.Right);
	                _resultString.Append(")");
                    break;

	            default:
	                throw new NotSupportedException($"Operation {node.NodeType} is not supported");
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

