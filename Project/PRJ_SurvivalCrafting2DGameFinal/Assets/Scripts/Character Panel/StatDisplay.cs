using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using kenith.CharacterStats;

/// <summary>
/// El script StatDisplay se utiliza para mostrar una estadística en la interfaz de usuario. 
/// Tiene propiedades Stat y Name para establecer la estadística y el nombre correspondientes. 
/// El método OnPointerEnter se llama cuando el puntero entra en el objeto, mostrando el tooltip asociado a la estadística. 
/// El método OnPointerExit se llama cuando el puntero sale del objeto, ocultando el tooltip. 
/// Los métodos UpdateStatValue se utilizan para actualizar el valor de la estadística en la interfaz de usuario.
/// </summary>
public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStat _stat;
    public CharacterStat Stat
    {
        get { return _stat; }
        set
        {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            nameText.text = _name.ToLower();
        }
    }

    [SerializeField] Text nameText;            // Referencia al componente Text para mostrar el nombre de la estadística
    [SerializeField] Text valueText;           // Referencia al componente Text para mostrar el valor de la estadística
    [SerializeField] StatTooltip tooltip;      // Referencia al script StatTooltip para mostrar el tooltip

    private bool showingTooltip;                // Indica si se está mostrando el tooltip

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];

        if (tooltip == null)
            tooltip = FindObjectOfType<StatTooltip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Stat, Name);          // Muestra el tooltip con la estadística y el nombre
        showingTooltip = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();                     // Oculta el tooltip
        showingTooltip = false;
    }

    public void UpdateStatValue(int StatLevel)
    {
        valueText.text = StatLevel.ToString();      // Actualiza el valor de la estadística con el nivel especificado
        if (showingTooltip)
        {
            tooltip.ShowTooltip(Stat, Name);         // Muestra el tooltip si está siendo mostrado actualmente
        }
    }

    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();     // Actualiza el valor de la estadística con el valor actual
        if (showingTooltip)
        {
            tooltip.ShowTooltip(Stat, Name);          // Muestra el tooltip si está siendo mostrado actualmente
        }
    }
}