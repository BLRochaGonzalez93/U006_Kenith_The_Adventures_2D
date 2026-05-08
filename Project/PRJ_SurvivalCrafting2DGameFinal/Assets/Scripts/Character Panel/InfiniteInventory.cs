using UnityEngine;

/// <summary>
/// Este script extiende la clase inventory y agrega la capacidad de tener un número infinito de espacios para objetos. Introduce una nueva variable llamada _maxSlots que determina el número máximo de espacios en el inventario. 
/// La propiedad MaxSlots proporciona un getter y un setter para acceder y modificar este valor.
/// En el método Awake, se llama a la función SetMaxSlots con el valor inicial de _maxSlots y luego se llama al método Awake de la clase padre.
/// La función SetMaxSlots se encarga de ajustar el número de espacios para objetos en base al valor de _maxSlots. Si el valor es menor o igual a 0, se establece _maxSlots en 1 para asegurarse de que siempre haya al menos un espacio disponible. 
/// De lo contrario, se actualiza _maxSlots con el nuevo valor.
/// Si _maxSlots es menor que el número actual de espacios para objetos (_itemSlots.Count), significa que se deben eliminar algunos espacios. Los espacios excedentes más allá de _maxSlots se destruyen y la lista _itemSlots se actualiza en consecuencia.
/// Si _maxSlots es mayor que el número actual de espacios para objetos, significa que se deben agregar nuevos espacios. La diferencia entre _maxSlots y el número actual de espacios (diff) determina cuántos nuevos espacios deben crearse. 
/// Cada nuevo espacio se crea como hijo de itemsParent y se agrega a la lista _itemSlots.
/// Al ajustar dinámicamente el número de espacios para objetos en función del valor de _maxSlots, el inventario puede admitir un número infinito de espacios.
/// </summary>
public class InfiniteInventory : Inventory
{
    [SerializeField] GameObject _itemSlotPrefab;

    [SerializeField] int _maxSlots;
    public int MaxSlots
    {
        get { return _maxSlots; }
        set { SetMaxSlots(value); }
    }

    protected override void Awake()
    {
        SetMaxSlots(_maxSlots);
        base.Awake();
    }

    private void SetMaxSlots(int value)
    {
        if (value <= 0)
        {
            _maxSlots = 1;
        }
        else
        {
            _maxSlots = value;
        }

        if (_maxSlots < itemSlots.Count)
        {
            // Remove excess item slots beyond the new _maxSlots
            for (int i = _maxSlots; i < itemSlots.Count; i++)
            {
                Destroy(itemSlots[i].transform.parent.gameObject);
            }
            int diff = itemSlots.Count - _maxSlots;
            itemSlots.RemoveRange(_maxSlots, diff);
        }
        else if (_maxSlots > itemSlots.Count)
        {
            // Add new item slots up to the new _maxSlots
            int diff = _maxSlots - itemSlots.Count;

            for (int i = 0; i < diff; i++)
            {
                GameObject gameObject = Instantiate(_itemSlotPrefab);
                gameObject.transform.SetParent(itemsParent, worldPositionStays: false);
                itemSlots.Add(gameObject.GetComponentInChildren<ItemSlot>());
            }
        }
    }
}