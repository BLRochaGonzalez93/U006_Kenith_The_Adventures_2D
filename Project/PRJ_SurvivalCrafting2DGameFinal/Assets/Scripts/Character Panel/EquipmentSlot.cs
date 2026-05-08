/// <summary>
/// En este código, la clase EquipmentSlot hereda de ItemSlot y agrega funcionalidad específica para un espacio de equipo.
/// equipmentType es una variable pública que indica el tipo de equipo asociado a este espacio.
/// El método OnValidate se utiliza para actualizar el nombre del objeto en el editor, combinando el tipo de equipo y la palabra "Slot".
/// El método CanReceiveItem verifica si un objeto puede ser recibido en este espacio. Retorna verdadero si el objeto es un EquippableItem (objeto equipable) y su equipmentType coincide con el equipmentType de este espacio.
/// </summary>
public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType; // Tipo de equipo asociado a este espacio

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot"; // Establece el nombre del objeto como el tipo de equipo seguido de "Slot"
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        EquippableItem equippableItem = item as EquippableItem; // Intenta convertir el objeto en un EquippableItem
        return equippableItem != null && equippableItem.equipmentType == equipmentType; // Retorna verdadero si el objeto es un EquippableItem y su equipmentType coincide con el equipmentType de este espacio
    }
}