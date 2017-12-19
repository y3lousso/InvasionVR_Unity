using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townie : MonoBehaviour {

    protected WorkPlace workPlace;
    protected Animator animator;
   
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
