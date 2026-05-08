using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// El código anterior define la clase ItemSlot, que hereda de BaseItemSlot e implementa las interfaces IBeginDragHandler, IEndDragHandler, IDragHandler e IDropHandler. 
/// Esta clase se utiliza para representar una ranura de objeto en una interfaz de usuario.
/// Comentarios:
/// OnBeginDragEvent: Evento que se activa cuando comienza el arrastre de la ranura del objeto.
/// OnEndDragEvent: Evento que se activa cuando finaliza el arrastre de la ranura del objeto.
/// OnDragEvent: Evento que se activa durante el arrastre de la ranura del objeto.
/// OnDropEvent: Evento que se activa al soltar un objeto sobre la ranura.
/// Métodos:
/// CanAddStack(item item, int amount = 1): Sobrescribe el método CanAddStack de la clase base para verificar si se puede agregar una pila adicional del objeto en la ranura. Verifica tanto el límite de apilamiento como la cantidad actual en la ranura.
/// CanReceiveItem(item item): Sobrescribe el método CanReceiveItem de la clase base para indicar que la ranura puede recibir cualquier objeto, devolviendo siempre true.
/// OnDisable(): Sobrescribe el método OnDisable de la clase base y realiza acciones adicionales al desactivar la ranura del objeto. Si la ranura estaba siendo arrastrada (isDragging es true), se llama al método OnEndDrag para finalizar el arrastre.
/// Métodos de las interfaces:
/// OnBeginDrag(PointerEventData eventData): Implementa el método de la interfaz IBeginDragHandler y se activa cuando comienza el arrastre de la ranura del objeto. Cambia el color de la imagen de la ranura para indicar que está siendo arrastrada y dispara el evento OnBeginDragEvent.
/// OnEndDrag(PointerEventData eventData): Implementa el método de la interfaz IEndDragHandler y se activa cuando finaliza el arrastre de la ranura del objeto. Restaura el color normal de la imagen de la ranura y dispara el evento OnEndDragEvent.
/// OnDrag(PointerEventData eventData): Implementa el método de la interfaz IDragHandler y se activa durante el arrastre de la ranura del objeto. Dispara el evento OnDragEvent.
/// OnDrop(PointerEventData eventData): Implementa el método de la interfaz IDropHandler y se activa cuando se suelta un objeto sobre la ranura. Dispara el evento OnDropEvent.
/// La clase ItemSlot es utilizada para gestionar las interacciones con las ranuras de objetos en una interfaz de usuario, permitiendo el arrastre, soltado y eventos relacionados con dichas ranuras
/// </summary>
public class ItemSlot : BaseItemSlot, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    // Eventos que se activan durante diferentes etapas del arrastre y soltado del objeto en la ranura.
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    private bool isDragging;
    private Color _dragColor = new(1, 1, 1, 0.5f);

    public override bool CanAddStack(Item item, int amount = 1)
    {
        // Verifica si se puede agregar una pila adicional del objeto en la ranura,
        // considerando el límite de apilamiento y la cantidad actual en la ranura.
        return base.CanAddStack(item, amount) && Amount + amount <= item.maximumStacks;
    }

    public override bool CanReceiveItem(Item item)
    {
        // Indica que la ranura puede recibir cualquier objeto.
        return true;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (isDragging)
        {
            // Finaliza el arrastre si la ranura se desactiva mientras se está arrastrando.
            OnEndDrag(null);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        if (Item != null)
            image.color = _dragColor;

        // Dispara el evento de inicio de arrastre.
        OnBeginDragEvent?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (Item != null)
            image.color = normalColor;

        // Dispara el evento de finalización de arrastre.
        OnEndDragEvent?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Dispara el evento de arrastre.
        OnDragEvent?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Dispara el evento de soltado de objeto sobre la ranura.
        OnDropEvent?.Invoke(this);
    }
}