using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public WaveMode waveMode = WaveMode.WaitingStart;
    public int currentLevel = 0;
    public List<Level> levels = new List<Level>();

	// Use this for initialization
	void Start () {
        levels = new List<Level>();
        levels.AddRange(GetComponentsInChildren<Level>());
        HideAllLevels();
        ShowLevel(0);
        StartLevel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HideAllLevels()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].gameObject.SetActive(false);
        }
    }
    
    private void ShowLevel(int index)
    {
        levels[index].gameObject.SetActive(true);
    }

    public void StartLevel()
    {
        levels[currentLevel].StartLevel();
        waveMode = WaveMode.Combat;
    }

    public void LevelCompleted()
    {
        currentLevel++;
    }

    private void TimerBeforeNext(int indexLevel)
    {
        
    }

    public void LevelLost()
    {
        Debug.Log("Level" + currentLevel.ToString() + " lost");
    }

}

public enum WaveMode
{
    Combat,
    WaitingCombat,
    WaitingStart,
    Pause
}
