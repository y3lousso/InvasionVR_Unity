using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisherman : Townie
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
        animator.SetBool("IsFishing", true);
    }

    public override void StopWork()
    {
        animator.SetBool("IsFishing", false);
    }

    public void FishingEndedCallback()
    {
        Debug.Log("fishing");
    }
}
