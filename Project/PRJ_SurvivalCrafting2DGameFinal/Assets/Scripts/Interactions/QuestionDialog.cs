using System;
using UnityEngine;

/// <summary>
/// El código define una clase llamada `QuestionDialog` que maneja un diálogo de pregunta con opciones "Yes" (Sí) y "No".
/// Los eventos `OnYesEvent` y `OnNoEvent` son eventos que pueden ser suscritos por otros componentes para realizar acciones cuando se hace clic en los botones "Yes" o "No".
/// El método `Show` muestra el diálogo de pregunta y reinicia los eventos `OnYesEvent` y `OnNoEvent`.
/// El método `Hide` oculta el diálogo de pregunta.
/// El método `OnYesButtonClick` maneja el evento del botón "Yes". Invoca el evento `OnYesEvent` y luego oculta el diálogo.
/// El método `OnNoButtonClick` maneja el evento del botón "No". Invoca el evento `OnNoEvent` y luego oculta el diálogo.
/// </summary>

public class QuestionDialog : MonoBehaviour
{
    public event Action OnYesEvent;
    public event Action OnNoEvent;

    /// <summary>
    /// Muestra el diálogo de pregunta.
    /// </summary>
    public void Show()
    {
        this.gameObject.SetActive(true);
        OnYesEvent = null;
        OnNoEvent = null;
    }

    /// <summary>
    /// Oculta el diálogo de pregunta.
    /// </summary>
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Maneja el evento del botón "Yes" (Sí).
    /// Invoca el evento OnYesEvent y luego oculta el diálogo.
    /// </summary>
    public void OnYesButtonClick()
    {
        OnYesEvent?.Invoke();

        Hide();
    }

    /// <summary>
    /// Maneja el evento del botón "No".
    /// Invoca el evento OnNoEvent y luego oculta el diálogo.
    /// </summary>
    public void OnNoButtonClick()
    {
        OnNoEvent?.Invoke();

        Hide();
    }
}