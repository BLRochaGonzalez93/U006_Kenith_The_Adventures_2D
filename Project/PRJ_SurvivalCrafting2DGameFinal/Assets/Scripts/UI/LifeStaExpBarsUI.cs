using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// El código es una clase MonoBehaviour que se encarga de actualizar las barras de vida, experiencia y energía en una interfaz de usuario.
/// Las variables serializadas representan los elementos visuales de la interfaz y los textos asociada cada barra.
/// El valor constante LIFE_BAR_WIDTH representa el ancho máximo de la barra de vida.
/// El método Start crea una instancia de LifeStaExpSystem y configura los valores iniciales de las barras.
/// El método Update actualiza la instancia de LifeStaExpSystem y actualiza los valores de las barras en cada cuadro.
/// El método SetLifeStaExpSystem permite establecer la instancia de LifeStaExpSystem utilizada por este componente.
/// El método SetExperience actualiza la barra de experiencia y el texto correspondiente.
/// El método SetEnergy actualiza la barra de energía y el texto correspondiente.
/// El método SetHealth actualiza la barra de vida y el texto correspondiente
/// </summary>

public class LifeStaExpBarsUI : MonoBehaviour
{
    private const float LIFE_BAR_WIDTH = 885f;
    [SerializeField] private Image _expeBarImage, _eneBarImage;
    [SerializeField] private RectTransform _lifeBarRecTransform;
    [SerializeField] private TextMeshProUGUI _lifeText, _experienceText, _energyText;

    private LifeStaExpSystem _lseSystem;

    /// <summary>
    /// Método Start que se ejecuta al iniciar el objeto.
    /// Crea una instancia de LifeStaExpSystem y configura los valores iniciales de las barras de vida, experiencia y energía.
    /// </summary>
    private void Start()
    {
        LifeStaExpSystem lseSystem = new();
        SetLifeStaExpSystem(lseSystem);

        SetEnergy();
        SetExperience();
        SetHealth();
    }

    /// <summary>
    /// Método Update que se ejecuta en cada cuadro.
    /// Actualiza la instancia de LifeStaExpSystem y actualiza los valores de las barras de vida, experiencia y energía.
    /// </summary>
    private void Update()
    {
        _lseSystem = new LifeStaExpSystem();
        SetLifeStaExpSystem(_lseSystem);

        SetEnergy();
        SetExperience();
        SetHealth();
    }

    /// <summary>
    /// Establece la instancia de LifeStaExpSystem utilizada por este componente.
    /// </summary>
    /// <param name="lseSystem">Instancia de LifeStaExpSystem</param>
    public void SetLifeStaExpSystem(LifeStaExpSystem lseSystem)
    {
        this._lseSystem = lseSystem;
    }

    /// <summary>
    /// Actualiza la barra de experiencia y el texto correspondiente.
    /// </summary>
    public void SetExperience()
    {
        _expeBarImage.fillAmount = ((float)_lseSystem.experienceAmount / _lseSystem.experienceAmountMax);
        _experienceText.text = "Experience: " + Mathf.Round(((float)_lseSystem.experienceAmount / _lseSystem.experienceAmountMax) * 100) + "% (" + _lseSystem.experienceAmount + "/" + _lseSystem.experienceAmountMax + ")";
    }

    /// <summary>
    /// Actualiza la barra de energía y el texto correspondiente.
    /// </summary>
    public void SetEnergy()
    {
        _eneBarImage.fillAmount = ((float)_lseSystem.energyAmount / _lseSystem.energyAmountMax);
        _energyText.text = "Energy: " + Mathf.Round(((float)_lseSystem.energyAmount / _lseSystem.energyAmountMax) * 100) + "%";
    }

    /// <summary>
    /// Actualiza la barra de vida y el texto correspondiente.
    /// </summary>
    public void SetHealth()
    {
        _lifeBarRecTransform.sizeDelta = new Vector2(((float)_lseSystem.healthAmount / _lseSystem.healthAmountMax) * LIFE_BAR_WIDTH, _lifeBarRecTransform.sizeDelta.y);
        _lifeText.text = "Life: " + Mathf.Round(((float)_lseSystem.healthAmount / _lseSystem.healthAmountMax) * 100) + "% (" + _lseSystem.healthAmount + "/" + _lseSystem.healthAmountMax + ")";
    }
}
