using UnityEngine;

/// <summary>
/// La clase Functions define un conjunto de métodos utilizados para controlar el flujo del juego y la interacción con los menús.
/// Los campos públicos baseMenu, _pauseMenu, tutorialPanel y controlsPanel son objetos GameObject que representan los distintos elementos del juego.
/// El método GoToGame cambia al modo de juego y oculta el menú base.
/// El método ExitGame sale del juego.
/// El método GoToMenu muestra el menú base y oculta el menú de pausa.
/// El método ResumeGame reanuda el juego y oculta el menú de pausa.
/// El método GoTutorial muestra el panel de tutorial.
/// El método CloseTutorial cierra el panel de tutorial.
/// El método CloseControls cierra el panel de controles.
/// El método GoControls muestra el panel de controles
/// </summary>

public class Functions : MonoBehaviour
{
    public GameObject baseMenu;
    public GameObject pauseMenu;
    public GameObject tutorialPanel;
    public GameObject controlsPanel;
    public AudioSource botonMenu;

    /// <summary>
    /// Cambia al modo de juego y oculta el menú base.
    /// </summary>
    public void GoToGame()
    {
        botonMenu.Play();
        Time.timeScale = 1;
        baseMenu.SetActive(false);
    }

    /// <summary>
    /// Sale del juego.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Muestra el menú base y oculta el menú de pausa.
    /// </summary>
    public void GoToMenu()
    {
        botonMenu.Play();
        baseMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Reanuda el juego y oculta el menú de pausa.
    /// </summary>
    public void ResumeGame()
    {
        botonMenu.Play();
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Muestra el panel de tutorial.
    /// </summary>
    public void GoTutorial()
    {
        botonMenu.Play();
        tutorialPanel.SetActive(true);
    }

    /// <summary>
    /// Cierra el panel de tutorial.
    /// </summary>
    public void CloseTutorial()
    {
        botonMenu.Play();
        tutorialPanel.SetActive(false);
    }

    /// <summary>
    /// Cierra el panel de controles.
    /// </summary>
    public void CloseControls()
    {
        botonMenu.Play();
        controlsPanel.SetActive(false);
    }

    /// <summary>
    /// Muestra el panel de controles.
    /// </summary>
    public void GoControls()
    {
        botonMenu.Play();
        controlsPanel.SetActive(true);
    }
}
