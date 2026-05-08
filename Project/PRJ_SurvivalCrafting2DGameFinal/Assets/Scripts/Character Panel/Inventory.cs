using UnityEngine;

/// <summary>
/// En este script de inventario, se utiliza una subclase de itemContainer para representar el inventario del jugador.
/// En el método OnValidate(), se realiza una validación cuando se realiza un cambio en el Inspector. Si el objeto itemsParent está asignado, se obtienen las ranuras de objetos dentro del contenedor de objetos utilizando GetComponentsInChildren. 
/// Esto permite vincular las ranuras de objetos en el contenedor a las ranuras de objetos en el script. Además, si la aplicación no se está ejecutando, se establecen los elementos iniciales llamando al método SetStartingItems().
/// En el método Awake(), se llama al método base Awake() de la clase itemContainer y luego se llama a SetStartingItems(). Esto se hace para asegurarse de que los elementos iniciales se establezcan correctamente al inicio.
/// El método SetStartingItems() se encarga de establecer los elementos iniciales en el inventario. Primero, se limpia el inventario llamando al método Clear(). Luego, se itera a través de los elementos iniciales proporcionados en startingItems. 
/// Para cada elemento, se agrega una copia al inventario llamando al método AddItem() con item.GetCopy(), lo que asegura que se agregue una copia del elemento en lugar de una referencia al mismo.
/// En resumen, este script de inventario permite establecer elementos iniciales en el inventario, ya sea desde el Editor o en tiempo de ejecución. 
/// También se asegura de que las ranuras de objetos estén correctamente vinculadas al contenedor de objetos.
/// </summary>
public class Inventory : ItemContainer
{
    [SerializeField] protected Item[] startingItems;
    [SerializeField] protected Transform itemsParent;

    protected override void OnValidate()
    {
        // Obtener las ranuras de objetos en el contenedor de objetos si el padre de los objetos está asignado
        if (itemsParent != null)
            itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);

        // Establecer los elementos iniciales del inventario solo en el Editor, no en tiempo de ejecución
        if (!Application.isPlaying)
        {
            SetStartingItems();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SetStartingItems();
    }

    private void SetStartingItems()
    {
        // Limpiar el inventario antes de agregar los elementos iniciales
        Clear();
        foreach (Item item in startingItems)
        {
            // Agregar una copia de cada elemento inicial al inventario
            AddItem(item.GetCopy());
        }
    }
}