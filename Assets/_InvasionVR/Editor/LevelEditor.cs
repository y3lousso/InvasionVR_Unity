using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
[CanEditMultipleObjects]
public class LevelEditor : Editor {

    private Level level;
    private int waveNumber;

    public override void OnInspectorGUI()
    {
        level = target as Level;
        base.OnInspectorGUI();

        /// ATTENTION AU CRASH
        /*if(waveNumber != level.waveNumber)
        {
            level.ActualizeWaveNumber(level.waveNumber);
            waveNumber = level.waveNumber;
        }*/
        
    }

}
