using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : MonoBehaviour
{
    private WorkPlaceManager workPlaceManager;

    public WorkPlaceType type;
    public Townie townie;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRemoveAdded()
    {
        switch (type)
        {
            case (WorkPlaceType.lake):
                // remove townie
                // add fisherman
                break;
            case (WorkPlaceType.forest):
                // remove townie
                // add chopper
                break;
            case (WorkPlaceType.mine):
                // remove townie
                // add miner
                break;
            default: break;
        }
    }

    public void OnWorkerRemoved()
    {
        // remove worker
        // add townie
    }
}

public enum WorkPlaceType
{
    lake,
    forest,
    mine
}
