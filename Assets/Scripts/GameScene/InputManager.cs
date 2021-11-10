using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private float _sidewaysMotion = 0f;

    public float SidewaysMotion
    {
        get
        {
            return _sidewaysMotion;
        }
    }

    private void Update()
    {
        Vector3 accel = Input.acceleration;

        _sidewaysMotion = accel.x;
    }
}
