using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDirector : MonoBehaviour
{
    public GameObject King;
    public GameObject Murder;
    public PlayerState State = PlayerState.Murder;

    public GameObject PlayableCharacter
    {
        get
        {
            if (State == PlayerState.King)
                return King;
            return Murder;
        }
    }

    [Header("Location")]
    public Transform KingSpawnPos;
    public Transform MurderSpawnPos;

    private void Start()
    {
        GetComponent<Act>().NextAct();
    }

    public void MurderAttack()
    {
        if (State == PlayerState.King)
        {
            State = PlayerState.Wait;


            Murder.GetComponent<MurderMovement>().GoTo(KingSpawnPos.position);
            Murder.GetComponent<MurderMovement>().ArmsDown();

            King.GetComponent<FaceController>().ChangeFaceToOther();
            King.GetComponent<Animator>().SetTrigger("death");

            PlayerPrefs.SetInt("Revealed Cnt", GameDirector.Instance.revealCnt);
            PlayerPrefs.SetInt("Catch Cnt", GameDirector.Instance.catchCnt);
            PlayerPrefs.SetString("End King Name", King.name);

            StartCoroutine(WaitFailScene(2f));
        }

        else
        {
            State = PlayerState.Wait;

            Murder.GetComponent<MurderMovement>().GoTo(KingSpawnPos.position);
            Murder.GetComponent<MurderMovement>().ArmsDown();

            King.GetComponent<FaceController>().ChangeFaceToOther();
            King.GetComponent<Animator>().SetTrigger("death");

            StartCoroutine(WaitNextScene(2f));
        }
    }
    IEnumerator WaitFailScene(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("FailScene");
    }
    IEnumerator WaitNextScene(float time)
    {
        yield return new WaitForSeconds(time);

        GetComponent<Act>().StopAct();
        GetComponent<Act>().NextAct();
    }

    public void RevealMurder()
    {
        if (State == PlayerState.Murder)
        {
            GameDirector.Instance.revealCnt++;
        }
        if(State == PlayerState.King)
        {
            GameDirector.Instance.catchCnt++;
        }
        State = PlayerState.Wait;

        Murder.GetComponent<MurderAttack>().StopAttack();

        StartCoroutine(WaitRestartScene(0.5f));
    }
    IEnumerator WaitRestartScene(float time)
    {
        yield return new WaitForSeconds(time);

        GetComponent<Act>().RestartAct();
    }

    public void OldDeath()
    {
        State = PlayerState.Wait;

        Murder.GetComponent<MurderMovement>().GoTo(KingSpawnPos.position);

        King.GetComponent<FaceController>().ChangeFaceToOther();
        King.GetComponent<Animator>().SetTrigger("death");

        StartCoroutine(WaitNextScene(1f));
    }

    public enum PlayerState
    {
        King,
        Murder,
        Wait
    }
}
