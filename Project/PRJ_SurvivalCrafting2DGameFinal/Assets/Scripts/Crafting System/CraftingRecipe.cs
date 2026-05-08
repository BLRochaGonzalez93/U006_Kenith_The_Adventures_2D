using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa una cantidad de un ítem en una receta de crafteo.
/// </summary>
[Serializable]
public struct ItemAmount
{
    public Item item;
    [Range(1, 999)]
    public int amount;
}

/// <summary>
/// Scriptable Object que define una receta de crafteo.
/// </summary>
[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> materials; // Lista de materiales requeridos para la receta.
    public List<ItemAmount> results; // Lista de resultados de la receta.

    /// <summary>
    /// Verifica si es posible realizar el crafteo de acuerdo a los materiales y espacio disponibles en el inventario.
    /// </summary>
    /// <param name="itemContainer">Contenedor de ítems (inventario).</param>
    /// <returns>True si se puede realizar el crafteo, False en caso contrario.</returns>
    public bool CanCraft(IItemContainer itemContainer)
    {
        return HasMaterials(itemContainer) && HasSpace(itemContainer);
    }

    /// <summary>
    /// Verifica si se tienen los materiales necesarios en el inventario.
    /// </summary>
    /// <param name="itemContainer">Contenedor de ítems (inventario).</param>
    /// <returns>True si se tienen los materiales, False en caso contrario.</returns>
    private bool HasMaterials(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in materials)
        {
            if (itemContainer.ItemCount(itemAmount.item.ID) < itemAmount.amount)
            {
                Debug.LogWarning("No tienes los materiales requeridos.");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Verifica si hay suficiente espacio en el inventario para los resultados de la receta.
    /// </summary>
    /// <param name="itemContainer">Contenedor de ítems (inventario).</param>
    /// <returns>True si hay suficiente espacio, False en caso contrario.</returns>
    private bool HasSpace(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in results)
        {
            if (!itemContainer.CanAddItem(itemAmount.item, itemAmount.amount))
            {
                Debug.LogWarning("Tu inventario está lleno.");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Realiza el crafteo de la receta, removiendo los materiales del inventario y agregando los resultados.
    /// </summary>
    /// <param name="itemContainer">Contenedor de ítems (inventario).</param>
    public void Craft(IItemContainer itemContainer)
    {
        if (CanCraft(itemContainer))
        {
            RemoveMaterials(itemContainer);
            AddResults(itemContainer);
        }
    }

    /// <summary>
    /// Realiza el crafteo de la receta específicamente para un banco de crafteo, removiendo los materiales del inventario y mostrando el resultado.
    /// </summary>
    /// <param name="itemContainer">Contenedor de ítems (inventario).</param>
    /// <param name="item">Objeto a activar para mostrar el resultado.</param>
    /// <param name="cartel">Objeto a desactivar después de mostrar el resultado.</param>
    public void CraftForBench(IItemContainer itemContainer, GameObject item, GameObject cartel)
    {
        if (CanCraft(itemContainer))
        {
            RemoveMaterials(itemContainer);
            item.SetActive(true);
            cartel.SetActive(false);
        }
    }

    /// <summary>
    /// Remueve los materiales de la receta del inventario.
    /// </summary>
    /// <param name="itemContainer">Contenedor de ítems (inventario).</param>
    private void RemoveMaterials(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in materials)
        {
            for (int i = 0; i < itemAmount.amount; i++)
            {
                Item oldItem = itemContainer.RemoveItem(itemAmount.item.ID);
                oldItem.Destroy();
            }
        }
    }

    /// <summary>
    /// Remueve los materiales de la receta del inventario específicamente para un banco de crafteo.
    /// </summary>
    /// <param name="itemContainer">Contenedor de ítems (inventario).</param>
    public void RemoveMatForBench(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in materials)
        {
            for (int i = 0; i < itemAmount.amount; i++)
            {
                Item oldItem = itemContainer.RemoveItem(itemAmount.item.ID);
                oldItem.Destroy();
            }
        }
    }

    /// <summary>
    /// Agrega los resultados de la receta al inventario.
    /// </summary>
    /// <param name="itemContainer">Contenedor de ítems (inventario).</param>
    private void AddResults(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in results)
        {
            for (int i = 0; i < itemAmount.amount; i++)
            {
                itemContainer.AddItem(itemAmount.item.GetCopy());
            }
        }
    }
}