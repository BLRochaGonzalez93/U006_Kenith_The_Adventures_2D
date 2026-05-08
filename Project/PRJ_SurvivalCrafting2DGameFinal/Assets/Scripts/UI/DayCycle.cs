using UnityEngine;

/// <summary>
/// La clase DayCycle controla el ciclo de día y noche en el juego.
/// Los campos _dawn, _dawnMorning, _morning, _morningDay, _day, _dayAfternoon, _afternoon, _afternoonNight, _night y _nightDawn son objetos GameObject que representan los distintos momentos del día.
/// Los campos _dawnTimer, _dawnMorningTimer, _morningTimer, _morningDayTimer, _dayTimer, _dayAfternoonTimer, _afternoonTimer, _afternoonNightTimer, _nightTimer, _nightDawnTimer y _delay son variables que controlan el tiempo en cada momento del ciclo de día y noche.
/// En el método Update, se incrementa el valor de _delay en cada fotograma con el tiempo transcurrido.
/// Luego se comparan los valores de _delay con los diferentes momentos del ciclo de día y noche para determinar qué objeto GameObject debe estar activo en cada momento.
/// Una vez que _delay alcanza el valor de _dayTimer, se reinicia a cero para comenzar nuevamente el ciclo
/// </summary>

public class DayCycle : MonoBehaviour
{
    [SerializeField] private GameObject _dawn, _dawnMorning, _morning, _morningDay, _day, _dayAfternoon, _afternoon, _afternoonNight, _night, _nightDawn;
    [SerializeField] private float _dawnTimer, _dawnMorningTimer, _morningTimer, _morningDayTimer, _dayTimer, _dayAfternoonTimer, _afternoonTimer, _afternoonNightTimer, _nightTimer, _nightDawnTimer, _delay;

    // Update is called once per frame
    void Update()
    {
        _delay += Time.deltaTime;

        // Configuración de los timers para cada momento del ciclo de día y noche
        _dayAfternoonTimer = 60;
        _afternoonTimer = 120;
        _afternoonNightTimer = 180;
        _nightTimer = 240;
        _nightDawnTimer = 300;
        _dawnTimer = 360;
        _dawnMorningTimer = 420;
        _morningTimer = 480;
        _morningDayTimer = 540;
        _dayTimer = 600;

        // Determinar el momento del día en función del tiempo transcurrido (_delay)
        if (_delay < _dayAfternoonTimer)
        {
            _morningDay.SetActive(false);
            _day.SetActive(true);
        }
        else if (_delay < _afternoonTimer)
        {
            _day.SetActive(false);
            _dayAfternoon.SetActive(true);
        }
        else if (_delay < _afternoonNightTimer)
        {
            _dayAfternoon.SetActive(false);
            _afternoon.SetActive(true);
        }
        else if (_delay < _nightTimer)
        {
            _afternoon.SetActive(false);
            _afternoonNight.SetActive(true);
        }
        else if (_delay < _nightDawnTimer)
        {
            _afternoonNight.SetActive(false);
            _night.SetActive(true);
        }
        else if (_delay < _dawnTimer)
        {
            _night.SetActive(false);
            _nightDawn.SetActive(true);
        }
        else if (_delay < _dawnMorningTimer)
        {
            _nightDawn.SetActive(false);
            _dawn.SetActive(true);
        }
        else if (_delay < _morningTimer)
        {
            _dawn.SetActive(false);
            _dawnMorning.SetActive(true);
        }
        else if (_delay < _morningDayTimer)
        {
            _dawnMorning.SetActive(false);
            _morning.SetActive(true);
        }
        else if (_delay < _dayTimer)
        {
            _morning.SetActive(false);
            _morningDay.SetActive(true);
        }
        else if (_delay >= _dayTimer)
        {
            _delay = 0;
        }
    }
}