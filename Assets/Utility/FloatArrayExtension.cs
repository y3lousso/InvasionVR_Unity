using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatArrayExtension {

	public static float Sample(this float[] fArr, float t)
    {
        int count = fArr.Length;
        if(count == 0)
        {
            Debug.LogError("Unable to sample an empty array.");
            return 0;
        }
        if(count == 1)
        {
            return fArr[0];
        }
        float iFloat = t * (count - 1);
        int idLower = Mathf.FloorToInt(iFloat);
        int idUpper = Mathf.FloorToInt(iFloat + 1);
        if(idUpper >= count)
        {
            return fArr[count - 1];
        }
        if(idLower < 0)
        {
            return fArr[0];
        }
        return Mathf.Lerp(fArr[idLower], fArr[idUpper], iFloat - idLower);
    }
}
