using UnityEngine;

namespace TestProject.Calculator
{
    [RequireComponent(typeof(BoxCollider))]
    public class CalculatorButton : MonoBehaviour
    {
        public int NumericValue = 0;
        public CalculatorButtonEnum ButtonFunctionEnum = CalculatorButtonEnum.None;
    }

    public enum CalculatorButtonEnum
    {
        None,
        Send,
        Clear,
        ClearEverything
    }
}