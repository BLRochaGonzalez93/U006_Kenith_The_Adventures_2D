using UnityEngine;

/// <summary>
/// Este script realiza un raycast desde la posición del objeto hacia adelante. 
/// Si el raycast colisiona con algún objeto, se dibuja un rayo rojo en el editor hasta la distancia de colisión y luego se destruye el objeto. 
/// Si el raycast no colisiona con ningún objeto, se dibuja un rayo azul en el editor con una distancia de 10 unidades. 
/// El script utiliza el método Physics.Raycast para realizar el raycast y los métodos Debug.DrawRay para visualizar los rayos en el editor
/// </summary>

public class RayCastController : MonoBehaviour
{
    private void FixedUpdate()
    {
        // Realiza un raycast hacia adelante desde la posición del objeto
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
        {
            // Dibuja un rayo rojo en el editor para visualizar el raycast y su distancia
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            // Destruye el objeto al colisionar con algo
            Destroy(gameObject);
        }
        else
        {
            // Dibuja un rayo azul en el editor para visualizar el raycast con una distancia de 10 unidades
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.blue);
        }
    }
}