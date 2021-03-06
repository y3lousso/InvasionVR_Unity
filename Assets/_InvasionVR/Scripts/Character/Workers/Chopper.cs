﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : Townie
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

    protected override void OnTownieGrabbed(object sender, VRTK.InteractableObjectEventArgs e)
    {
        base.OnTownieGrabbed(sender,e); 
    }

    public override void StartWork()
    {
        Start();
        animator.SetBool("IsChopping", true);
    }

    public override void StopWork()
    {
        animator.SetBool("IsChopping", false);
    }

    public void ChoppingEndedCallback()
    {
        Debug.Log("chopping");
    }
}
