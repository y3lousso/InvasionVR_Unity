using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(VRTK.VRTK_InteractableObject))]
public class Townie : MonoBehaviour {

    protected WorkPlace workPlace;
    protected Animator animator;
    protected VRTK.VRTK_InteractableObject interactableObject;

    protected virtual void Awake()
    {
        // Init components
        animator = GetComponent<Animator>();
        interactableObject = GetComponent<VRTK.VRTK_InteractableObject>();

        // Subscribe to events
        interactableObject.InteractableObjectGrabbed += OnTownieGrabbed;
    }

    protected virtual void OnTownieGrabbed(object sender, VRTK.InteractableObjectEventArgs e)
    {
        if(workPlace != null)
        {
            workPlace.OnWorkerRemoved(sender, e);
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void StartWork()
    {

    }

    public virtual void StopWork()
    {
        
    }

    public void SetWorkPlace(WorkPlace _workPlace) { workPlace = _workPlace; }
  
}
