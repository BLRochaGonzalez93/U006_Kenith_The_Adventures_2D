using UnityEngine;

/// <summary>
/// Este script gestiona la experiencia y el nivel de un personaje. 
/// Tiene variables para almacenar el nivel actual, la experiencia actual, la experiencia requerida para subir de nivel, el nivel máximo permitido y una referencia a un script de interfaz de usuario para mostrar la experiencia en la interfaz. 
/// El script incluye métodos para recibir experiencia, calcular la experiencia requerida para el próximo nivel y subir de nivel. 
/// Cuando se recibe experiencia, se añade a la experiencia actual y se comprueba si se ha alcanzado o superado la experiencia requerida para subir de nivel. 
/// En ese caso, se llama al método LvlUp() para subir de nivel, reiniciar la experiencia actual y calcular la nueva experiencia requerida
/// </summary>
public class XP : MonoBehaviour
{
    public int actualLvl;                  // Nivel actual del personaje
    public int actualXP;                   // Experiencia actual del personaje
    public int reqXP = 100;                // Experiencia requerida para subir de nivel
    public int previousXP;                 // Experiencia requerida previa para subir de nivel
    public int maxLvl;                     // Nivel máximo permitido
    public LifeStaExpBarsUI lseUI;         // Referencia al script de la interfaz de usuario para mostrar la experiencia
    public AudioSource lvlUpSource;

    public void Start()
    {
        maxLvl = 25;                      // Establecer el nivel máximo permitido
        actualLvl = 0;                    // Inicializar el nivel actual en 0
    }

    public void GetXP(int quantity)
    {
        actualXP += quantity;             // Añadir la cantidad de experiencia recibida
        lseUI.SetExperience();             // Actualizar la interfaz de usuario para mostrar la experiencia

        if (actualXP >= reqXP)
        {
            LvlUp();                      // Si la experiencia actual es igual o superior a la requerida, subir de nivel
        }
    }

    public void XpNeeded()
    {
        reqXP = Mathf.RoundToInt(previousXP * 1.2f);   // Calcular la experiencia requerida para el próximo nivel (un 20% más que la anterior)
    }

    public void LvlUp()
    {
        actualXP = 0;                    // Reiniciar la experiencia actual a 0
        actualLvl++;                     // Incrementar el nivel actual en 1
        GetComponent<Character>().LevelUp();         // Llamar al método LevelUp() del componente Character asociado al personaje
        previousXP = reqXP;              // Guardar la experiencia requerida anterior para futuros cálculos
        XpNeeded();                      // Calcular la experiencia requerida para el próximo nivel
        lvlUpSource.Play();
    }
}