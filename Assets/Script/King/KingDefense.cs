using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingDefense : MonoBehaviour
{
    public bool isChecking = false;

    public void StartCheck()
    {
        isChecking = true;
        StartCoroutine("Check");
        GetComponent<Animator>().SetBool("walk", false);

        foreach (var background in GameDirector.Instance.Backgrounds)
        {
            background.StopScroll();
        }

        //flip
        Vector3 localScale = transform.localScale;
        localScale.x = -1;
        transform.localScale = localScale;

    }

    public void StopCheck()
    {
        isChecking = false;
        StopCoroutine("Check");
        GetComponent<Animator>().SetBool("walk", true);

        foreach (var background in GameDirector.Instance.Backgrounds)
        {
            background.StartScroll();
        }

        //flip
        Vector3 localScale = transform.localScale;
        localScale.x = 1;
        transform.localScale = localScale;
    }

    IEnumerator Check()
    {
        while (true)
        {
            if (GameDirector.Instance.Character.Murder.GetComponent<MurderAttack>().isCharging)
            {
                GameDirector.Instance.Character.RevealMurder();
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
