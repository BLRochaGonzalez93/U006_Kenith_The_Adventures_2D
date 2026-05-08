using UnityEngine;

/// <summary>
/// Este script controla el tiempo de la lluvia en el juego. Cuando se alcanza un tiempo aleatorio determinado, se activa la lluvia durante un período de tiempo específico. 
/// Durante la lluvia, se activan los efectos visuales de las gotas de lluvia, las salpicaduras y la atmósfera de lluvia. 
/// Después de que la duración de la lluvia haya transcurrido, los efectos se desactivan y se establece un nuevo tiempo aleatorio para la próxima lluvia
/// </summary>
public class RainTime : MonoBehaviour
{
    [SerializeField] private float _rainDuration = 120f;
    [SerializeField] private float _randomTime;
    [SerializeField] private float _timer;
    [SerializeField] private float _rainTimer;
    [SerializeField] private GameObject _rainDrops;
    [SerializeField] private GameObject _rainSplash;
    [SerializeField] private GameObject _rainAtmosphere;
    [SerializeField] private bool _isRaining;
    public AudioSource men;

    private void Start()
    {
        // Establece un tiempo aleatorio para la próxima lluvia
        _randomTime = Random.Range(240, 600);
    }

    void Update()
    {
        _timer += Time.deltaTime;

        // Si está lloviendo
        if (_isRaining)
        {
            _rainTimer += Time.deltaTime;
            // Activa los efectos de lluvia
            _rainDrops.SetActive(true);
            _rainSplash.SetActive(true);
            _rainAtmosphere.SetActive(true);
        }

        // Comprueba si ha pasado el tiempo suficiente para iniciar la lluvia
        if (_timer >= _randomTime)
        {
            _isRaining = true;
            
        }

        // Comprueba si ha pasado la duración de la lluvia
        if (_rainTimer >= _rainDuration)
        {
            _isRaining = false;
            _rainTimer = 0;
            // Desactiva los efectos de lluvia
            _rainDrops.SetActive(false);
            _rainSplash.SetActive(false);
            _rainAtmosphere.SetActive(false);
            _timer = 0;
            // Establece un nuevo tiempo aleatorio para la próxima lluvia
            _randomTime = Random.Range(240, 600);
        }
    }
}