using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered;  // Evento Unity que se activa cuando se dispara un evento de animación
    public UnityEvent OnAttackPerformed;          // Evento Unity que se activa cuando se realiza un ataque

    /// <summary>
    /// Activa el evento OnAnimationEventTriggered.
    /// Este método se llama desde una animación para indicar que se ha disparado un evento.
    /// </summary>
    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();  // Invoca el evento OnAnimationEventTriggered, si hay suscriptores
    }

    /// <summary>
    /// Activa el evento OnAttackPerformed.
    /// Este método se llama desde una animación para indicar que se ha realizado un ataque.
    /// </summary>
    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();  // Invoca el evento OnAttackPerformed, si hay suscriptores
    }
}