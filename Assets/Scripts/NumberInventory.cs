using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberInventory : MonoBehaviour
{
    public int initialSize = 1;
    public int currentSize = 6;
    public List<int> increaseSizeOnCount;
    private int sizeIncreaseIndex = 0;

    public int maxNumberValue = 99;
    public GameObject slotGrid;
    public GameObject numberGrid;
    public GameObject numbersParentTransform;
    public GameObject slotPrefab;
    public GameObject numberPrefab;
    public GameObject panel;
    [HideInInspector] public List<NumberSlot> slots;
    private int setupCounter = 0;
    private int numberCount = 0;
	public int NumberCount { get => numberCount; set => numberCount = value; }

    [SerializeField] private DepthValue targetDepth;
    [SerializeField] private DepthValue currentDepth;


	void Start()
    {
        ResetInventory();
	}

    public void SetupInventory()
	{
        ClearSlots();

        setupCounter++;
        if(increaseSizeOnCount != null && sizeIncreaseIndex < increaseSizeOnCount.Count && setupCounter == increaseSizeOnCount[sizeIncreaseIndex])
		{
            this.currentSize++;
            sizeIncreaseIndex++;
		}
        

        for(int i=0; i< this.currentSize; i++)
		{
            var newSlot = Instantiate(slotPrefab, slotGrid.transform);
            var newNumberSlotComponent = newSlot.GetComponent<NumberSlot>();
            newNumberSlotComponent.OnSlotEmpty += () => numberCount--;
            newNumberSlotComponent.OnSlotPut += () => numberCount++;
			slots.Add(newNumberSlotComponent);
        }

		StartCoroutine(SetNumbersAtEndOfFrame());

		//var targetDepthValue = targetDepth.currentValue;
  //      var currentDepthValue = currentDepth.currentValue;

  //      List<int> numberSet = GenerateNumberValueSet(targetDepthValue - currentDepthValue, currentSize);

  ////      //for (int i = 0; i < currentSize; i++)
  ////      //{
  ////      //    slots[i].transform.SetParent(slotGrid.transform);
  ////      //}

  //      var gridRectTransform = slotGrid.GetComponent<RectTransform>();
		//UpdateGrid(slotGrid.GetComponent<LayoutGroup>());
		////      slotGrid.gameObject.SetActive(!slotGrid.gameObject.activeSelf);
		////      slotGrid.gameObject.SetActive(!slotGrid.gameObject.activeSelf);
		//////LayoutRebuilder.ForceRebuildLayoutImmediate(gridRectTransform);
		//////LayoutRebuilder.ForceRebuildLayoutImmediate(panel.GetComponent<RectTransform>());

		////      Debug.Log("grid Pos 1 " + slots[0].transform.localPosition);

		////      for (int i = 0; i < currentSize; i++)
		////      {
		////          slots[i].transform.SetParent(panel.transform, true);
		////      }

		////LayoutRebuilder.ForceRebuildLayoutImmediate(panel.GetComponent<RectTransform>());

		////Debug.Log("panel Pos 1 " + slots[0].transform.localPosition);

		//for (int i=0; i < currentSize; i++)
		//{
		//	var slotCanvasPosition = GetChildLocalPosition(gridRectTransform, slots[i].GetComponent<RectTransform>());
		//	//var slotCanvasPosition = slots[i].transform.anch;
		//	CreateNumberInSlot(slots[i], slotCanvasPosition, numberSet[i]);
		//	//CreateNumberInSlots(numberSet[i]);
		//}

  //      //for (int i = 0; i < currentSize; i++)
  //      //{
  //      //    slots[i].transform.SetParent(slotGrid.transform);
  //      //}

  //      //LayoutRebuilder.ForceRebuildLayoutImmediate(gridRectTransform);
  //      //LayoutRebuilder.ForceRebuildLayoutImmediate(panel.GetComponent<RectTransform>());

  //      //UpdateGrid(slotGrid.GetComponent<LayoutGroup>());

  //      Debug.Log("grid Pos 2 " + slots[0].transform.localPosition);
    }

	private void CreateNumberInSlots(int v)
	{
		for(int i=0; i<currentSize; i++)
		{
            var newNumber = Instantiate(numberPrefab, numberGrid.transform);

        }
	}

	public void UpdateGrid(LayoutGroup gridLayoutGroup)
    {
        gridLayoutGroup.CalculateLayoutInputHorizontal();
        gridLayoutGroup.CalculateLayoutInputVertical();
        gridLayoutGroup.SetLayoutHorizontal();
        gridLayoutGroup.SetLayoutVertical();
    }

    public Vector2 GetChildLocalPosition(RectTransform rectTransformGrid, RectTransform rectTransformChild)
    {
        var localPositionGrid = (Vector2)rectTransformGrid.localPosition;
        var sizeDeltaGrid = rectTransformGrid.sizeDelta;
        var deltaGridFromCenterToLeftTop = new Vector2(-0.5f * sizeDeltaGrid.x, 0.5f * sizeDeltaGrid.y);

        var anchoredPositionChild = rectTransformChild.anchoredPosition;
        var childPosition = localPositionGrid + anchoredPositionChild + deltaGridFromCenterToLeftTop;
        return childPosition;
    }

    public void ResetInventory()
	{
        currentSize = initialSize;
        sizeIncreaseIndex = 0;
        setupCounter = 0;
        numberCount = 0;

        SetupInventory();
	}

	private void ClearSlots()
	{
        int count = slots.Count;
		for(int i=0; i<count; i++)
		{
            if(slots[i].Number != null)
                Destroy(slots[i].Number.gameObject);
            Destroy(slots[i].gameObject);
        }
        slots.Clear();
	}

    private IEnumerator SetNumbersAtEndOfFrame()
	{
        yield return new WaitForEndOfFrame();
        for(int i=0; i<currentSize; i++)
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

        List<int> numberSet = GenerateNumberValueSet(-1 * (targetDepthValue - currentDepthValue), currentSize);

        for (int i = 0; i < currentSize; i++)
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
            var nextVal = Random.Range(minVal, maxVal+1);
            Debug.Log("value: " + value + "min: " + " " + minVal + " max: " + maxVal + " val: " + nextVal);
            while(nextVal == currVal || nextVal == value)
			{
                nextVal = Random.Range(minVal, maxVal);
			}
            numSet.Add(Mathf.Abs(nextVal - currVal));
            currVal = nextVal;
        }
        // Add last number
        numSet.Add(Mathf.Abs(value - currVal));

        numSet.Sort((a, b) => a.CompareTo(b));

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
        //Debug.Log("new position: " + newPos);
        //RectTransformUtility.WorldToScreenPoint(null, slot.GetComponent<RectTransform>().TransformPoint(slot.transform.localPosition));
        //newNumber.transform.localPosition = newPos;
        var newNumberComponent = newNumber.GetComponent<Number>();
        newNumberComponent.NumberValue = value;
        newNumberComponent.dragAndDropComponent.StartPosition = newPos;
        newNumberComponent.transform.localPosition = newPos;
        slot.Number = newNumberComponent;
    }

    public void CreateNumberInSlot(NumberSlot slot, Vector3 slotCanvasPosition, int value)
	{
        if (!slot.IsEmpty)
        {
            slot.Clear();
        }
        var newNumber = Instantiate(numberPrefab, panel.transform);
        var newPos = slotCanvasPosition;
        //Debug.Log("new position: " + newPos);
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
