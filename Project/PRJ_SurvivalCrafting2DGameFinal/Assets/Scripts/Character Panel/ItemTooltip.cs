using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// El script ItemTooltip se encarga de mostrar y ocultar un tooltip (información adicional) para un objeto. 
/// Tiene referencias a componentes Text que se utilizan para mostrar el nombre, tipo y descripción del objeto en el tooltip. 
/// El método ShowTooltip se llama para mostrar el tooltip con la información del objeto especificado, mientras que el método HideTooltip se llama para ocultar el tooltip. 
/// El objeto de tooltip se desactiva al inicio en el método Awake.
/// </summary>
public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text _itemNameText;         // Referencia al componente Text para mostrar el nombre del objeto
    [SerializeField] Text _itemTypeText;         // Referencia al componente Text para mostrar el tipo de objeto
    [SerializeField] Text _itemDescriptionText;  // Referencia al componente Text para mostrar la descripción del objeto

    private void Awake()
    {
        gameObject.SetActive(false);  // Desactiva el objeto de tooltip al inicio
    }

    public void ShowTooltip(Item item)
    {
        _itemNameText.text = item.itemName;                // Establece el nombre del objeto en el componente Text
        _itemTypeText.text = item.GetItemType();           // Establece el tipo de objeto en el componente Text
        _itemDescriptionText.text = item.GetDescription(); // Establece la descripción del objeto en el componente Text
        gameObject.SetActive(true);                       // Activa el objeto de tooltip para mostrarlo
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);  // Desactiva el objeto de tooltip para ocultarlo
    }
}