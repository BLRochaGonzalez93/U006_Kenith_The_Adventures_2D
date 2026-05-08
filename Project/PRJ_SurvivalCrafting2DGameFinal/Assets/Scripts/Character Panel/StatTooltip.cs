using System.Text;
using UnityEngine;
using UnityEngine.UI;
using kenith.CharacterStats;

/// <summary>
/// La clase StatTooltip maneja la visualización de información de una estadística en un tooltip.
/// Tiene tres campos serializados que corresponden a los elementos de texto utilizados para mostrar la información en el tooltip: _statNameText, _statModifiersLabelText y _statModifiersText.
/// El método `Awake()` se ejecuta al despertar el objeto y desactiva el tooltip.
/// El método `ShowTooltip(CharacterStat stat, string statName)` muestra el tooltip con la información de la estadística especificada. Recibe la estadística y su nombre como parámetros.
/// El método `HideTooltip()` oculta el tooltip.
/// El método `GetStatTopText(CharacterStat stat, string statName)` obtiene el texto superior del tooltip que muestra el nombre y valor de la estadística. Toma la estadística y su nombre como parámetros y devuelve el texto resultante.
/// El método `GetStatModifiersText(CharacterStat stat)` obtiene el texto que muestra los modificadores de la estadística en el tooltip. Recorre los modificadores de la estadística y construye el texto correspondiente. Devuelve el texto resultante.
/// </summary>
public class StatTooltip : MonoBehaviour
{
    [SerializeField] Text _statNameText; // Texto que muestra el nombre de la estadística
    [SerializeField] Text _statModifiersLabelText; // Etiqueta para los modificadores de la estadística
    [SerializeField] Text _statModifiersText; // Texto que muestra los modificadores de la estadística

    private readonly StringBuilder _sb = new();

    /// <summary>
    /// Método invocado al despertar el objeto.
    /// Desactiva el tooltip.
    /// </summary>
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Muestra el tooltip con la información de la estadística especificada.
    /// </summary>
    /// <param name="stat">Estadística para mostrar</param>
    /// <param name="statName">Nombre de la estadística</param>
    public void ShowTooltip(CharacterStat stat, string statName)
    {
        _statNameText.text = GetStatTopText(stat, statName);
        _statModifiersText.text = GetStatModifiersText(stat);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Oculta el tooltip.
    /// </summary>
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Obtiene el texto superior del tooltip que muestra el nombre y valor de la estadística.
    /// </summary>
    /// <param name="stat">Estadística</param>
    /// <param name="statName">Nombre de la estadística</param>
    /// <returns>Texto superior del tooltip</returns>
    private string GetStatTopText(CharacterStat stat, string statName)
    {
        _sb.Length = 0;
        _sb.Append(statName);
        _sb.Append(" ");
        _sb.Append(stat.Value);

        if (stat.Value != stat.baseValue)
        {
            _sb.Append(" (");
            _sb.Append(stat.baseValue);

            if (stat.Value > stat.baseValue)
                _sb.Append("+");

            _sb.Append(System.Math.Round(stat.Value - stat.baseValue, 4));
            _sb.Append(")");
        }

        return _sb.ToString();
    }

    /// <summary>
    /// Obtiene el texto que muestra los modificadores de la estadística en el tooltip.
    /// </summary>
    /// <param name="stat">Estadística</param>
    /// <returns>Texto de los modificadores</returns>
    private string GetStatModifiersText(CharacterStat stat)
    {
        _sb.Length = 0;

        foreach (StatModifier mod in stat.StatModifiers)
        {
            if (_sb.Length > 0)
                _sb.AppendLine();

            if (mod.value > 0)
                _sb.Append("+");

            if (mod.type == StatModType.Flat)
            {
                _sb.Append(mod.value);
            }
            else
            {
                _sb.Append(mod.value * 100);
                _sb.Append("%");
            }

            Item item = mod.source as Item;

            if (item != null)
            {
                _sb.Append(" ");
                _sb.Append(item.itemName);
            }
            else
            {
                Debug.LogError("Modifier is not an item!");
            }
        }

        return _sb.ToString();
    }
}