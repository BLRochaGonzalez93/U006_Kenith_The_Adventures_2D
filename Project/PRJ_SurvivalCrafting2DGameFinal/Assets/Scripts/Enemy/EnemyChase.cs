using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject target;               // Objeto jugador como objetivo del enemigo
    public float speed = 0.8f;               // Velocidad de movimiento del enemigo
    public GameObject bulletPrefab;         // Prefabricado de la bala
    public CircleCollider2D radius;         // Radio de detección del jugador
    public Transform shootPoint;            // Punto de disparo del enemigo
    public bool inside;                     // Indica si el jugador está dentro del radio de detección
    public bool isMelee;                    // Indica si el enemigo es de ataque cuerpo a cuerpo
    public bool isRanged;                   // Indica si el enemigo es de ataque a distancia
    public bool isBullet;                   // Indica si el enemigo utiliza balas
    public int bullets;                     // Cantidad de balas del enemigo
    public float timer;                     // Temporizador para controlar el tiempo de disparo
    public float timerDmg;                  // Temporizador para controlar el daño periódico
    public Animator anim;                   // Referencia al componente Animator del enemigo

    private void Awake()
    {
        target = GameObject.Find("Player"); // Encuentra el objeto jugador y lo establece como objetivo
    }

    public void OnTriggerEnter2D(Collider2D Radius)
    {
        if (Radius.gameObject.CompareTag("Player"))  // Comprueba si el jugador entra en el radio de detección
        {
            inside = true;
        }
    }

    public void OnTriggerExit2D(Collider2D Radius)
    {
        if (Radius.gameObject.CompareTag("Player"))  // Comprueba si el jugador sale del radio de detección
        {
            inside = false;
        }
    }

    public void BulletReset()
    {
        bullets--;  // Reduce la cantidad de balas del enemigo
    }

    void Update()
    {
        anim.SetBool("Walking", inside);  // Establece el parámetro "Walking" del Animator según si el jugador está dentro del radio de detección

        if (isMelee)
        {
            if (inside)
            {
                // Mueve el enemigo hacia la posición del jugador a una velocidad constante
                transform.position = Vector2.MoveTowards(transform.position, target.GetComponent<Transform>().position, speed * Time.deltaTime);

                // Voltea la imagen del enemigo según la dirección hacia el jugador
                if (target.GetComponent<Transform>().position.x - transform.position.x >= 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }

        if (isRanged)
        {
            timer += Time.deltaTime;
            if (inside)
            {
                if (bullets < 1 && timer > 4)
                {
                    anim.Play("Attack");  // Reproduce la animación de ataque
                    GameObject Projectile = Instantiate(bulletPrefab, shootPoint);  // Instancia una bala
                    Projectile.SetActive(true);
                    bullets++;
                    timer = 0;
                }
            }
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))  // Comprueba si colisiona con el jugador
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(5);
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        timerDmg += Time.deltaTime;
        if (other.gameObject.CompareTag("Player"))  // Comprueba si está en contacto continuo con el jugador
        {
            timerDmg += Time.deltaTime;
            if (timerDmg >= 1)
            {
                // Inflige daño periódico al jugador después de transcurrido un tiempo determinado
                other.gameObject.GetComponent<PlayerMovement>().TakeDamage(2);
                timerDmg = 0;
            }
        }
    }
}