using UnityEngine;

public class SimpleTutorial : MonoBehaviour
{
    [SerializeField] GameObject _unequipAdvice, _equipAdvice, _inventoryButtons;
    [SerializeField] bool _isTutorial=true;
    [SerializeField] int _activatedTimes = 0,_lastAdvice=0;


    void Start()
    {
        _unequipAdvice.SetActive(true);
        Time.timeScale = 1;
    }


    void Update()
    {
   if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.B)) && _isTutorial)
        {
            Time.timeScale = 0;
            _unequipAdvice.SetActive(false);
            _equipAdvice.SetActive(false);

        }

   if (Time.timeScale== 0 && (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.B)))
        {
            {
                if (_lastAdvice==1)
                {
                    _equipAdvice.SetActive(true);
                }
                else
                {
                    _unequipAdvice.SetActive(true);

                }
            }
        }
    }

    public void UnequipAdvice()
    {
        if (_activatedTimes < 1)
        {
            _lastAdvice = 1;
            _unequipAdvice.SetActive(false);
            _equipAdvice.SetActive(true);
            _activatedTimes = 1;
        }
        else
        {
            return;
        }
    }
    public void EquipAdvice()
    {
        
        Time.timeScale = 1;
        _equipAdvice.SetActive(false);
        _isTutorial = false;
    }


}
