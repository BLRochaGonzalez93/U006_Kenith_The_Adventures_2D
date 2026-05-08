using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Animator animator;
    public float delay = 1f;
    private bool _attackDelay = false;

    public bool IsAttacking { get; private set; }

    public Transform circleOrigin;
    public float radius;

    public GameObject str;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Update()
    {
        // Si el personaje está atacando, se sale de la función
        if (IsAttacking)
        {
            return;
        }

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

    public void Attack()
    {
        // Si hay un retraso de ataque, se sale de la función
        if (_attackDelay)
        {
            return;
        }
        else
        {
            Debug.Log("Animator Attack");
            // Activa la animación de ataque en el animador
            animator.SetTrigger("Attack");
            IsAttacking = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja una esfera en el editor para representar el área de detección de colisiones
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        // Detecta colisiones con los objetos dentro del área de detección
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            Debug.Log(collider.name);

            // Obtiene el componente EnemyHealth del objeto colisionado
            EnemyHealth enemyhealth;
            if (enemyhealth = collider.GetComponent<EnemyHealth>())
            {
                Debug.Log(transform.gameObject);
                Debug.Log(transform.parent.gameObject);
                Debug.Log(transform.parent.parent.gameObject);
                // Aplica daño al enemigo con un valor aleatorio basado en la fuerza del personaje
                enemyhealth.GetHit(Random.Range(Mathf.RoundToInt(str.GetComponent<StatDisplay>().Stat.Value / 1.2f), Mathf.RoundToInt(str.GetComponent<StatDisplay>().Stat.Value * 1.5f)), transform.parent.parent.gameObject);
                Debug.Log(enemyhealth.currentHealth);
            }
        }
    }
}
