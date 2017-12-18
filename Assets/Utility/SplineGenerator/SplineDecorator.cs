using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BezierSpline))]
public class SplineDecorator : MonoBehaviour {

    public BezierSpline spline;

    public int frequency;
    public bool lookForward;
    public GameObject[] prefabs;

    private List<GameObject> currentInstantiatedPrefabs;

    private Vector3 lastPoint;
    
    public void UpdateDecoration()
    {
        if (currentInstantiatedPrefabs != null)
        {
            ClearDecoration();
        }
        currentInstantiatedPrefabs = new List<GameObject>();
        if (frequency <= 0 || prefabs == null || prefabs.Length == 0)
        {
            return;
        }
        float stepSize = frequency * prefabs.Length;
        if (spline.Loop || stepSize == 1)
        {
            stepSize = 1f / stepSize;
        }
        else
        {
            stepSize = 1f / (stepSize - 1);
        }
        for (int i = 0; i < spline.ptsDistances.Length; i++)
        {
            Debug.Log(spline.ptsDistances[i]);
        }
        
        for (int  f = 0, p = 0; f < frequency; f++)
        {
            for (int i = 0; i < prefabs.Length; i++, p++)
            {
                GameObject go = Instantiate(prefabs[i]) as GameObject;
                currentInstantiatedPrefabs.Add(go);
                spline.CalcLengthTableInto();
                
                Debug.Log("Normal : " + p * stepSize);
                Debug.Log("Modifier : " + FloatArrayExtension.Sample(spline.ptsDistances, p * stepSize));
                Vector3 position = spline.GetPoint(FloatArrayExtension.Sample(spline.ptsDistances, p * stepSize));
                Vector3 position1 = spline.GetPoint( p * stepSize);
                go.transform.localPosition = position1;
                if (lookForward)
                {
                    go.transform.LookAt(position1 + spline.GetTangent(p * stepSize));
                }
                go.transform.parent = transform;
            }
        }
    }

    private void ClearDecoration()
    {
        for (int i = 0; i < currentInstantiatedPrefabs.Count; i++)
        {
            DestroyImmediate(currentInstantiatedPrefabs[i]);
        }
    }

    public void ForceClearDecoration()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }


    // Use this for initialization
    void Start () {
        lastPoint = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
