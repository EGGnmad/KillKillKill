using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderAttack : MonoBehaviour
{
    public bool isCharging = false;
    [SerializeField] float time = 7f;
    [SerializeField] ArmController[] arms;

    private void Start()
    {
        arms = GetComponents<ArmController>();
    }

    public void StartAttack()
    {
        isCharging = true;
        StartCoroutine("Charge");
    }

    public void StopAttack()
    {
        isCharging = false;
        StopCoroutine("Charge");

        // reset gauge
        GameDirector.Instance.MurderGauge.value = 0f;

        // reset arms
        foreach (var arm in arms)
        {
            arm.LerpArm(GameDirector.Instance.MurderGauge.value);
        }

        // reset face
        GetComponent<FaceController>().ChangeFaceToIdle();
    }

    public void Attack()
    {
        GameDirector.Instance.Character.MurderAttack();
    }

    IEnumerator Charge()
    {
        while (true)
        {
            if (GameDirector.Instance.MurderGauge.value >= 1f) break;

            GameDirector.Instance.MurderGauge.value += Time.deltaTime/time;
            yield return new WaitForSeconds(Time.deltaTime/time);

            // arms up
            foreach(var arm in arms)
            {
                arm.LerpArm(GameDirector.Instance.MurderGauge.value);
            }

            // face
            if(GameDirector.Instance.MurderGauge.value > 0.5f)
            {
                GetComponent<FaceController>().ChangeFaceToOther();
            }
        }

        Attack();
    }
}
