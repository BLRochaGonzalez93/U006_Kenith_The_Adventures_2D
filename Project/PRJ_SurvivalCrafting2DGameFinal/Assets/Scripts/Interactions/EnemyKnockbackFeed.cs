using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// La clase EnemyKnockbackFeed controla el efecto de retroceso de un enemigo cuando es golpeado. El script contiene un Rigidbody2D para aplicar fuerza al enemigo y dos eventos, 
/// OnBegin y OnDone, que pueden ser asignados desde el inspector y se invocan al comienzo y al finalizar el retroceso, respectivamente.
/// El m�todo PlayFeedback reproduce el efecto de retroceso en el enemigo. Toma como par�metro el objeto que env�a el golpe y aplica una fuerza en la direcci�n opuesta al vector entre el enemigo y el objeto golpeador. 
/// Luego, inicia una corrutina llamada Reset para restablecer la velocidad del Rigidbody2D despu�s de un retraso.
/// La corrutina Reset espera durante el tiempo especificado por _delay y luego establece la velocidad del Rigidbody2D en cero. Finalmente, invoca el evento OnDone si est� asignado.
/// Espero que esta documentaci�n aclare el prop�sito y el funcionamiento del c�digo. Si tienes m�s preguntas, no dudes en hacerlas.
/// </summary>
public class EnemyKnockbackFeed : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidB2D;
    [SerializeField] private float _pushStr = 10, _delay = 0.2f;

    public UnityEvent OnBegin; // Evento invocado al comenzar el retroceso.
    public UnityEvent OnDone; // Evento invocado al finalizar el retroceso.

    /// <summary>
    /// Reproduce el efecto de retroceso en el enemigo.
    /// </summary>
    /// <param name="sender">El objeto que env�a el golpe.</param>
    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke(); // Invoca el evento OnBegin si est� asignado.
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        _rigidB2D.AddForce(direction * _pushStr, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(_delay);
        _rigidB2D.linearVelocity = Vector3.zero;
        OnDone?.Invoke(); // Invoca el evento OnDone si est� asignado.
    }
}