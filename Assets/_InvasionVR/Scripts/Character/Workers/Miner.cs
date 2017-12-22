using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Townie
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void StartWork()
    {
        animator.SetBool("IsMining", true);
    }

    public override void StopWork()
    {
        animator.SetBool("IsMining", false);
    }

    public void MiningEndedCallback()
    {
        Debug.Log("mining");
    }
}
