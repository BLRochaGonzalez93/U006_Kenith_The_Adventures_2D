using System.Collections.Generic;
using UnityEngine;

public class ActivateBench : MonoBehaviour
{
    public Collider2D interactArea;         // Área de interacción del banco
    public bool canInteract = false;        // Indica si se puede interactuar con el banco
    public GameObject activable;           // Objeto activable
    public Canvas canvas;                   // Objeto de lienzo
    public GameObject cartel;               // Objeto de _cartel

    public List<ItemAmount> materials;      // Lista de materiales necesarios para activar el banco
    public ItemContainer itemContainer;     // Contenedor de objetos

    /// <summary>
    /// Método llamado cuando se produce una colisión en el área de activación del banco.
    /// Activa la interacción y muestra el lienzo.
    /// </summary>
    /// <param name="other">Collider2D que colisionó con el área de activación.</param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        else
        {
            canInteract = true;
            canvas.enabled = true;
        }
    }

    /// <summary>
    /// Método llamado cuando un objeto sale de la zona de activación del banco.
    /// Desactiva la interacción y oculta el lienzo.
    /// </summary>
    /// <param name="other">Collider2D que salió de la zona de activación.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        else
        {
            canInteract = false;
            canvas.enabled = false;
        }
    }

    /// <summary>
    /// Activa el objeto activable y destruye el _cartel.
    /// </summary>
    public void Activate()
    {
        activable.SetActive(true);
        Destroy(cartel);
    }
}