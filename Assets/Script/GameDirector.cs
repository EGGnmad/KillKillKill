using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [Header("Character")]
    public CharacterDirector Character;
    public CharacterController Controller;
    public int revealCnt = 0;
    public int catchCnt = 0;

    [Header("Gauge")]
    public Gauge MurderGauge;
    [SerializeField] GameObject murderGaugeUI;


    public Gauge KingGauge;
    [SerializeField] GameObject kingGaugeUI;


    public void UpdateGaugeUI()
    {
        kingGaugeUI.GetComponent<Slider>().value = KingGauge.value;
        murderGaugeUI.GetComponent<Slider>().value = MurderGauge.value;
    }

    public void DisableGaugeUI(CharacterDirector.PlayerState state)
    {
        if(state == CharacterDirector.PlayerState.King)
        {
            kingGaugeUI.SetActive(true);
            murderGaugeUI.SetActive(false);
        }
        else if (state == CharacterDirector.PlayerState.Murder)
        {
            kingGaugeUI.SetActive(false);
            murderGaugeUI.SetActive(true);
        }
    }

    [Header("Background")]
    public PositionScroller[] Backgrounds;




    // Singleton pattern
    static GameDirector _instance = null;

    public static GameDirector Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
