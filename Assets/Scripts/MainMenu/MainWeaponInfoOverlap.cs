using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWeaponInfoOverlap : MonoBehaviour
{
    public Text WeaponName;
    public Image WeaponImage;
    public Button UseButton;
    public Button BuyButton;
    public Button DeleteButton;
    public Button cancelButton;
    public static MainWeaponInfoOverlap Instance;
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
    public void ShowBuy(int WeaponIndex)
    {
        DefaultWeapon temp = GameManager.Instance.Weapons[WeaponIndex].GetComponent<DefaultWeapon>();
        WeaponName.text = temp.Cost.ToString();
        WeaponImage.sprite = temp.UISprite;
        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();


        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    public void ShowUse(int WeaponIndex)
    {
        DefaultWeapon temp = GameManager.Instance.Weapons[WeaponIndex].GetComponent<DefaultWeapon>();
        WeaponName.text = temp.Cost.ToString();
        WeaponImage.sprite = temp.UISprite;
        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();


        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
