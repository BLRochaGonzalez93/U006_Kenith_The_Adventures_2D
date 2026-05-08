using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed = 2f;      // Velocidad a la que se mueve la bala
    public float maxTime;               // Tiempo máximo de vida de la bala

    public GameObject objective;        // Objetivo al que se dirige la bala
    public float timer;                 // Temporizador para controlar el tiempo de vida de la bala
    public GameObject enemyRanged;      // Referencia al enemigo que disparó la bala

    private void Awake()
    {
        objective = GameObject.Find("Player");  // Encuentra el objeto jugador y lo establece como objetivo
    }

    void Update()
    {
        timer += Time.deltaTime;  // Incrementa el temporizador con el tiempo transcurrido desde el último frame

        if (timer > maxTime)
        {
            enemyRanged.GetComponent<EnemyChase>().bullets = 0;  // Establece la cantidad de balas del enemigo en 0
            timer = 0;
            Destroy(gameObject);  // Destruye la bala
        }

        // Mueve la bala hacia el objetivo a una velocidad constante
        transform.position = Vector2.MoveTowards(transform.position, objective.transform.position, bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))  // Comprueba si la bala colisionó con el jugador
        {
            // Reduce la salud del jugador llamando a su método TakeDamage
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(10);

            enemyRanged.GetComponent<EnemyChase>().bullets = 0;  // Establece la cantidad de balas del enemigo en 0
            Destroy(gameObject);  // Destruye la bala
        }
    }
}