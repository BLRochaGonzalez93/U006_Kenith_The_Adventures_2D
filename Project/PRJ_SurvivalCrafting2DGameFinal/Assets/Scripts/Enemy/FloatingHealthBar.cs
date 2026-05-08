using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Este script se encarga de actualizar una barra de salud flotante que muestra la cantidad actual de vida en relación con la vida máxima. 
/// El deslizador (`Slider`) se utiliza para representar visualmente la barra de salud. El método `UpdateHealthBar` se utiliza para actualizar la barra de salud con los valores actuales de vida y vida máxima. 
/// Además, el lienzo que contiene la barra de salud se activa o desactiva según si la vida actual es igual o diferente a la vida máxima.
/// </summary>

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        // Desactiva inicialmente el lienzo que contiene la barra de salud
        _slider.GetComponentInParent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Actualiza la barra de salud con los valores actuales de vida y vida máxima.
    /// </summary>
    /// <param name="currentLife">Valor actual de vida.</param>
    /// <param name="maxLife">Valor máximo de vida.</param>
    public void UpdateHealthBar(float currentLife, float maxLife)
    {
        // Si la vida actual no es igual a la vida máxima, se activa el lienzo que contiene la barra de salud
        if (currentLife != maxLife)
        {
            _slider.GetComponentInParent<Canvas>().enabled = true;
        }

        // Calcula el valor de la barra de salud y lo asigna al deslizador
        _slider.value = currentLife / maxLife;
    }
}