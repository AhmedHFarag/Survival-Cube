using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public static MainMenu instance;
    public Text Score;
    public Text Coins;
    void Start () {
        
    }
	
	void Update () {
	
	}
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
