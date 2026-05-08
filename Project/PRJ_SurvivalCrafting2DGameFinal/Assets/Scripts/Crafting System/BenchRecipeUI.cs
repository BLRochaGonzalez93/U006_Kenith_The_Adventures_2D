using System;
using System.Collections.Generic;
using UnityEngine;

public class BenchRecipeUI : MonoBehaviour
{
	[Header("References")]
	[SerializeField] RectTransform _arrowParent;
	[SerializeField] BaseItemSlot[] _itemSlots;
	[SerializeField] GameObject _bench, _cartel;

	[Header("Public Variables")]
	public ItemContainer itemContainer;

	private CraftingRecipe _craftingRecipe;
	public CraftingRecipe CraftingRecipe
	{
		get { return _craftingRecipe; }
		set { SetCraftingRecipe(value); }
	}

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;

	public AudioSource craft;

	private void OnValidate()
	{
		_itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
	}

	private void Start()
	{
		foreach (BaseItemSlot itemSlot in _itemSlots)
		{
			itemSlot.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			itemSlot.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
		}
	}

	public void OnCraftButtonClick()
	{
		if (_craftingRecipe != null && itemContainer != null)
		{
			_craftingRecipe.CraftForBench(itemContainer, _bench, _cartel);
			craft.Play();
			
		}
	}

	private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
	{
		_craftingRecipe = newCraftingRecipe;

		if (_craftingRecipe != null)
		{
			int slotIndex = 0;
			slotIndex = SetSlots(_craftingRecipe.materials, slotIndex);
			_arrowParent.SetSiblingIndex(slotIndex);
			slotIndex = SetSlots(_craftingRecipe.results, slotIndex);

			for (int i = slotIndex; i < _itemSlots.Length; i++)
			{
				_itemSlots[i].transform.parent.gameObject.SetActive(false);
			}

			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
	{
		for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
		{
			ItemAmount itemAmount = itemAmountList[i];
			BaseItemSlot itemSlot = _itemSlots[slotIndex];

			itemSlot.Item = itemAmount.item;
			itemSlot.Amount = itemAmount.amount;
			itemSlot.transform.parent.gameObject.SetActive(true);
		}
		return slotIndex;
	}
}
