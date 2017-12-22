using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : MonoBehaviour
{
    private WorkPlaceManager workPlaceManager;
    public Townie townie;

    public Transform attachPoint;

    // Workers
    public GameObject basicTowniePrefab;
    public GameObject associatedTowniePrefab;

    // Use this for initialization
    void Start()
    {
        if(basicTowniePrefab == null || associatedTowniePrefab == null)
            Debug.LogError("Missing a prefab.");
        if (attachPoint == null)
            Debug.LogError("Missing a attachPoint.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnWorkerAdded()
    {
        if (townie == null)
        {
            AddTownie(associatedTowniePrefab);
            if(townie is Chopper)
                ((Chopper)townie).StartWork();
        }
        else
        {
            Debug.Log("Can't add a townie where someone is already working.");
        }
    }

    public void OnWorkerRemoved()
    {
        // remove worker
        // add townie
    }

    private void AddTownie(GameObject townieToAdd)
    {
        GameObject current;
        current = Instantiate(townieToAdd, attachPoint.position, attachPoint.rotation);
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
