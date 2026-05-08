using kenith.CharacterStats;
using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves,
    Boots,
    Weapon1,
    Weapon2,
    Accessory1,
    Accessory2,
}

[CreateAssetMenu(menuName = "Items/Equippable item")]
public class EquippableItem : Item
{
    public int strengthBonus;                // Bonificación de fuerza
    public int agilityBonus;                 // Bonificación de agilidad
    public int intelligenceBonus;            // Bonificación de inteligencia
    public int vitalityBonus;                // Bonificación de vitalidad
    [Space]
    public float strengthPercentBonus;       // Bonificación porcentual de fuerza
    public float agilityPercentBonus;        // Bonificación porcentual de agilidad
    public float intelligencePercentBonus;   // Bonificación porcentual de inteligencia
    public float vitalityPercentBonus;       // Bonificación porcentual de vitalidad
    [Space]
    public EquipmentType equipmentType;      // Tipo de equipo

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

    /// <summary>
    /// Equips the item to the specified character.
    /// </summary>
    /// <param name="c">The character to equip the item to.</param>
    public void Equip(Character c)
    {
        if (strengthBonus != 0)
            c.strength.AddModifier(new StatModifier(strengthBonus, StatModType.Flat, this));
        if (agilityBonus != 0)
            c.agility.AddModifier(new StatModifier(agilityBonus, StatModType.Flat, this));
        if (vitalityBonus != 0)
            c.vitality.AddModifier(new StatModifier(vitalityBonus, StatModType.Flat, this));

        if (strengthPercentBonus != 0)
            c.strength.AddModifier(new StatModifier(strengthPercentBonus, StatModType.PercentMult, this));
        if (agilityPercentBonus != 0)
            c.agility.AddModifier(new StatModifier(agilityPercentBonus, StatModType.PercentMult, this));
        if (vitalityPercentBonus != 0)
            c.vitality.AddModifier(new StatModifier(vitalityPercentBonus, StatModType.PercentMult, this));
    }

    /// <summary>
    /// The Unequip method is called to remove the stat modifiers applied by this equippable item from the character. 
    /// It removes all modifiers associated with this item's source from the character's strength, agility, and vitality _stats.
    /// </summary>
    /// <param name="c"></param>
    public void Unequip(Character c)
    {
        c.strength.RemoveAllModifiersFromSource(this);
        c.agility.RemoveAllModifiersFromSource(this);
        c.vitality.RemoveAllModifiersFromSource(this);
    }

    /// <summary>
    /// The GetItemType method overrides the base class method to return the string representation of the equipmentType. 
    /// It is used to get the type of the equippable item
    /// </summary>
    /// <returns>the string representation of the equipmentType</returns>
    public override string GetItemType()
    {
        return equipmentType.ToString();
    }

    /// <summary>
    /// The GetDescription method is used to retrieve the description of the equippable item. 
    /// It constructs the description by adding the bonuses and their corresponding stat names to a StringBuilder (_sb). 
    /// Each bonus is appended to a new line in the _sb, and positive bonuses are prefixed with a "+" sign. 
    /// The bonuses are displayed as either flat values or percentages based on the isPercent flag.
    /// </summary>
    /// <returns>the description of the equippable item</returns>
    public override string GetDescription()
    {
        sb.Length = 0;
        AddStat(strengthBonus, "strength");
        AddStat(agilityBonus, "agility");
        AddStat(intelligenceBonus, "intelligence");
        AddStat(vitalityBonus, "vitality");

        AddStat(strengthPercentBonus, "strength", isPercent: true);
        AddStat(agilityPercentBonus, "agility", isPercent: true);
        AddStat(intelligencePercentBonus, "intelligence", isPercent: true);
        AddStat(vitalityPercentBonus, "vitality", isPercent: true);

        return sb.ToString();
    }

    /// <summary>
    /// The AddStat method is a helper function used by GetDescription. 
    /// It adds a stat bonus to the StringBuilder _sb if the value is non-zero. 
    /// It formats the bonus value as a percentage if isPercent is true and appends the stat name to the _sb. Positive bonuses are prefixed with a "+" sign
    /// </summary>
    /// <param name="value"></param>
    /// <param name="statName"></param>
    /// <param name="isPercent"></param>
    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
