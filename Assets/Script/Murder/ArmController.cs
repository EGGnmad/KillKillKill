using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] Transform arm;
    [SerializeField] float to;
    float from;

    private void Start()
    {
        from = arm.eulerAngles.z;
    }

    public void RestoreArm()
    {
        arm.eulerAngles = new Vector3(0, 0, from);
    }

    public void LerpArm(float range)
    {
        float angle = Mathf.LerpAngle(from, to, range);
        arm.eulerAngles = new Vector3(0, 0, angle);
    }
}
