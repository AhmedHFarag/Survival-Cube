using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class MSGScript : MonoBehaviour {

    public Text question;
    public Image iconImage;
    public Button okButton;
    public Button yesButton;
    public Button noButton;
    public Button cancelButton;
    public static MSGScript Instance;
    //public static MSGScript Instance()
    //{
    //    if (!msgbox)
    //    {
    //        msgbox = GameObject.Find("MSGBox").GetComponent<MSGScript>();
    //        if (!msgbox)
    //            Debug.LogError("There needs to be one MSGBox script on a GameObject in your scene.");
    //    }

    //    return msgbox;
    //}

    void OnEnable()
    {
        transform.SetAsLastSibling();
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void Choice(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent)
    {
        gameObject.SetActive(true);
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(noEvent);
        noButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(cancelEvent);
        cancelButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }
    // Ok: A string, a Ok event
    public void OK(string question, UnityAction OkEvent)
    {
        gameObject.SetActive(true);
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(OkEvent);
        okButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
