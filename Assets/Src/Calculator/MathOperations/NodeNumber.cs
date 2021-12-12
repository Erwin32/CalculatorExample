namespace TestProject.Calculator.MathOperations
{
    public class NodeNumber : Node
    {
        public NodeNumber(decimal number)
        {
            myNumber = number;
        }

        private readonly decimal myNumber;   
        
        public override decimal Eval()
        {
            return myNumber;
        }
    }
}