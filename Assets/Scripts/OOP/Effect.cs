using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public float Value { get; private set; }

    public Timer Timer
    {
        get
        {
            if (_timer == null)
                _timer = new Timer(_time);
            return _timer;
        }
    }

    private float _time = 0;
    private Timer _timer = null;

    public Effect(float time, float value = 0)
    {
        _time = time;
        Value = value;
    }
}
