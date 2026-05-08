using UnityEngine;

/// <summary>
/// Este script representa un efecto específico de un objeto que cura al personaje cuando se utiliza. Hereda de la clase UsableItemEffect.
/// Campos:
/// healAmount: La cantidad de salud que se restaurará cuando se use el objeto.
/// Métodos:
/// ExecuteEffect(UsableItem usableItem, Character character): Este método se llama cuando se ejecuta el efecto del objeto. 
/// Recibe el objeto UsableItem que representa el objeto que se está utilizando y el objeto Character que representa el personaje que está siendo curado. 
/// Si la salud actual del personaje más la cantidad de curación supera su salud máxima (character.startHealth), se establece la salud actual del personaje en su salud máxima. 
/// De lo contrario, se suma la cantidad de curación a la salud actual del personaje.
/// GetDescription(): Este método devuelve una descripción del efecto del objeto. En este caso, devuelve una cadena que indica la cantidad de salud que el objeto curará.
/// La clase HealItemEffect está marcada con el atributo CreateAssetMenu, lo que permite crearla como un nuevo recurso en el menú del editor de Unity. 
/// Esto permite que los diseñadores puedan crear y configurar fácilmente efectos de curación para los objetos.
/// </summary>
[CreateAssetMenu(menuName = "item Effects/Heal")]
public class HealItemEffect : UsableItemEffect
{
	public int healAmount;

	public override void ExecuteEffect(UsableItem usableItem, Character character)
	{
		if (character.currentHealth + healAmount > character.startHealth)
		{
			character.currentHealth = character.startHealth;

		}
		else
        {
			character.currentHealth += healAmount;
        }
	}

	public override string GetDescription()
	{
		return "Heals for " + healAmount + " health.";
	}
}
