using UnityEngine;

public class LifeStaExpSystem
{
    public int healthAmount;                // Cantidad actual de salud
    public int healthAmountMax;             // Cantidad máxima de salud
    public float energyAmount;              // Cantidad actual de energía
    public float energyAmountMax;           // Cantidad máxima de energía
    public int experienceAmount;            // Cantidad actual de experiencia
    public int experienceAmountMax;         // Cantidad máxima de experiencia

    /// <summary>
    /// Constructor de la clase LifeStaExpSystem.
    /// Inicializa las propiedades de cantidad de vida, energía y experiencia obteniendo los valores actuales de un objeto jugador.
    /// </summary>
    public LifeStaExpSystem()
    {
        // Obtener los valores actuales de vida, energía y experiencia del objeto jugador
        healthAmount = GameObject.Find("Player").GetComponent<Character>().currentHealth;
        healthAmountMax = GameObject.Find("Player").GetComponent<Character>().startHealth;
        energyAmount = GameObject.Find("Player").GetComponent<PlayerMovement>().timer;
        energyAmountMax = GameObject.Find("Player").GetComponent<PlayerMovement>().dashCD;
        experienceAmount = GameObject.Find("Player").GetComponent<XP>().actualXP;
        experienceAmountMax = GameObject.Find("Player").GetComponent<XP>().reqXP;
    }

    /// <summary>
    /// Obtiene la cantidad normalizada de salud, en un rango de 0 a 1.
    /// </summary>
    /// <returns>La cantidad normalizada de salud.</returns>
    public float GetHealthNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    /// <summary>
    /// Obtiene la cantidad normalizada de experiencia, en un rango de 0 a 1.
    /// </summary>
    /// <returns>La cantidad normalizada de experiencia.</returns>
    public float GetExperienceNormalized()
    {
        return (float)experienceAmount / experienceAmountMax;
    }

    /// <summary>
    /// Obtiene la cantidad normalizada de energía, en un rango de 0 a 1.
    /// </summary>
    /// <returns>La cantidad normalizada de energía.</returns>
    public float GetEnergyNormalized()
    {
        return (float)energyAmount / energyAmountMax;
    }
}