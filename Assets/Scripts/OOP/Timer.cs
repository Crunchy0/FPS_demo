using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float TimeLeft { get; private set; }
    public bool IsExpired { get => TimeLeft == 0; }

    private float _time;

    public Timer(float time) => TimeLeft = _time = time;

    public void Update(float deltaTime)
    {
        if (IsExpired)
            return;

        TimeLeft -= deltaTime;
        if (TimeLeft < 0)
            TimeLeft = 0;
    }
    
    public void Reset()
    {
        TimeLeft = _time;
    }
}
