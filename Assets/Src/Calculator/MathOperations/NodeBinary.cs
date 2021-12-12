using System;

namespace TestProject.Calculator.MathOperations
{
    public class NodeBinary : Node
    {
        public NodeBinary(Node lhs, Node rhs, Func<decimal, decimal, decimal> operation)
        {
            myLeftHandSide = lhs;
            myRightHandSite = rhs;
            myOperation = operation;
        }

        private readonly Node myLeftHandSide;                              
        private readonly Node myRightHandSite;                              
        private readonly Func<decimal, decimal, decimal> myOperation;       

        public override decimal Eval()
        {
            // Evaluate both sides
            var lhsVal = myLeftHandSide.Eval();
            var rhsVal = myRightHandSite.Eval();

            // Evaluate and return
            var result = myOperation(lhsVal, rhsVal);
            return result;
        }
    }
}