using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TestProject.Calculator
{
    [RequireComponent(typeof(CalculatorMemory))]
    public class CalculatorController : MonoBehaviour
    {
        private CalculatorMemory myMemory;
        [SerializeField]
        private Text myDisplayText = null;

        [SerializeField]
        private Canvas myCanvas = null;

        private void Awake()
        {
            myMemory = GetComponent<CalculatorMemory>();
            myMemory.OnDisplayChange += MemoryOnOnDisplayChange;
            myMemory.ClearEverything();
            myCanvas.worldCamera = Camera.main;
            myDisplayText.text = "0";
        }

        private void OnDestroy()
        {
            myMemory.OnDisplayChange -= MemoryOnOnDisplayChange;
        }

        private void MemoryOnOnDisplayChange(string displayValue)
        {
            myDisplayText.text = displayValue;
        }

        public void ___InputNumber(int number)
        {
            myMemory.AddNumber(number);
        }
        
        public void ___InputOperationAdd()
        {
            myMemory.AddOperation(CalculatorOperationEnum.Add);
        }
        
        public void ___InputOperationSubtract()
        {
            myMemory.AddOperation(CalculatorOperationEnum.Subtract);
        }
        
        public void ___InputOperationDivide()
        {
            myMemory.AddOperation(CalculatorOperationEnum.Division);
        }
        
        public void ___InputOperationMultiply()
        {
            myMemory.AddOperation(CalculatorOperationEnum.Multiply);
        }
        
        public void ___InputCe()
        {
            myMemory.ClearEverything();
        }
        
        public void ___InputExecute()
        {
            myMemory.Execute();
            if (myMemory.Result != null)
            {
                myDisplayText.text = myMemory.Result.Value.ToString();
            }
        }
    }
}