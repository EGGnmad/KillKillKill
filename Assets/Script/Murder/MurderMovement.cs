using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderMovement : MonoBehaviour
{
    public bool isGoingTo = false;

    public void GoTo(Vector2 pos)
    {
        StartCoroutine(Go(pos));
    }

    IEnumerator Go(Vector2 to)
    {
        isGoingTo = true;
        Vector2 from = transform.position;
        float cnt = 0f;


        while (true)
        {
            if (cnt >= 1f) break;

            cnt += Time.deltaTime;
            transform.position = Vector2.Lerp(from, to, cnt);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        isGoingTo = false;
    }

    public void ArmsDown()
    {
        StartCoroutine(Down());
    }

    IEnumerator Down()
    {
        float cnt = 1f;

        while (true)
        {
            if (cnt <= 0f) break;

            cnt -= Time.deltaTime*4;
            foreach(var arm in GetComponents<ArmController>())
            {
                arm.LerpArm(cnt);
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
