using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationStack : MonoBehaviour
{
	private class Operation
	{
		public Number number;
		public int multiplier;

		public Operation(Number number, int multiplier)
		{
			this.number = number;
			this.multiplier = multiplier;
		}
	}

    private Stack<Operation> operationStack = new Stack<Operation>();

	public CurrentDepth currentDepth;
	public NumberInventory numberInventory;

	public Bag DiveBag;
	public Bag RiseBag;

	private void Start()
	{
		DiveBag.OnNumberDrop += PushNumberOperation;
		RiseBag.OnNumberDrop += PushNumberOperation;
	}

	public void UndoNumberOperation()
	{
		if (operationStack.Count > 0)
		{
			var lastOperation = operationStack.Pop();
			var newValue = currentDepth.currentValue + lastOperation.number.NumberValue * lastOperation.multiplier * -1;
			currentDepth.UpdateValue(newValue);
			lastOperation.number.Reenable();
		} else
		{
			Debug.Log("Operation Stack empty!");
		}
	}

	public void PushNumberOperation(Number number, int multiplier)
	{
		operationStack.Push(new Operation(number, multiplier));
	}

	public void ResetOperations()
	{
		List<Operation> operations = new List<Operation>();
		while(operationStack.Count > 0)
		{
			Destroy(operationStack.Pop().number.gameObject);
		}
	}
}
