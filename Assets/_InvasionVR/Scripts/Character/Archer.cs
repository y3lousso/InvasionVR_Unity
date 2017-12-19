using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character {

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void GrabArrow()
    {
        Debug.Log("grab");
    }

    public void ShootArrow()
    {
        Debug.Log("shoot");
        Fire();
    }
}
