using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sick : CurcorFollower
{
    [SerializeField] private CameraALotOfEffects effects;

    //private bool CanDoThis;

    [SerializeField] private float acceptableDistance;
    [SerializeField] private float timeToCheck = 0.1f;
    [SerializeField] private float timer;

    [SerializeField] private Vector2 stPos;
    [SerializeField] private Vector2 endPos;

    private void Start()
    {
        stPos = new Vector2(transform.position.x, transform.position.y);
    }
    protected override void Update()
    {
        base.Update();

        CheckToDown();
    }

    private void CheckToDown()
    {
        timer += Time.deltaTime;

        if (timer >= timeToCheck)
        {
            endPos = new Vector2(transform.position.x, transform.position.y);

            if (Vector2.Distance(endPos, stPos) > acceptableDistance)
            {
                DownWithTheSikness();
            }

            timer = 0;
            stPos = endPos;
        }
    }

    private void DownWithTheSikness()
    {
        effects.StartChrome(true);
    }
}
