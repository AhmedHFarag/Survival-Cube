using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Text Score;
	void Start () {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            Score.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        }
        else
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("BestScore", 0);
        }

    }
	
	void Update () {
	
	}
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
