using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : MonoBehaviour
{
    private WorkPlaceManager workPlaceManager;
    private Townie townie;

    private VRTK.VRTK_SnapDropZone snapZone;

    // Workers
    public GameObject basicTowniePrefab;
    public GameObject associatedTowniePrefab;

    private void Awake()
    {
        snapZone = GetComponentInChildren<VRTK.VRTK_SnapDropZone>();
        snapZone.ObjectSnappedToDropZone += OnWorkerAdded;
    }

    // Use this for initialization
    void Start()
    {      
        if (basicTowniePrefab == null || associatedTowniePrefab == null)
            Debug.LogError("Missing a prefab.");
        if (snapZone == null)
            Debug.LogError("Missing a attachPoint.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnWorkerAdded(object o, VRTK.SnapDropZoneEventArgs e)
    {
        Destroy(e.snappedObject);
        if (townie == null)
        {
            AddTownie();
            if (townie is Chopper)
                ((Chopper)townie).StartWork();             
        }
        else
        {
            Debug.Log("Can't add a townie where someone is already working.");
        }
    }

    public void OnWorkerRemoved(object o, VRTK.InteractableObjectEventArgs e)
    {
        VRTK.VRTK_InteractGrab controller = e.interactingObject.GetComponent<VRTK.VRTK_InteractGrab>();       
        Transform currentTransform = townie.transform;
        Destroy(townie.gameObject);
        GameObject current = Instantiate(basicTowniePrefab, currentTransform.position, currentTransform.rotation);
        controller.AttemptGrab();
    }

    private void AddTownie()
    {
        GameObject current = Instantiate(associatedTowniePrefab, snapZone.transform.position, snapZone.transform.rotation);
        if (current == null)
        {
            Debug.LogError("Prefab doesn't have a townie component.");
        }
        else
        {
            townie = current.GetComponent<Townie>();
            townie.SetWorkPlace(this);
        }
    }
}
