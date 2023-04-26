using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MurderController : MonoBehaviour
{
    public void Tick()
    {
        GameObject king = GameDirector.Instance.Character.King;

        if (Input.GetKeyDown(KeyCode.Space) && !GetComponent<MurderMovement>().isGoingTo)
        {
            GetComponent<MurderAttack>().StartAttack();

            king.GetComponent<KingController>().StartRandomCheck();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<MurderAttack>().StopAttack();

            king.GetComponent<KingController>().StopRandomCheck();
        }
        else if(GameDirector.Instance.MurderGauge.value >= 1f)
        {
            king.GetComponent<KingController>().StopRandomCheck();
        }
    }

    //AI
    public void StartRandomAttack()
    {
        StartCoroutine("Attack");
    }

    public bool StopRandomAttack()
    {
        if(GameDirector.Instance.MurderGauge.value < Random.Range(0.6f, 0.9f))
        {
            GetComponent<MurderAttack>().StopAttack();
            StopCoroutine("Attack");
            return true;
        }
        return false;
    }


    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

        GetComponent<MurderAttack>().StartAttack();
    }
}
