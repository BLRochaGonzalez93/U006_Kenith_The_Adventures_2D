using UnityEngine;
using UnityEngine.UI;
using kenith.CharacterStats;

public class Character : MonoBehaviour
{
	// Niveles de las estadísticas
	public int vitalityLvl = 0;
	public int strengthLvl = 0;
	public int agilityLvl = 0;
	public int intelligenceLvl = 0;

	public int startHealth = 50; // Salud inicial del personaje
	public int currentHealth; // Salud actual del personaje
	public int skillPoints; // Puntos de habilidad

	public LifeStaExpBarsUI lseUI; // Referencia al UI de barras de vida, estado y experiencia

	[Header("Stats")]
	public CharacterStat level; // Estadística de nivel
	public CharacterStat strength; // Estadística de fuerza
	public CharacterStat agility; // Estadística de agilidad
	public CharacterStat intelligence; // Estadística de inteligencia
	public CharacterStat vitality; // Estadística de vitalidad

	[Header("Public")]
	public Inventory inventory; // Inventario del personaje
	public EquipmentPanel equipmentPanel; // Panel de equipo del personaje

	[Header("Serialize Field")]
	[SerializeField] CraftingWindow _craftingWindow; // Ventana de creación
	[SerializeField] StatPanel _statPanel; // Panel de estadísticas
	[SerializeField] ItemTooltip _itemTooltip; // Información sobre objetos
	[SerializeField] Image _draggableItem; // Imagen del objeto arrastrable
	[SerializeField] DropItemArea _dropItemArea; // Área de soltar objetos
	[SerializeField] QuestionDialog _reallyDropItemDialog; // Diálogo de confirmación de eliminar objeto

	public GameObject strUpBtn, agiUpBtn, inteUpBtn, vitUpBtn; // Botones para aumentar las estadísticas

	private BaseItemSlot _dragItemSlot; // Ranura del objeto arrastrado

	public AudioSource curacion, lvlUpSrc;

	private void OnValidate()
	{
		if (_itemTooltip == null)
			_itemTooltip = FindObjectOfType<ItemTooltip>();
	}

	private void Awake()
	{
		// Configurar los valores iniciales de las estadísticas
		_statPanel.SetStats(level, strength, agility, intelligence, vitality);
		_statPanel.UpdateStatValues(strengthLvl);
		_statPanel.UpdateStatValues(intelligenceLvl);
		_statPanel.UpdateStatValues(agilityLvl);
		_statPanel.UpdateStatValues(vitalityLvl);

		currentHealth = startHealth;

		// Configurar eventos:
		// Clic derecho
		inventory.OnRightClickEvent += InventoryRightClick;
		equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
		// Entrada del puntero
		inventory.OnPointerEnterEvent += ShowTooltip;
		equipmentPanel.OnPointerEnterEvent += ShowTooltip;
		// Salida del puntero
		inventory.OnPointerExitEvent += HideTooltip;
		equipmentPanel.OnPointerExitEvent += HideTooltip;
		// Inicio del arrastre
		inventory.OnBeginDragEvent += BeginDrag;
		equipmentPanel.OnBeginDragEvent += BeginDrag;
		// Fin del arrastre
		inventory.OnEndDragEvent += EndDrag;
		equipmentPanel.OnEndDragEvent += EndDrag;
		// Arrastre
		inventory.OnDragEvent += Drag;
		equipmentPanel.OnDragEvent += Drag;
		// Soltar
		inventory.OnDropEvent += Drop;
		equipmentPanel.OnDropEvent += Drop;
		_dropItemArea.OnDropEvent += DropItemOutsideUI;
	}

