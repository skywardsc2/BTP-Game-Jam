using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberInventory : MonoBehaviour
{
    public int size = 6;
    public int maxSize = 8;
    public List<int> increaseSizeOn;
    private int sizeIncreaseIndex = 0;

    public int maxNumberValue = 99;
    public GameObject slotGrid;
    public GameObject numbersParentTransform;
    public GameObject slotPrefab;
    public GameObject numberPrefab;
    [HideInInspector] public List<NumberSlot> slots;
    private int setupCounter = 0;
    private int numberCount = 0;
	public int NumberCount { get => numberCount; set => numberCount = value; }

    [SerializeField] private DepthValue targetDepth;
    [SerializeField] private DepthValue currentDepth;


	void Start()
    {
        SetupInventory();
	}

    public void SetupInventory()
	{
        var currentSize = size;
        if(setupCounter == 0)
		{
            currentSize = 0;
		}

        ClearSlots();

        setupCounter++;
        if(increaseSizeOn != null && sizeIncreaseIndex < increaseSizeOn.Count && setupCounter == increaseSizeOn[sizeIncreaseIndex])
		{
            size++;
            sizeIncreaseIndex++;
		}
        

        for(int i=0; i<size; i++)
		{
            var newSlot = Instantiate(slotPrefab, slotGrid.transform);
            var newNumberSlotComponent = newSlot.GetComponent<NumberSlot>();
            newNumberSlotComponent.OnSlotEmpty += () => numberCount--;
            newNumberSlotComponent.OnSlotPut += () => numberCount++;
            slots.Add(newNumberSlotComponent);
        }

        StartCoroutine(SetNumbersAtEndOfFrame());

    }

	private void ClearSlots()
	{
        int count = slots.Count;
		for(int i=0; i<count; i++)
		{
            Destroy(slots[i].gameObject);
		}
        slots.Clear();
	}

    private IEnumerator SetNumbersAtEndOfFrame()
	{
        yield return new WaitForEndOfFrame();
        for(int i=0; i<size; i++)
		{
            slots[i].transform.SetParent(this.transform, true);
		}
        StartCoroutine(SetSlotsBackToGridEndOfFrame());
    }

    private IEnumerator SetSlotsBackToGridEndOfFrame()
	{
        yield return new WaitForEndOfFrame();
        var targetDepthValue = targetDepth.currentValue;
        var currentDepthValue = currentDepth.currentValue;

        List<int> numberSet = GenerateNumberValueSet(targetDepthValue - currentDepthValue, size);

        for (int i = 0; i < size; i++)
        {
            CreateNumberInSlot(slots[i], numberSet[i]);
            slots[i].transform.SetParent(slotGrid.transform);
        }

    }

	private List<int> GenerateNumberValueSet(int value, int size)
	{
        List<int> numSet = new List<int>();
        var currVal = 0;
        for(int i=0; i<size-1; i++)
		{
            var minVal = Mathf.Max(0, currVal - maxNumberValue);
            var maxVal = Mathf.Min(value + maxNumberValue, currVal + maxNumberValue);
            var nextVal = Random.Range(minVal, maxVal);
            while(nextVal == currVal)
                nextVal = Random.Range(minVal, maxVal);
            numSet.Add(Mathf.Abs(nextVal - currVal));
            currVal = nextVal;
        }
        // Add last number
        numSet.Add(Mathf.Abs(value - currVal));
        return numSet;
	}

    public void CreateNumberInSlot(NumberSlot slot, int value)
	{
		if (!slot.IsEmpty)
		{
            slot.Clear();
		}
        var newNumber = Instantiate(numberPrefab, this.transform);
        var newPos = slot.transform.localPosition;
        Debug.Log("new position: " + newPos);
        //RectTransformUtility.WorldToScreenPoint(null, slot.GetComponent<RectTransform>().TransformPoint(slot.transform.localPosition));
        //newNumber.transform.localPosition = newPos;
        var newNumberComponent = newNumber.GetComponent<Number>();
        newNumberComponent.NumberValue = value;
        newNumberComponent.dragAndDropComponent.StartPosition = newPos;
        newNumberComponent.transform.localPosition = newPos;
        slot.Number = newNumberComponent;
    }

    public void CreateNumberInAnySlot(int value)
	{
		NumberSlot freeSlot = GetFirstFreeSlot();
        var newNumber = Instantiate(numberPrefab, numbersParentTransform.transform);
        newNumber.transform.position = freeSlot.transform.position;
        var newNumberComponent = newNumber.GetComponent<Number>();
        newNumberComponent.NumberValue = value;
        freeSlot.Number = newNumberComponent;
    }

	private NumberSlot GetFirstFreeSlot()
	{
		int i = 0;
		while (!slots[i].IsEmpty && i < slots.Count)
			i++;
		if (i == slots.Count)
		{
			Debug.Log("Number Slots full!");
			return null;
		}

        return slots[i];
	}
}
