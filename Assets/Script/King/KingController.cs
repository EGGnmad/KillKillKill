using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour
{
    public void Tick()
    {
        GameObject murder = GameDirector.Instance.Character.Murder;

        if (Input.GetKeyDown(KeyCode.Space) && !murder.GetComponent<MurderMovement>().isGoingTo)
        {
            bool result = murder.GetComponent<MurderController>().StopRandomAttack();
            StopDecreaseKingGauge();
            GetComponent<KingDefense>().StartCheck();

            if (result)
            {
                murder.GetComponent<Animator>().SetBool("walk", false);
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<KingDefense>().StopCheck();
            StartDecreaseKingGauge();

            murder.GetComponent<Animator>().SetBool("walk", true);
            murder.GetComponent<MurderController>().StartRandomAttack();
        }
    }

    // AI
    public void StartRandomCheck()
    {
        StartCoroutine("Check");
    }

    public void StopRandomCheck()
    {
        StopCoroutine("Check");
        GetComponent<FaceController>().ChangeFaceToIdle();
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(Random.Range(1f, 12f));

        GetComponent<FaceController>().ChangeFaceToOther();

        yield return new WaitForSeconds(0.35f);

        GetComponent<FaceController>().ChangeFaceToIdle();

        GetComponent<KingDefense>().StartCheck();

        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        GetComponent<KingDefense>().StopCheck();
    }

    // Hp Gauge
    public void StartDecreaseKingGauge()
    {
        StartCoroutine("DecreaseGauge");
    }

    public void StopDecreaseKingGauge()
    {
        StopCoroutine("DecreaseGauge");
    }

    IEnumerator DecreaseGauge()
    {
        while (true)
        {
            if (GameDirector.Instance.KingGauge.value <= 0f)
            {
                GameDirector.Instance.Character.OldDeath();
                break;
            }

            GameDirector.Instance.KingGauge.value -= Time.deltaTime / 30f;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