	private void InventoryRightClick(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item is EquippableItem item)
		{
			Equip(item);
		}
		else if (itemSlot.Item is UsableItem usableItem)
        {
            usableItem.Use(this);
			curacion.Play();

            if (usableItem.IsConsumable)
            {
                itemSlot.Amount--;
                usableItem.Destroy();
            }
        }
    }

	private void EquipmentPanelRightClick(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item is EquippableItem item)
		{
			Unequip(item);
		}
	}

	private void ShowTooltip(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item != null)
		{
			_itemTooltip.ShowTooltip(itemSlot.Item);
		}
	}

	private void HideTooltip(BaseItemSlot itemSlot)
	{
		if (_itemTooltip.gameObject.activeSelf)
		{
			_itemTooltip.HideTooltip();
		}
	}

	private void BeginDrag(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item != null)
		{
			_dragItemSlot = itemSlot;
			_draggableItem.sprite = itemSlot.Item.icon;
			_draggableItem.transform.position = Input.mousePosition;
			_draggableItem.gameObject.SetActive(true);
		}
	}

	private void Drag(BaseItemSlot itemSlot)
	{
		_draggableItem.transform.position = Input.mousePosition;
	}

	private void EndDrag(BaseItemSlot itemSlot)
	{
		_dragItemSlot = null;
		_draggableItem.gameObject.SetActive(false);
	}

	private void Drop(BaseItemSlot dropItemSlot)
	{
		if (_dragItemSlot == null) return;

		if (dropItemSlot.CanAddStack(_dragItemSlot.Item))
		{
			AddStacks(dropItemSlot);
		}
		else if (dropItemSlot.CanReceiveItem(_dragItemSlot.Item) && _dragItemSlot.CanReceiveItem(dropItemSlot.Item))
		{
			SwapItems(dropItemSlot);
		}
	}

	private void AddStacks(BaseItemSlot dropItemSlot)
	{
		int numAddableStacks = dropItemSlot.Item.maximumStacks - dropItemSlot.Amount;
		int stacksToAdd = Mathf.Min(numAddableStacks, _dragItemSlot.Amount);

		dropItemSlot.Amount += stacksToAdd;
		_dragItemSlot.Amount -= stacksToAdd;
	}

	private void SwapItems(BaseItemSlot dropItemSlot)
	{
		EquippableItem dragEquipItem = _dragItemSlot.Item as EquippableItem;
		EquippableItem dropEquipItem = dropItemSlot.Item as EquippableItem;

		if (dropItemSlot is EquipmentSlot)
		{
			if (dragEquipItem != null) dragEquipItem.Equip(this);
			if (dropEquipItem != null) dropEquipItem.Unequip(this);
		}
		if (_dragItemSlot is EquipmentSlot)
		{
			if (dragEquipItem != null) dragEquipItem.Unequip(this);
			if (dropEquipItem != null) dropEquipItem.Equip(this);
		}
		_statPanel.UpdateStatValues(strengthLvl);
		_statPanel.UpdateStatValues(intelligenceLvl);
		_statPanel.UpdateStatValues(agilityLvl);
		_statPanel.UpdateStatValues(vitalityLvl);

		Item draggedItem = _dragItemSlot.Item;
		int draggedItemAmount = _dragItemSlot.Amount;

		_dragItemSlot.Item = dropItemSlot.Item;
		_dragItemSlot.Amount = dropItemSlot.Amount;

		dropItemSlot.Item = draggedItem;
		dropItemSlot.Amount = draggedItemAmount;
	}

	private void DropItemOutsideUI()
	{
		if (_dragItemSlot == null) return;

		_reallyDropItemDialog.Show();
		BaseItemSlot slot = _dragItemSlot;
		_reallyDropItemDialog.OnYesEvent += () => DestroyItemInSlot(slot);
	}

	private void DestroyItemInSlot(BaseItemSlot itemSlot)
	{
		// If the item is equiped, unequip first
		if (itemSlot is EquipmentSlot)
		{
			EquippableItem equippableItem = (EquippableItem)itemSlot.Item;
			equippableItem.Unequip(this);
		}

		itemSlot.Item.Destroy();
		itemSlot.Item = null;
	}

	public void Equip(EquippableItem item)
	{
		if (inventory.RemoveItem(item))
		{
			if (equipmentPanel.AddItem(item, out EquippableItem previousItem))
			{
				if (previousItem != null)
				{
					inventory.AddItem(previousItem);
					previousItem.Unequip(this);
					_statPanel.UpdateStatValues(strengthLvl);
					_statPanel.UpdateStatValues(intelligenceLvl);
					_statPanel.UpdateStatValues(agilityLvl);
					_statPanel.UpdateStatValues(vitalityLvl);
				}
				item.Equip(this);
				_statPanel.UpdateStatValues(strengthLvl);
				_statPanel.UpdateStatValues(intelligenceLvl);
				_statPanel.UpdateStatValues(agilityLvl);
				_statPanel.UpdateStatValues(vitalityLvl);
			}
			else
			{
				inventory.AddItem(item);
			}
		}
	}

	public void Unequip(EquippableItem item)
	{
		if (inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item))
		{
			item.Unequip(this);
			_statPanel.UpdateStatValues(strengthLvl);
			_statPanel.UpdateStatValues(intelligenceLvl);
			_statPanel.UpdateStatValues(agilityLvl);
			_statPanel.UpdateStatValues(vitalityLvl);
			inventory.AddItem(item);
		}
	}

	private ItemContainer openItemContainer;

	private void TransferToItemContainer(BaseItemSlot itemSlot)
	{
		Item item = itemSlot.Item;
		if (item != null && openItemContainer.CanAddItem(item))
		{
			inventory.RemoveItem(item);
			openItemContainer.AddItem(item);
		}
	}

	private void TransferToInventory(BaseItemSlot itemSlot)
	{
		Item item = itemSlot.Item;
		if (item != null && inventory.CanAddItem(item))
		{
			openItemContainer.RemoveItem(item);
			inventory.AddItem(item);
		}
	}

	public void OpenItemContainer(ItemContainer itemContainer)
	{
		openItemContainer = itemContainer;

		inventory.OnRightClickEvent -= InventoryRightClick;
		inventory.OnRightClickEvent += TransferToItemContainer;

		itemContainer.OnRightClickEvent += TransferToInventory;

		itemContainer.OnPointerEnterEvent += ShowTooltip;
		itemContainer.OnPointerExitEvent += HideTooltip;
		itemContainer.OnBeginDragEvent += BeginDrag;
		itemContainer.OnEndDragEvent += EndDrag;
		itemContainer.OnDragEvent += Drag;
		itemContainer.OnDropEvent += Drop;
	}

	public void CloseItemContainer(ItemContainer itemContainer)
	{
		openItemContainer = null;

		inventory.OnRightClickEvent += InventoryRightClick;
		inventory.OnRightClickEvent -= TransferToItemContainer;

		itemContainer.OnRightClickEvent -= TransferToInventory;

		itemContainer.OnPointerEnterEvent -= ShowTooltip;
		itemContainer.OnPointerExitEvent -= HideTooltip;
		itemContainer.OnBeginDragEvent -= BeginDrag;
		itemContainer.OnEndDragEvent -= EndDrag;
		itemContainer.OnDragEvent -= Drag;
		itemContainer.OnDropEvent -= Drop;
	}

	public void UpdateStatValues()
	{
		_statPanel.UpdateStatValues(strengthLvl);
		_statPanel.UpdateStatValues(intelligenceLvl);
		_statPanel.UpdateStatValues(agilityLvl);
		_statPanel.UpdateStatValues(vitalityLvl);
	}
	
	public void GetSkillPoint()
	{
		skillPoints++;
	}

	public void VitalityUp()
	{
		if (skillPoints >= 1 && vitalityLvl < 5)
		{
			skillPoints--;
			vitalityLvl++;
			vitality.baseValue++;
			startHealth += 15;
			currentHealth += 15;
			_statPanel.UpdateStatValues(vitalityLvl);
			lseUI.SetHealth();
		}
	}
	public void PowerUp()
	{
		if (skillPoints >= 1 && strengthLvl < 5)
		{
			skillPoints--;
			strengthLvl++;
			strength.baseValue++;
			_statPanel.UpdateStatValues(strengthLvl);
		}
	}
	public void AgilityUp()
	{
		if (skillPoints >= 1 && agilityLvl < 5)
		{
			skillPoints--;
			agilityLvl++;
			agility.baseValue++;
			GetComponent<PlayerMovement>().dashCD -= 0.5f;
			_statPanel.UpdateStatValues(agilityLvl);
			lseUI.SetEnergy();
		}
	}

	public void IntelligenceUp()
	{
		if (skillPoints >= 1 && intelligenceLvl < 5)
		{
			skillPoints--;
			intelligenceLvl++;
			intelligence.baseValue++;
			_statPanel.UpdateStatValues(intelligenceLvl);
		}
	}

	public void LevelUp()
	{
		level.baseValue++;
		GetSkillPoint();
		lvlUpSrc.Play();
		_statPanel.UpdateStatValues(strengthLvl);
		_statPanel.UpdateStatValues(intelligenceLvl);
		_statPanel.UpdateStatValues(agilityLvl);
		_statPanel.UpdateStatValues(vitalityLvl);
		lseUI.SetExperience();
		strUpBtn.SetActive(true);
		agiUpBtn.SetActive(true);
		inteUpBtn.SetActive(true);
		vitUpBtn.SetActive(true);
	}

	public void CheckLevelUps()
    {
        if (skillPoints > 0)
        {

			strUpBtn.SetActive(true);
			agiUpBtn.SetActive(true);
			inteUpBtn.SetActive(true);
			vitUpBtn.SetActive(true);
		}
        else
        {
			strUpBtn.SetActive(false);
			agiUpBtn.SetActive(false);
			inteUpBtn.SetActive(false);
			vitUpBtn.SetActive(false);
		}
    }

}
