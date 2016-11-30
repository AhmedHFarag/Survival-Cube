using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempWeaponButton : MonoBehaviour
{
    public int WeaponIndex;
    Slider coolDownSlider;
    Image ButtonImage;
    // Use this for initialization
    void Start()
    {
        coolDownSlider = GetComponentInChildren<Slider>();
        coolDownSlider.maxValue = GameManager.Instance.weaponCoolDown;
        coolDownSlider.value = coolDownSlider.maxValue;
        ButtonImage = GetComponent<Image>();
        ButtonImage.sprite = GameManager.Instance.TempWeapons[DataHandler.Instance.GetTempWeapon(WeaponIndex)].GetComponent<TempWeapon>().UISprite;
    }
    public void TempWeaponPressed()
    {
        if (Player_Controller.Instance.ActiveTempWeapon(WeaponIndex) && coolDownSlider.maxValue <= coolDownSlider.value)
        {
            coolDownSlider.value = 0;
            StartCoroutine(WeaponCoolDown());
        }
    }
    IEnumerator WeaponCoolDown()
    {
        while (coolDownSlider.maxValue > coolDownSlider.value)
        {
            // set slider 
            coolDownSlider.value++;
            yield return new WaitForSeconds(.5f);
        }
        StopCoroutine(WeaponCoolDown());
    }
    public void OnPointerEnter()
    {
        TempWeaponPressed();
    }
}
