using System;

namespace TestProject.Calculator.MathOperations
{
    public class ExpressionBuilder
    {
        private Tokenizer myTokenizer;

        public ExpressionBuilder(Tokenizer tokenizer)
        {
            myTokenizer = tokenizer;
        }

        public Node Build()
        {
            var result = BuildAddSubtract();

            if (myTokenizer.Token != Token.EndOfExpression)
            {
                throw new MathSyntaxException("Unexpected characters at end of expression");
            }

            return result;
        }

        private Node BuildAddSubtract()
        {
            var leftHandSide = BuildMultiplyDivide();

            while (true)
            {
                // Work out the operator
                Func<decimal, decimal, decimal> operation = null;
                if (myTokenizer.Token == Token.Add)
                {
                    operation = (a, b) => a + b;
                }
                else if (myTokenizer.Token == Token.Subtract)
                {
                    operation = (a, b) => a - b;
                }

                // Binary operator found?
                if (operation == null)
                {
                    // no
                    return leftHandSide;
                }
    
                // Skip the operator
                myTokenizer.NextToken();

                // Parse the right hand side of the expression
                var rightHandSide = BuildMultiplyDivide();

                // Create a binary node and use it as the left-hand side from now on
                leftHandSide = new NodeBinary(leftHandSide, rightHandSide, operation);
            }
        }
        
        Node BuildMultiplyDivide()
        {
            // Parse the left hand side
            var leftHandSide = BuildLeaf();

            while (true)
            {
                // Work out the operator
                Func<decimal, decimal, decimal> op = null;
                if (myTokenizer.Token == Token.Multiply)
                {
                    op = (a, b) => a * b;
                }
                else if (myTokenizer.Token == Token.Divide)
                {
                    op = (a, b) => a / b;
                }

                // Binary operator found?
                if (op == null)
                {
                    // no
                    return leftHandSide;             
                }

                // Skip the operator
                myTokenizer.NextToken();

                // Parse the right hand side of the expression
                var rightHandSide = BuildLeaf();

                // Create a binary node and use it as the left-hand side from now on
                leftHandSide = new NodeBinary(leftHandSide, rightHandSide, op);
            }
        }

        private Node BuildLeaf()
        {
            // Is it a number?
            if (myTokenizer.Token == Token.Number)
            {
                var node = new NodeNumber(myTokenizer.Number);
                myTokenizer.NextToken();
                return node;
            }

            // Parenthesis?
            if (myTokenizer.Token == Token.OpenParens)
            {
                // Skip '('
                myTokenizer.NextToken();

                // Parse a top-level expression
                var node = BuildAddSubtract();

                // Check and skip ')'
                if (myTokenizer.Token != Token.CloseParens)
                {
                    throw new MathSyntaxException("Missing close parenthesis");
                }
                myTokenizer.NextToken();

                // Return
                return node;
            }

            // Don't Understand
            throw new MathSyntaxException($"Unexpect token: {myTokenizer.Token}");
        }
    }
}