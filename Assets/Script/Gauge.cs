using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    float _value;

    public float value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            _value = Mathf.Clamp(_value, min, max);
            GameDirector.Instance.UpdateGaugeUI();
        }
    }

    public float min = 0;
    public float max = 1;
}
