using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// En este código, se define la clase `CraftingRecipeUI` que se encarga de gestionar la interfaz de usuario para una receta de crafteo.
/// `References`: Esta sección contiene las referencias a los objetos del Editor de Unity que se utilizan en el script.
/// `Public Variables`: En esta sección se declaran las variables públicas que se pueden configurar desde el Inspector de Unity.
/// `CraftingRecipe`: Propiedad que permite obtener y establecer la receta de crafteo actual.
/// `OnPointerEnterEvent` y `OnPointerExitEvent`: Eventos que se activan cuando el cursor del mouse entra o sale de un espacio de ítem.
/// `OnValidate()`: Método llamado cuando se valida el script, se encarga de obtener los componentes `BaseItemSlot` de los hijos, incluyendo los inactivos.
/// `Start()`: Método llamado al iniciar el objeto, se suscriben los eventos `OnPointerEnterEvent` y `OnPointerExitEvent` de los espacios de ítems.
/// `OnCraftButtonClick()`: Método llamado cuando se hace clic en el botón de crafteo, verifica si hay una receta de crafteo y un contenedor de ítems y realiza el crafteo.
/// `SetCraftingRecipe()`: Establece la receta de crafteo y actualiza la interfaz de usuario en consecuencia.
/// `SetSlots()`: Establece los espacios de ítems en la interfaz de usuario según la lista de cantidades de ítems.
/// </summary>
public class CraftingRecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform _arrowParent;
    [SerializeField] BaseItemSlot[] _itemSlots;

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

    /// <summary>
    /// Método llamado cuando se valida el script.
    /// Obtiene los componentes BaseItemSlot de los hijos, incluyendo los inactivos.
    /// </summary>
    private void OnValidate()
    {
        _itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
    }

    /// <summary>
    /// Método llamado al iniciar el objeto.
    /// Suscribe los eventos OnPointerEnterEvent y OnPointerExitEvent de los espacios de ítems.
    /// </summary>
    private void Start()
    {
        foreach (BaseItemSlot itemSlot in _itemSlots)
        {
            itemSlot.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            itemSlot.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
        }
    }

    /// <summary>
    /// Método llamado cuando se hace clic en el botón de crafteo.
    /// Realiza el crafteo de la receta si está disponible.
    /// </summary>
    public void OnCraftButtonClick()
    {
        if (_craftingRecipe != null && itemContainer != null)
        {
            _craftingRecipe.Craft(itemContainer);
            craft.Play();
        }
    }

    /// <summary>
    /// Establece la receta de crafteo y actualiza la interfaz de usuario en consecuencia.
    /// </summary>
    /// <param name="newCraftingRecipe">La nueva receta de crafteo.</param>
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

    /// <summary>
    /// Establece los espacios de ítems en la interfaz de usuario según la lista de cantidades de ítems.
    /// </summary>
    /// <param name="itemAmountList">La lista de cantidades de ítems.</param>
    /// <param name="slotIndex">El índice del espacio de ítems donde comenzar a establecer.</param>
    /// <returns>El nuevo índice del espacio de ítems.</returns>
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