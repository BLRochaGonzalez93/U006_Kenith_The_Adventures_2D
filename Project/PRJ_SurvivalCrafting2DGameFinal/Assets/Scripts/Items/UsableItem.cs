using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase UsableItem hereda de la clase item y representa un objeto usable en el juego.
/// El atributo IsConsumable indica si el objeto es consumible o no.
/// La lista Effects contiene los efectos asociados al objeto usable.
/// El método Use se encarga de usar el objeto en un personaje, aplicando todos los efectos asociados.
/// El método GetItemType devuelve el tipo de objeto, que puede ser "Consumable" o "Usable".
/// El método GetDescription obtiene la descripción del objeto, incluyendo los efectos asociados
/// </summary>
[CreateAssetMenu(menuName = "Items/Usable item")]
public class UsableItem : Item
{
    /// <summary>
    /// Indica si el objeto es consumible.
    /// </summary>
    public bool IsConsumable;

    /// <summary>
    /// Lista de efectos del objeto usable.
    /// </summary>
    public List<UsableItemEffect> Effects;

    /// <summary>
    /// Usa el objeto en un personaje, aplicando todos los efectos asociados.
    /// </summary>
    /// <param name="character">El personaje en el que se usará el objeto.</param>
    public virtual void Use(Character character)
    {
        foreach (UsableItemEffect effect in Effects)
        {
            effect.ExecuteEffect(this, character);
        }
    }

    /// <summary>
    /// Obtiene el tipo de objeto.
    /// </summary>
    /// <returns>El tipo de objeto, ya sea "Consumable" o "Usable".</returns>
    public override string GetItemType()
    {
        return IsConsumable ? "Consumable" : "Usable";
    }

    /// <summary>
    /// Obtiene la descripción del objeto, incluyendo los efectos asociados.
    /// </summary>
    /// <returns>La descripción del objeto.</returns>
    public override string GetDescription()
    {
        sb.Length = 0;
        foreach (UsableItemEffect effect in Effects)
        {
            sb.AppendLine(effect.GetDescription());
        }
        return sb.ToString();
    }
}