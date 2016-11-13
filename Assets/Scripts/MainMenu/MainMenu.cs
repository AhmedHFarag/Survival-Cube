using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Text Score;
    public Text Coins;
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
        if (PlayerPrefs.HasKey("Coins"))
        {
            Coins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
            //GameManager.Instance.Coins = PlayerPrefs.GetInt("Coins", 0);
            DataHandler.Instance.playerCoins = PlayerPrefs.GetInt("playerCoins", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Coins", 0);
        }

    }
	
	void Update () {
	
	}
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
