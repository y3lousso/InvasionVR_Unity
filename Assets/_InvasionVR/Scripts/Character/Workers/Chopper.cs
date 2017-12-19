using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : Townie
{
    // Use this for initialization
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public override void StartWork()
    {
        animator.SetBool("IsChopping", true);
    }

    public override void StopWork()
    {
        animator.SetBool("IsChopping", false);
    }

    public void OnChoppingEnd()
    {
        Debug.Log("chopping");
    }
}
