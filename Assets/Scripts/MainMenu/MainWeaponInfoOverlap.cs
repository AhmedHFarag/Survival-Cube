﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainWeaponInfoOverlap : MonoBehaviour
{
    public Text WeaponName;
    public Image WeaponImage;
    public Button UseButton;
    public Button BuyButton;
    public Button UpgradeButton;
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
    public void MainWeapon_ShowBuy(int WeaponIndex, UnityAction BuyEvent)
    {
        DefaultWeapon temp = GameManager.Instance.Weapons[WeaponIndex].GetComponent<DefaultWeapon>();
        WeaponName.text = temp.Cost.ToString();
        WeaponImage.sprite = temp.UISprite;

        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.RemoveAllListeners();

        UpgradeButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        BuyButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        BuyButton.onClick.AddListener(BuyEvent);
        BuyButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    public void TempWeapon_ShowBuy(int WeaponIndex, UnityAction BuyEvent)
    {
        TempWeapon temp = GameManager.Instance.TempWeapons[WeaponIndex].GetComponent<TempWeapon>();
        WeaponName.text = temp.UnlockCost.ToString();
        WeaponImage.sprite = temp.UISprite;

        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.RemoveAllListeners();

        UpgradeButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        BuyButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        BuyButton.onClick.AddListener(BuyEvent);
        BuyButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    public void MainWeapon_ShowInfo(int WeaponIndex)
    {
        DefaultWeapon temp = GameManager.Instance.Weapons[WeaponIndex].GetComponent<DefaultWeapon>();
        WeaponName.text = temp.Cost.ToString();
        WeaponImage.sprite = temp.UISprite;

        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.RemoveAllListeners();

        UpgradeButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        cancelButton.gameObject.SetActive(true);
        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    public void MainWeapon_ShowUse(int WeaponIndex, UnityAction UseEvent)
    {
        DefaultWeapon temp = GameManager.Instance.Weapons[WeaponIndex].GetComponent<DefaultWeapon>();
        WeaponName.text = temp.Cost.ToString();
        WeaponImage.sprite = temp.UISprite;

        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.RemoveAllListeners();

        UpgradeButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(false);
        BuyButton.   gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        UseButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        UseButton.onClick.AddListener(UseEvent);
        UseButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    public void TempWeapon_ShowUse(int WeaponIndex, UnityAction UseEvent)
    {
        TempWeapon temp = GameManager.Instance.TempWeapons[WeaponIndex].GetComponent<TempWeapon>();
        WeaponName.text = temp.InGameUseCost.ToString();
        WeaponImage.sprite = temp.UISprite;

        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.RemoveAllListeners();

        UpgradeButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        UseButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        UseButton.onClick.AddListener(UseEvent);
        UseButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    public void MainWeapon_ShowUpgrade(int WeaponIndex, UnityAction UseEvent)
    {
        DefaultWeapon temp = GameManager.Instance.Weapons[WeaponIndex].GetComponent<DefaultWeapon>();
        WeaponName.text = temp.UpgradeCosts[DataHandler.Instance.GetMainWeaponLevel(WeaponIndex)].ToString();
        WeaponImage.sprite = temp.UISprite;

        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.RemoveAllListeners();

        UpgradeButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        UpgradeButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        UpgradeButton.onClick.AddListener(UseEvent);
        UpgradeButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    public void TempWeapon_ShowUpgrade(int WeaponIndex, UnityAction UseEvent)
    {
        TempWeapon temp = GameManager.Instance.TempWeapons[WeaponIndex].GetComponent<TempWeapon>();
        WeaponName.text = temp.UpgradeCost.ToString();
        WeaponImage.sprite = temp.UISprite;

        UseButton.onClick.RemoveAllListeners();
        BuyButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.RemoveAllListeners();

        UpgradeButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        UpgradeButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        UpgradeButton.onClick.AddListener(UseEvent);
        UpgradeButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.AddListener(ClosePanel);
        gameObject.SetActive(true);
    }
    void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
