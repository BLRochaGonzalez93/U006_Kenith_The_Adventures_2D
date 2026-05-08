using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public ItemContainer itemContainer;  // Referencia al contenedor de objetos
    public Item item;  // Objeto a soltar
    public Item secondRandopDrop;  // Segundo objeto aleatorio a soltar
    [SerializeField] GameObject _equippedItem;  // Objeto equipado (opcional)

    /// <summary>
    /// Suelta la recompensa al contenedor de objetos.
    /// </summary>
    public void DropReward()
    {
        // Agrega una cantidad aleatoria de los objetos al contenedor de objetos
        itemContainer.AddRandomQuantityItem(item, secondRandopDrop);
    }
}