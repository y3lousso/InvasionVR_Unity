using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGUI : MonoBehaviour {

    public GameObject emptyInterface;
    public GameObject buildedInterface;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(VRTK.VRTK_DeviceFinder.HeadsetCamera())
            transform.LookAt(VRTK.VRTK_DeviceFinder.HeadsetCamera().position);
	}

    public void Show()
    {
        if(GetComponentInParent<TurretZone>().buildedTurret == null)
        {
            emptyInterface.SetActive(true);
            buildedInterface.SetActive(false);
        }else
        {
            emptyInterface.SetActive(false);
            buildedInterface.SetActive(true);
        }
    }

    public void Hide()
    {
        emptyInterface.SetActive(false);
        buildedInterface.SetActive(false);
    }
}
