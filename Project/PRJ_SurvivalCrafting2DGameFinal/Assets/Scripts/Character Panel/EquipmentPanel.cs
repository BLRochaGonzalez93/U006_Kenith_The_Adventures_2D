using System;
using UnityEngine;

/// <summary>
/// Panel que muestra y gestiona los slots de equipo del jugador.
/// </summary>
public class EquipmentPanel : MonoBehaviour
{
    public EquipmentSlot[] equipmentSlots;
    [SerializeField] Transform _equipmentSlotsParent;
    [SerializeField] SpriteRenderer _playerEquippedWeapon;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    [SerializeField] Item _weaponEquipped;
    [SerializeField] GameObject _wSword, _cSword, _iSword, _gSword;

    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            equipmentSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
            equipmentSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
            equipmentSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
            equipmentSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
            equipmentSlots[i].OnDragEvent += slot => OnDragEvent(slot);
            equipmentSlots[i].OnDropEvent += slot => OnDropEvent(slot);
        }

        EquipWeaponOnStart(_weaponEquipped);
    }

    private void OnValidate()
    {
        equipmentSlots = _equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    /// <summary>
    /// Agrega un ítem equipable al panel de equipo.
    /// </summary>
    /// <param name="item">Ítem equipable a agregar.</param>
    /// <param name="previousItem">Ítem equipable previo en el mismo tipo de equipo.</param>
    /// <returns><c>true</c> si se agrega el ítem correctamente, <c>false</c> de lo contrario.</returns>
    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].equipmentType == item.equipmentType)
            {
                previousItem = (EquippableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                equipmentSlots[i].Amount = 1;
                EquipItems();
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    /// <summary>
    /// Remueve un ítem equipable del panel de equipo.
    /// </summary>
    /// <param name="item">Ítem equipable a remover.</param>
    /// <returns><c>true</c> si se remueve el ítem correctamente, <c>false</c> de lo contrario.</returns>
    public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                equipmentSlots[i].Amount = 0;
                EquipItems();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Equipa los ítems en los slots de equipo.
    /// </summary>
    private void EquipItems()
    {
        EquipWeapon();
    }

    // <summary>
    /// Equipa el arma en el jugador según el ítem equipado en el slot correspondiente.
    /// </summary>
    private void EquipWeapon()
    {
        if (equipmentSlots[4].Item != null)
        {
            _playerEquippedWeapon.enabled = true;
            _playerEquippedWeapon.sprite = equipmentSlots[4].Item.icon;

            switch (equipmentSlots[4].Item.itemName)
            {
                case "Wooden Sword":
                    _wSword.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    _cSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _iSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _gSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    break;
                case "Copper Sword":
                    _wSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _cSword.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    _iSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _gSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    break;
                case "Iron Sword":
                    _wSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _cSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _iSword.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    _gSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    break;
                case "Golden Sword":
                    _wSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _cSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _iSword.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _gSword.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
       
        }
    }


    private void EquipWeaponOnStart(Item item)
    {
        equipmentSlots[4].Item = item;
        _playerEquippedWeapon.enabled = true;
        _playerEquippedWeapon.sprite = equipmentSlots[4].Item.icon;
    }
    /// <summary>
    /// Equipa un arma al inicio del juego.
    /// </summary>
    /// <param name="item">Ítem de arma a equipar.</param>

}
