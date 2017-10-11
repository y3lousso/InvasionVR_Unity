using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TriggerUtilityEventArgs
{

}

public delegate void TriggerUtilityEventHandler(object o, TriggerUtilityEventArgs e);

public class TriggerUtility : MonoBehaviour {

    public string tagToDetect;

    public TriggerUtilityEventHandler OnTriggerUtilityEnter;
    public TriggerUtilityEventHandler OnTriggerUtilityExit;

    public virtual void TriggerUtilityEnterSender(TriggerUtilityEventArgs e)                 //SENDER
    {
        if (OnTriggerUtilityEnter != null)
        {
            OnTriggerUtilityEnter(this, e);
        }
    }

    public virtual void TriggerUtilityExitSender(TriggerUtilityEventArgs e)                 //SENDER
    {
        if (OnTriggerUtilityExit != null)
        {
            OnTriggerUtilityExit(this, e);
        }
    }


    public TriggerUtilityEventArgs SetTriggerUtility()
    {
        TriggerUtilityEventArgs e;
        return e;
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == tagToDetect)
        {
            TriggerUtilityEnterSender(SetTriggerUtility());
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == tagToDetect)
        {
            TriggerUtilityExitSender(SetTriggerUtility());
        }
    }

}
