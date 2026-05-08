using kenith.CharacterStats;
using UnityEngine;

/// <summary>
/// El script StatPanel se utiliza para mostrar un panel de estadísticas en la interfaz de usuario. 
/// Tiene un arreglo de StatDisplay para mostrar las estadísticas y un arreglo de string para almacenar los nombres de las estadísticas. 
/// El método SetStats se utiliza para establecer las estadísticas que se mostrarán en el panel. 
/// El método UpdateStatValues se utiliza para actualizar los valores de las estadísticas en el panel. 
/// El método UpdateStatNames se utiliza para actualizar los nombres de las estadísticas en el panel.
/// </summary>
public class StatPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] _statDisplays;   // Arreglo de StatDisplay para mostrar las estadísticas
    [SerializeField] string[] _statNames;           // Arreglo de nombres de estadísticas

    private CharacterStat[] _stats;                 // Arreglo de CharacterStat para almacenar las estadísticas

    private void OnValidate()
    {
        _statDisplays = GetComponentsInChildren<StatDisplay>();    // Obtiene los componentes StatDisplay en los hijos del objeto
        UpdateStatNames();
    }

    public void SetStats(params CharacterStat[] charStats)
    {
        _stats = charStats;

        if (_stats.Length > _statDisplays.Length)
        {
            Debug.LogError("Not Enough Stat Displays!");    // Muestra un mensaje de error si no hay suficientes StatDisplay para todas las estadísticas
            return;
        }

        for (int i = 0; i < _statDisplays.Length; i++)
        {
            _statDisplays[i].gameObject.SetActive(i < _stats.Length);   // Activa o desactiva los objetos StatDisplay según la cantidad de estadísticas

            if (i < _stats.Length)
            {
                _statDisplays[i].Stat = _stats[i];   // Asigna la estadística correspondiente a cada StatDisplay
            }
        }
    }

    public void UpdateStatValues(int StatLevel)
    {
        for (int i = 0; i < _stats.Length; i++)
        {
            _statDisplays[i].UpdateStatValue();    // Actualiza los valores de las estadísticas en los StatDisplay
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < _statNames.Length; i++)
        {
            _statDisplays[i].Name = _statNames[i];   // Actualiza los nombres de las estadísticas en los StatDisplay
        }
    }
}