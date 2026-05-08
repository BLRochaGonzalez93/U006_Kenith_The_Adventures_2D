using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f; // Velocidad de movimiento del jugador.
    private Rigidbody2D _rb; // Referencia al componente Rigidbody2D del jugador.
    private Vector2 _movementDirection, _pointerInput; // Direcci�n de movimiento y entrada del puntero.
    public float dashTime = 0.25f; // Duraci�n del _dash.
    public float dashCD = 5f; // Tiempo de reutilizaci�n del _dash.
    public float timer = 0f; // Temporizador para el _dash.
    public bool canDash = true; // Indica si se puede realizar un _dash.
    public bool isDashing = false; // Indica si el jugador est� realizando un _dash.
    public bool isWalking = false; // Indica si el jugador est� caminando.
    public float dashPower = 10f; // Fuerza aplicada durante el _dash.
    [SerializeField] private Animator _animator; // Referencia al componente Animator del jugador.
    private RaycastHit2D _hit; // Informaci�n del raycast.
    public LayerMask obstacle; // M�scara de capa para los obst�culos.
    private Vector3 _dir; // Direcci�n del raycast.
    public Vector3 pos; // Posici�n del raycast.

    public LifeStaExpBarsUI lseUI; // Referencia al script para la interfaz de vida, estado y barras de experiencia.
    [SerializeField] private GameObject _pauseMenu; // Referencia al men� de pausa.

    [SerializeField] private GameObject _cam; // Referencia a la c�mara.
    public Transform camPoint; // Punto de anclaje para la c�mara.
    public Vector2 camLimitX; // L�mites horizontales de la c�mara.
    public Vector2 camLimitY; // L�mites verticales de la c�mara.

    [SerializeField] private InputActionReference _movement, _pointerPosition, _dash, _pauseGame, _attack; // Referencias a las acciones de entrada.

    [SerializeField] private Weapon Weapon; // Referencia al script Weapon para el arma W.
    [SerializeField] private WeaponParent WeaponHolder; // Referencia al script WeaponParent para el Holder.


    [SerializeField] private GameObject _flickShape; // Referencia al objeto para el efecto de destello.
    [SerializeField] GameObject _roof; // Referencia al objeto techo.
    [SerializeField] GameObject _advice; // Referencia al objeto consejo.
    [SerializeField] ParticleSystem _forgeLightPS; // Referencia al sistema de part�culas para la luz de la forja.

    public AudioSource pasos, dmgTaken, death;

    public GameObject eriel; // Referencia al objeto Eriel.

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del jugador.
        _forgeLightPS.gameObject.SetActive(false); // Desactivar el sistema de part�culas de la luz de la forja.
        camLimitX = new Vector2(-195, 192); // Establecer los l�mites horizontales de la c�mara.
        camLimitY = new Vector2(156, -59); // Establecer los l�mites verticales de la c�mara.

    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        _movementDirection = _movement.action.ReadValue<Vector2>(); // Obtener la direcci�n de movimiento del jugador.
        _rb.linearVelocity = _movementDirection * _movementSpeed; // Aplicar la velocidad de movimiento al Rigidbody2D.
  
        _animator.SetBool("Run", _rb.linearVelocity != Vector2.zero); // Establecer el par�metro "Run" en el Animator.
        _animator.SetBool("Run", _rb.linearVelocity == Vector2.zero); // Establecer el par�metro "Run" en el Animator.
        if (_animator.GetBool("Run"))
        {
            pasos.Play();
        }

        _pointerInput = GetPointerInput(); // Obtener la entrada del puntero.
        WeaponHolder.PointerPosition = _pointerInput; // Establecer la posici�n del puntero para el Holder de las armas.

        if (canDash && _dash.action.IsPressed())
        {
            timer = 0;
            lseUI.SetEnergy();
            StartCoroutine(Dash());
        }
        if (timer > dashCD)
        {
            timer = dashCD;
        }
        timer += Time.deltaTime;

        ManageCamMovement(); // Gestionar el movimiento de la c�mara.
        ManageInteractions(); // Gestionar las interacciones.

        if (_pauseGame.action.IsPressed())
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }

        eriel.transform.position = Vector2.Lerp(eriel.transform.position, new Vector2(transform.position.x + 1.5f, transform.position.y + 1.5f), 2.5f * Time.deltaTime);
    }

    // Se llama cuando se habilita el script.
    private void OnEnable()
    {
        _attack.action.performed += PerformAttack; // Asignar el m�todo PerformAttack al evento performed de la acci�n de ataque.
    }

    // Se llama cuando se deshabilita el script.
    private void OnDisable()
    {
        _attack.action.performed -= PerformAttack; // Quitar el m�todo PerformAttack del evento performed de la acci�n de ataque.
    }

    // Realiza el ataque del jugador.
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("Player Attack");
        WeaponHolder.Attack();   // Realizar el ataque del arma Holder.
    }

    // Recibe da�o.
    public void TakeDamage(int dmg)
    {
        Invoke(nameof(EnableFlick), 0f);
        Invoke(nameof(DisableFlick), 0.25f);
        gameObject.GetComponent<Character>().currentHealth -= dmg;
        dmgTaken.Play();
        CheckLife();
        lseUI.SetHealth();
    }

    // Verificar si el jugador ha perdido toda su vida.
    private void CheckLife()
    {
        if (gameObject.GetComponent<Character>().currentHealth <= 0)
        {
            death.Play();
            transform.position = new Vector2(-70, 60);
            gameObject.GetComponent<Character>().currentHealth = gameObject.GetComponent<Character>().startHealth;
        }
    }

    // Corrutina para realizar el dash.
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        _rb.linearVelocity = _movementDirection * dashPower;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    // Obtener la entrada del puntero.
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = _pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    // Gestionar las interacciones.
    private void ManageInteractions()
    {
        if (CheckCollision)
        {
            if (_hit.transform.TryGetComponent(out Interaction interaction))
            {
                Debug.Log("Has interaction with: " + _hit.transform.gameObject.layer);
                _advice.SetActive(true);
                interaction.Interact(_hit.transform);
            }
        }
        else
        {
            _advice.SetActive(false);
        }
    }

    // Verificar si hay colisi�n.
    public bool CheckCollision
    {
        get
        {
            _hit = Physics2D.Raycast(transform.position + pos, _dir, 1, obstacle);
            return _hit.collider != null;
        }
    }

    // Activar el destello.
    private void EnableFlick()
    {
        _flickShape.SetActive(true);
    }

    // Desactivar el destello.
    private void DisableFlick()
    {
        _flickShape.SetActive(false);
    }

    // Se llama cuando se produce una colisi�n con un objeto desencadenador.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Roof"))
        {
            _roof.SetActive(false);
            _forgeLightPS.gameObject.SetActive(true);
        }
    }

    // Se llama cuando se produce una salida de colisi�n con un objeto desencadenador.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Roof"))
        {
            _roof.SetActive(true);
            _forgeLightPS.gameObject.SetActive(false);
        }
    }

    // Gestionar el movimiento de la c�mara.
    private void ManageCamMovement()
    {
        Vector3 finalCamPos = camPoint.transform.position;
        finalCamPos.z = -10;

        if (finalCamPos.x < camLimitX.x)
        {
            finalCamPos.x = camLimitX.x;
        }
        if (finalCamPos.x > camLimitX.y)
        {
            finalCamPos.x = camLimitX.y;
        }
        if (finalCamPos.y > camLimitY.x)
        {
            finalCamPos.y = camLimitY.x;
        }
        if (finalCamPos.y < camLimitY.y)
        {
            finalCamPos.y = camLimitY.y;
        }
        _cam.transform.position = Vector3.Lerp(_cam.transform.position, finalCamPos, 2f * Time.deltaTime);
    }
}
