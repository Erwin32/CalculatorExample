using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TestProject.Calculator;
using UnityEngine;
using UnityEngine.TestTools;
using Debug = System.Diagnostics.Debug;

namespace Tests
{
    public class CalculatorTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CalculatorExecuteTest()
        {
            // Use the Assert class to test conditions.
            var go = new GameObject("SampleCalculator");
            try
            {
                var calculator = go.AddComponent<CalculatorMemory>();
                yield return null;
                calculator.AddNumber(2);
                calculator.AddOperation(CalculatorOperationEnum.Add);
                calculator.AddNumber(3);
                calculator.AddOperation(CalculatorOperationEnum.Multiply);
                calculator.AddNumber(2);
                yield return null;
                calculator.Execute();
                yield return null;
                Debug.Assert(calculator.Result != null, "calculator.Result != null");
                Assert.AreEqual(8, calculator.Result.Value);
                yield return null;
                calculator.ClearEverything();
            }
            finally
            {
                GameObject.Destroy(go);
            }
        }
    }
}
