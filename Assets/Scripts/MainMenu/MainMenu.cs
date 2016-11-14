using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public static MainMenu instance;
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
        if (PlayerPrefs.HasKey("playerCoins"))
        {
            Coins.text = PlayerPrefs.GetInt("playerCoins", 0).ToString();
            //GameManager.Instance.Coins = PlayerPrefs.GetInt("Coins", 0);
            DataHandler.Instance.playerCoins = PlayerPrefs.GetInt("playerCoins", 0);
        }
        else
        {
            PlayerPrefs.SetInt("playerCoins", 0);
        }
    }
	
	void Update () {
	
	}
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
