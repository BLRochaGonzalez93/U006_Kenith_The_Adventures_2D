using UnityEngine;

/// <summary>
/// En este código, la clase InventoryInput maneja la entrada del jugador para alternar la visibilidad de los paneles del personaje y el inventario.
/// _characterPanelGameObject y _equipmentPanelGameObject son referencias a los objetos de los paneles del personaje y del equipo en el juego, respectivamente.
/// _toggleCharacterPanelKeys es un arreglo de teclas que se utilizan para alternar la visibilidad del panel del personaje.
/// _toggleInventoryKeys es un arreglo de teclas que se utilizan para alternar la visibilidad del inventario.
/// _toggleBothKeys es un arreglo de teclas que se utilizan para alternar la visibilidad de ambos paneles.
/// El método Update se llama en cada fotograma y verifica si se presionan las teclas correspondientes para alternar la visibilidad de los paneles.
/// Los métodos ToggleCharacterPanel, ToggleInventory y ToggleBoth verifican si se presionó alguna de las teclas asignadas y, en caso afirmativo, activan o desactivan los paneles correspondientes según su estado actual.
/// </summary>
public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject _characterPanelGameObject; // Referencia al objeto del panel del personaje en el juego
    [SerializeField] GameObject _equipmentPanelGameObject; // Referencia al objeto del panel del equipo en el juego
    [SerializeField] KeyCode[] _toggleCharacterPanelKeys; // Teclas para alternar la visibilidad del panel del personaje
    [SerializeField] KeyCode[] _toggleInventoryKeys; // Teclas para alternar la visibilidad del inventario
    [SerializeField] KeyCode[] _toggleBothKeys; // Teclas para alternar la visibilidad de ambos paneles

    void Update()
    {
        ToggleCharacterPanel();
        ToggleInventory();
        ToggleBoth();
    }

    private void ToggleCharacterPanel()
    {
        for (int i = 0; i < _toggleCharacterPanelKeys.Length; i++)
        {
            if (Input.GetKeyDown(_toggleCharacterPanelKeys[i]))
            {
                _characterPanelGameObject.SetActive(!_characterPanelGameObject.activeSelf); // Activa o desactiva el panel del personaje según su estado actual
                break;
            }
        }
    }

    private void ToggleInventory()
    {
        for (int i = 0; i < _toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(_toggleInventoryKeys[i]))
            {
                _equipmentPanelGameObject.SetActive(!_equipmentPanelGameObject.activeSelf); // Activa o desactiva el panel del equipo según su estado actual
                break;
            }
        }
    }

    public void ToggleBoth()
    {
        for (int i = 0; i < _toggleBothKeys.Length; i++)
        {
            if (Input.GetKeyDown(_toggleBothKeys[i]))
            {
                _equipmentPanelGameObject.SetActive(!_equipmentPanelGameObject.activeSelf); // Activa o desactiva el panel del equipo según su estado actual
                _characterPanelGameObject.SetActive(_equipmentPanelGameObject.activeSelf); // Activa o desactiva el panel del personaje según el estado actual del panel del equipo
                break;
            }
        }
    }
}