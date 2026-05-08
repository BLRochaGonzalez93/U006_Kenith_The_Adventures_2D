using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// El código define una clase llamada DropItemArea que representa un área donde se pueden soltar objetos.
/// Implementa la interfaz IDropHandler de EventSystem para recibir eventos de soltar.
/// El evento OnDropEvent es un evento que puede ser suscrito por otros componentes para realizar acciones cuando se suelta un objeto en el área de soltar.
/// El método OnDrop es llamado cuando se suelta un objeto en el área de soltar. Invoca el evento OnDropEvent, notificando a los componentes suscritos que se ha soltado un objeto en el área.
/// </summary>

public class DropItemArea : MonoBehaviour, IDropHandler
{
    public event Action OnDropEvent;

    /// <summary>
    /// Maneja el evento de soltar un objeto en el área de soltar.
    /// Invoca el evento OnDropEvent.
    /// </summary>
    /// <param name="eventData">Información sobre el evento de soltar.</param>
    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke();
    }
}