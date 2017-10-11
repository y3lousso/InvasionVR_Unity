using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TriggerUtility))]
public class TriggerUtility_UnityEvent : MonoBehaviour {

    private TriggerUtility tu;

    [System.Serializable]
    public class UnityObjectEvent : UnityEvent<object, TriggerUtilityEventArgs> { }

    /// <summary>
    /// Emits the InteractableObjectTouched class event.
    /// </summary>
    public UnityObjectEvent TriggerUtilityEnter;
    /// <summary>
    /// Emits the InteractableObjectUntouched class event.
    /// </summary>
    public UnityObjectEvent TriggerUtilityExit;

    private void SetTriggerUtility()
    {
        if (tu == null)
        {
            tu = GetComponent<TriggerUtility>();
        }

    }
    public void OnEnable()
    {
        SetTriggerUtility();
        tu.OnTriggerUtilityEnter += OnTriggerUtilityEnter;
        tu.OnTriggerUtilityExit += OnTriggerUtilityExit;
    }

    public void OnDisable()
    {
        tu.OnTriggerUtilityEnter -= OnTriggerUtilityEnter;
        tu.OnTriggerUtilityExit -= OnTriggerUtilityExit;
    }

    private void OnTriggerUtilityEnter(object o, TriggerUtilityEventArgs e)
    {
        TriggerUtilityEnter.Invoke(o, e);
    }

    private void OnTriggerUtilityExit(object o, TriggerUtilityEventArgs e)
    {
        TriggerUtilityExit.Invoke(o, e);
    }
}
