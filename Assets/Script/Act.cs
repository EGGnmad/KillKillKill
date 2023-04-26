using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static CharacterDirector;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class Act : MonoBehaviour
{
    [SerializeField] ActObject[] actObjects;
    int index = 0;

    GameObject _king;
    GameObject _murder;
    PlayerState _state;

    CharacterDirector director;

    private void Start()
    {
        director = GameDirector.Instance.Character;
    }

    public void NextAct()
    {
        GameObject newKing = actObjects[index].King;
        GameObject newMurder = actObjects[index].Murder;
        PlayerState newState = actObjects[index].state;

        print(index);
        index++;
        StartNewAct(newKing, newMurder, newState);
    }

    void StartNewAct(GameObject newKing, GameObject newMurder, PlayerState newState)
    {
        // save original
        _king = newKing;
        _murder = newMurder;
        _state = newState;

        GameDirector.Instance.MurderGauge.value = GameDirector.Instance.MurderGauge.min;
        GameDirector.Instance.KingGauge.value = GameDirector.Instance.KingGauge.max;
        GameDirector.Instance.DisableGaugeUI(newState);

        if(newState == PlayerState.Wait)
        {
            PlayerPrefs.SetInt("Revealed Cnt", GameDirector.Instance.revealCnt);
            PlayerPrefs.SetInt("Catch Cnt", GameDirector.Instance.catchCnt);
            SceneManager.LoadScene("ClearScene");
        }

        if (newKing != null)
            director.King = Instantiate(newKing, director.KingSpawnPos.position, Quaternion.identity);

        if (newMurder != null)
        {
            Vector2 murderPos = director.MurderSpawnPos.position - director.MurderSpawnPos.right * 10f;
            director.Murder = Instantiate(newMurder, murderPos, Quaternion.identity);
            director.Murder.GetComponent<MurderMovement>().GoTo(director.MurderSpawnPos.position);

            director.Murder.GetComponent<Animator>().SetBool("walk", true);
        }

        director.State = newState;

        if (director.State == PlayerState.King)
        {
            director.Murder.GetComponent<MurderController>().StartRandomAttack();
            director.King.GetComponent<KingController>().StartDecreaseKingGauge();
        }
    }

    public void StopAct()
    {
        if (director.State == PlayerState.King)
        {
            director.King.GetComponent<KingController>().StopDecreaseKingGauge();
        }

        Destroy(director.King);
        Destroy(director.Murder);
        director.State = PlayerState.Wait;
    }

    public void RestartAct()
    {
        PlayerState stateTemp = director.State;
        StopAct();

        GameDirector.Instance.MurderGauge.value = GameDirector.Instance.MurderGauge.min;

        if (_king != null)
            director.King = Instantiate(_king, director.KingSpawnPos.position, Quaternion.identity);

        if (_murder != null)
        {
            Vector2 murderPos = director.MurderSpawnPos.position - director.MurderSpawnPos.right * 10f;
            director.Murder = Instantiate(_murder, murderPos, Quaternion.identity);
            director.Murder.GetComponent<MurderMovement>().GoTo(director.MurderSpawnPos.position);

            director.Murder.GetComponent<Animator>().SetBool("walk", true);
        }

        if (_state == PlayerState.King)
        {
            director.Murder.GetComponent<MurderController>().StartRandomAttack();
            director.King.GetComponent<KingController>().StartDecreaseKingGauge();
        }

        director.State = _state;

        foreach (var background in GameDirector.Instance.Backgrounds)
        {
            background.StartScroll();
        }
    }
}
