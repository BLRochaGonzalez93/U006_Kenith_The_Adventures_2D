using UnityEngine;

/// <summary>
/// La clase PlaceableItem hereda de la clase item y representa un objeto colocable en el juego.
/// El atributo IsPlaceable indica si el objeto es colocable o no.
/// Los atributos _placeables y _placedBench son elementos adicionales relacionados con la colocación del objeto, pero no se utilizan en esta implementación.
/// El método Use se encarga de usar el objeto en un personaje, pero en esta implementación no realiza ninguna acción.
/// El método GetItemType devuelve el tipo de objeto, que puede ser "Placeable" o "Usable".
/// </summary>
[CreateAssetMenu(menuName = "Items/Placeable item")]
public class PlaceableItem : Item
{
    /// <summary>
    /// Indica si el objeto es colocable.
    /// </summary>
    public bool IsPlaceable;

    [SerializeField] Item[] _placeables;
    [SerializeField] Transform _placedBench;

    /// <summary>
    /// Método para usar el objeto en un personaje. No realiza ninguna acción en esta implementación.
    /// </summary>
    /// <param name="character">El personaje en el que se usará el objeto.</param>
    public virtual void Use(Character character)
    {
        // No realiza ninguna acción en esta implementación
    }

    /// <summary>
    /// Obtiene el tipo de objeto.
    /// </summary>
    /// <returns>El tipo de objeto, ya sea "Placeable" o "Usable".</returns>
    public override string GetItemType()
    {
        return IsPlaceable ? "Placeable" : "Usable";
    }
}