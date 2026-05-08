using UnityEngine;

/// <summary>
/// La clase abstracta UsableItemEffect define el comportamiento de los efectos de los objetos usables.
/// El método ExecuteEffect debe ser implementado por las clases derivadas para ejecutar el efecto del objeto usable en un personaje.
/// El método GetDescription debe ser implementado por las clases derivadas para obtener la descripción del efecto del objeto usable.
/// </summary>
public abstract class UsableItemEffect : ScriptableObject
{
    /// <summary>
    /// Ejecuta el efecto del objeto usable en un personaje.
    /// </summary>
    /// <param name="parentItem">El objeto usable padre que contiene este efecto.</param>
    /// <param name="character">El personaje en el que se aplicará el efecto.</param>
    public abstract void ExecuteEffect(UsableItem parentItem, Character character);

    /// <summary>
    /// Obtiene la descripción del efecto del objeto usable.
    /// </summary>
    /// <returns>La descripción del efecto.</returns>
    public abstract string GetDescription();
}