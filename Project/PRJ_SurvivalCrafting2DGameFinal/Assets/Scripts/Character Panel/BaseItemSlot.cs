using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// En este código, la clase BaseItemSlot representa un espacio para mostrar un ítem en una interfaz de usuario.
/// image: Referencia al componente Image que muestra el ícono del ítem.
/// amountText: Referencia al componente Text que muestra la cantidad del ítem.
/// OnPointerEnterEvent: Evento que se activa cuando el cursor del mouse entra en el espacio del ítem.
/// OnPointerExitEvent: Evento que se activa cuando el cursor del mouse sale del espacio del ítem.
/// OnRightClickEvent: Evento que se activa cuando se hace clic derecho en el espacio del ítem.
/// item: Propiedad que representa el ítem actual del espacio. Al establecer un valor, se actualiza el ícono y el color del espacio.
/// amount: Propiedad que representa la cantidad actual del ítem. Al establecer un valor, se actualiza el texto de la cantidad.
/// CanAddStack: Método virtual que comprueba si se puede agregar un ítem a la pila existente en el espacio del ítem.
/// CanReceiveItem: Método virtual que comprueba si se puede recibir un ítem en el espacio del ítem.
/// OnPointerClick: Método llamado cuando se hace clic en el espacio del ítem. Si es un clic derecho, se invoca el evento OnRightClickEvent.
/// OnPointerEnter: Método llamado cuando el cursor del mouse entra en el espacio del ítem. Se activa el evento OnPointerEnterEvent.
/// OnPointerExit: Método llamado cuando el cursor del mouse sale del espacio del ítem. Se activa el evento OnPointerExitEvent.
/// Estos son los elementos principales de la clase BaseItemSlot que se utiliza como base para implementar espacios de ítems en una interfaz de usuario de inventario, crafteo u otras funcionalidades relacionadas con ítems.
/// </summary>
public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image image; // Referencia al componente Image para mostrar el ícono del ítem
    [SerializeField] protected Text amountText; // Referencia al componente Text para mostrar la cantidad del ítem

    public event Action<BaseItemSlot> OnPointerEnterEvent; // Evento que se activa cuando el cursor del mouse entra en el espacio del ítem
    public event Action<BaseItemSlot> OnPointerExitEvent; // Evento que se activa cuando el cursor del mouse sale del espacio del ítem
    public event Action<BaseItemSlot> OnRightClickEvent; // Evento que se activa cuando se hace clic derecho en el espacio del ítem

    protected bool isPointerOver; // Indica si el cursor del mouse está sobre el espacio del ítem

    protected Color normalColor = Color.white; // Color normal del ícono del ítem
    protected Color disabledColor = new(1, 1, 1, 0); // Color desactivado del ícono del ítem

    protected Item _item; // Ítem actual del espacio
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null && Amount != 0) Amount = 0;

            if (_item == null)
            {
                image.sprite = null;
                image.color = disabledColor;
            }
            else
            {
                image.sprite = _item.icon;
                image.color = normalColor;
            }

            if (isPointerOver)
            {
                OnPointerExit(null);
                OnPointerEnter(null);
            }
        }
    }

    private int _amount; // Cantidad actual del ítem
    public int Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0 && Item != null) Item = null;

            if (amountText != null)
            {
                amountText.enabled = _item != null && _amount > 1;
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }
        }
    }

    // Comprueba si se puede agregar un ítem a la pila existente en el espacio del ítem
    public virtual bool CanAddStack(Item item, int amount = 1)
    {
        return Item != null && Item.ID == item.ID;
    }

    // Comprueba si se puede recibir un ítem en el espacio del ítem
    public virtual bool CanReceiveItem(Item item)
    {
        return false;
    }

    // Método llamado cuando se valida el script
    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (amountText == null)
            amountText = GetComponentInChildren<Text>();

        Item = _item;
        Amount = _amount;
    }

    // Método llamado cuando el objeto se desactiva
    protected virtual void OnDisable()
    {
        if (isPointerOver)
        {
            OnPointerExit(null);
        }
    }

    // Método llamado cuando se hace clic en el espacio del ítem
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            OnRightClickEvent?.Invoke(this);
        }
    }

    // Método llamado cuando el cursor del mouse entra en el espacio del ítem
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;

        OnPointerEnterEvent?.Invoke(this);
    }

    // Método llamado cuando el cursor del mouse sale del espacio del ítem
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;

        OnPointerExitEvent?.Invoke(this);
    }
}