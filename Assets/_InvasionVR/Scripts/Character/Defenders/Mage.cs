﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Defender
{

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public void CastSpell()
    {
        Fire();
    }
}
