using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using TestProject.Calculator.MathOperations;
using UnityEngine;

namespace TestProject.Calculator
{
    public class CalculatorMemory : MonoBehaviour
    {
        // public int StepIndex = 0;
        private readonly StringBuilder myCalculatorStringBuilder = new StringBuilder();
        private bool myLastCharacterWasOperator = false;

        public decimal? Result { get; private set; } = null;
        public string ExpressionString { get; private set;}

        public delegate void NotifyChange(string shownExpression);

        public event NotifyChange OnDisplayChange;
        
        #region cached

        #endregion

        // private void Start()
        // {
        //     Execute();
        // }

        public void AddOperation(CalculatorOperationEnum operation)
        {
            if (myLastCharacterWasOperator)
            {
                throw new InvalidDataException($"Invalid math syntax => cannot add operator after previous operator without adding a number or parentheses");
            }
            
            switch (operation)
            {
                case CalculatorOperationEnum.Add:
                    myCalculatorStringBuilder.Append(" + ");
                    break;
                case CalculatorOperationEnum.Subtract:
                    myCalculatorStringBuilder.Append(" - ");
                    break;
                case CalculatorOperationEnum.Multiply:
                    myCalculatorStringBuilder.Append(" * ");
                    break;
                case CalculatorOperationEnum.Division:
                    myCalculatorStringBuilder.Append(" / ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }

            myLastCharacterWasOperator = true;
            
            BuildStringExpression();
            OnDisplayChange?.Invoke(ExpressionString);
        }

        public void AddNumber(int number)
        {
            myCalculatorStringBuilder.Append(number);
            myLastCharacterWasOperator = false;
            BuildStringExpression();
            OnDisplayChange?.Invoke(ExpressionString);
        }

        public void ClearEverything()
        {
            myCalculatorStringBuilder.Clear();
            Result = null;
            OnDisplayChange?.Invoke("0");
        }
        
        /// <summary>
        /// Simple arithmetic execution
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Execute()
        {
            BuildStringExpression();

            using (var stringReader = new StringReader(ExpressionString))
            {
                var tokenizer = new Tokenizer(stringReader);
                var builder = new ExpressionBuilder(tokenizer);
            
                //calculate
                Result = builder.Build().Eval();    
            }

            //display result and prep memory for followup
            myCalculatorStringBuilder.Clear();
            myCalculatorStringBuilder.Append(Result.Value);
        }

        private void BuildStringExpression()
        {
            ExpressionString = myCalculatorStringBuilder.ToString().TrimEnd('\0') + "\0";
        }
    }

    public enum CalculatorOperationEnum
    {
        Add,
        Subtract,
        Multiply,
        Division
    }
}