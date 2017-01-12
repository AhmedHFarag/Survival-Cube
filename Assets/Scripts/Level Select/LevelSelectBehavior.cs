using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class LevelSelectBehavior : MonoBehaviour
{
    public int MaxLevels;
    public int WavesPerLevel;

    public Text LevelText;
    public GridLayoutGroup WavePanel;
    public Image waveBoostPanel;
    public Sprite WaveLocked;
    public Sprite WaveUnlocked;
    Image[] WaveImage;
    int currentLevel;

    

    // Use this for initialization
    void Start () {
        currentLevel = 1;
        
        LevelText.text = "Level " + DataHandler.Instance.GetLevelNumber(currentLevel-1);
        DataHandler.Instance.GetLevelUnlocked(currentLevel - 1);
        WaveImage = WavePanel.GetComponentsInChildren<Image>();
        RefreshWaves();
    }
	public void NavButtonClicked(int dir)
    {
        if(dir==1 && currentLevel<10)
        {
            currentLevel += 1;
            
        }
        if(dir==-1&& currentLevel>1)
        {
            currentLevel -= 1;
        }
        LevelText.text = "Level " + DataHandler.Instance.GetLevelNumber(currentLevel - 1);
        RefreshWaves();
    }
    void RefreshWaves()
    {
        for(int i=0;i<WaveImage.Length;i++)
        {
           WaveImage[i].GetComponentInChildren<Text>().text= DataHandler.Instance.GetWaveNumber((currentLevel-1) * 10 + i).ToString();
            WaveImage[i].sprite = DataHandler.Instance.GetWaveUnlocked((currentLevel - 1) * 10 + i) == 1 ? WaveUnlocked : WaveLocked;
            WaveImage[i].GetComponent<WaveData>().WaveNumber= DataHandler.Instance.GetWaveNumber((currentLevel - 1) * 10 + i);
            WaveImage[i].GetComponent<WaveData>().Unlocked= DataHandler.Instance.GetWaveUnlocked((currentLevel - 1) * 10 + i);
        }
    }
    public void WaveChoice(WaveData wave)
    {

    }
	// Update is called once per frame
	void Update () {
		
	}
}
