using System.Collections;
using UnityEngine;
using kenith.CharacterStats;

/// <summary>
/// La clase StatBuffItemEffect representa el efecto de aumento de estadística de un objeto usable.
/// Esta clase hereda de UsableItemEffect, lo que implica que se puede asignar como efecto a un objeto usable.
/// El campo agilityBuff define la cantidad de aumento de la estadística de agilidad.
/// El campo duration define la duración del efecto en segundos.
/// El método ExecuteEffect se encarga de ejecutar el efecto de aumento de estadística en un personaje. Crea un modificador de estadística con el aumento de agilidad, lo agrega a la estadística del personaje y luego inicia una coroutine para remover el modificador después de la duración especificada.
/// El método GetDescription retorna una descripción del efecto de aumento de estadística, indicando la cantidad de aumento y la duración.
/// El método RemoveBuff es una coroutine que espera el tiempo especificado y luego remueve el modificador de estadística de agilidad del personaje.
/// La clase está decorada con el atributo [CreateAssetMenu], lo que permite crear instancias de esta clase como objetos Scriptable en el editor de Unity
/// </summary>
[CreateAssetMenu(menuName = "item Effects/Stat Buff")]
public class StatBuffItemEffect : UsableItemEffect
{
    public int agilityBuff; // Cantidad de aumento de la estadística Agilidad
    public float duration; // Duración del efecto en segundos

    /// <summary>
    /// Ejecuta el efecto de aumento de estadística en el personaje.
    /// </summary>
    /// <param name="parentItem">El objeto usable que ejecuta el efecto.</param>
    /// <param name="character">El personaje al que se aplica el efecto.</param>
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        StatModifier statModifier = new(agilityBuff, StatModType.Flat, parentItem);
        character.agility.AddModifier(statModifier);
        character.StartCoroutine(RemoveBuff(character, statModifier, duration));
    }

    /// <summary>
    /// Obtiene la descripción del efecto de aumento de estadística.
    /// </summary>
    /// <returns>La descripción del efecto.</returns>
    public override string GetDescription()
    {
        return "Grants " + agilityBuff + " agility for " + duration + " seconds.";
    }

    /// <summary>
    /// Coroutine que remueve el efecto de aumento de estadística después de un cierto período de tiempo.
    /// </summary>
    /// <param name="character">El personaje al que se aplica el efecto.</param>
    /// <param name="statModifier">El modificador de estadística a remover.</param>
    /// <param name="duration">La duración del efecto en segundos.</param>
    private static IEnumerator RemoveBuff(Character character, StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.agility.RemoveModifier(statModifier);
    }
}