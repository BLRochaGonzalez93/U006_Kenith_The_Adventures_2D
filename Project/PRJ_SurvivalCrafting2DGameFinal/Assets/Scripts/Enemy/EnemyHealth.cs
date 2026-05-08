using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Este script maneja la salud y el comportamiento de un enemigo. Permite recibir golpes de ataques y realizar acciones cuando la salud llega a cero, como soltar recompensas. 
/// También activa efectos visuales de parpadeo al recibir golpes. Los eventos `OnHitWithReference` y `OnDeathWithReference` se activan cuando el enemigo recibe un golpe y muere respectivamente, 
/// proporcionando una referencia al objeto que realizó el ataque.
/// </summary>

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference;  // Evento que se activa al recibir un golpe con referencia al atacante
    public UnityEvent<GameObject> OnDeathWithReference;  // Evento que se activa al morir con referencia al atacante

    [SerializeField] private bool _isDead = false;

    public ItemContainer itemContainer;  // Contenedor de objetos
    public Item item;  // Objeto a soltar
    public Item secondRandopDrop;  // Segundo objeto aleatorio a soltar
    [SerializeField] GameObject _equippedItem;  // Objeto equipado (opcional)

    [SerializeField] FloatingHealthBar _healthBar;  // Barra de vida flotante
    [SerializeField] private GameObject _flickShape;  // Efecto visual de parpadeo

    public GameObject target;  // Objetivo a atacar

    public AudioSource ouchs;
    private void Awake()
    {
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
        target = GameObject.Find("Player");
    }

    private void Start()
    {
        _healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    /// <summary>
    /// Inicializa la salud del enemigo con un valor específico.
    /// </summary>
    /// <param name="healthValue">Valor de salud inicial</param>
    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        _isDead = false;
    }

    /// <summary>
    /// Recibe un golpe con una cantidad específica de daño de un atacante.
    /// </summary>
    /// <param name="amount">Cantidad de daño</param>
    /// <param name="sender">Objeto que realiza el ataque</param>
    public void GetHit(int amount, GameObject sender)
    {
        if (_isDead)
        {
            return;
        }
        if (sender.layer == gameObject.layer)
        {
            return;
        }

        // Voltea la orientación del enemigo según la posición del objetivo
        if (target.GetComponent<Transform>().position.x - transform.position.x >= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
      
        Invoke(nameof(EnableFlick), 0f);  // Activa el efecto de parpadeo
        Invoke(nameof(DisableFlick), 0.25f);  // Desactiva el efecto de parpadeo
        currentHealth -= amount;
        _healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log("Ouchs!!");
        ouchs.Play();
        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);  // Activa el evento de recibir golpe con referencia al atacante
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);  // Activa el evento de muerte con referencia al atacante
            _isDead = true;
            DropReward();  // Suelta la recompensa
            GetComponent<EnemyChase>().inside = false;
            GetComponent<EnemyChase>().anim.Play("Death");
            Invoke(nameof(EnemyDeath), 0.75f);  // Invoca la destrucción del enemigo después de un tiempo
        }
    }

    /// <summary>
    /// Destruye el objeto del enemigo.
    /// </summary>
    private void EnemyDeath()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Suelta la recompensa al morir el enemigo.
    /// </summary>
    public void DropReward()
    {
        itemContainer.AddRandomDropFromEnemies(item, secondRandopDrop);
    }

    /// <summary>
    /// Activa el efecto visual de parpadeo.
    /// </summary>
    private void EnableFlick()
    {

         _flickShape.GetComponent<SpriteRenderer>().flipX = gameObject.GetComponent<SpriteRenderer>().flipX;
         _flickShape.SetActive(true);
    }

    /// <summary>
    /// Desactiva el efecto visual de parpadeo.
    /// </summary>
    private void DisableFlick()
    {

        _flickShape.SetActive(false);
    }
}