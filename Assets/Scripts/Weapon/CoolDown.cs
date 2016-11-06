using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CoolDown : MonoBehaviour
{
    public Slider coolDownSlider;
    public Text weaponCost;

    float coolDownCounter = 0;
    float coolDownMaxValue;

    // Use this for initialization
    void Start()
    {
        coolDownMaxValue = GameManager.Instance.weaponCoolDown;
        coolDownSlider.maxValue = GameManager.Instance.weaponCoolDown;
        coolDownSlider.value = coolDownSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WeaponCoolDownMethod()
    {
        Debug.Log("Hi");
        if (int.Parse( weaponCost.text) <= GameManager.Instance.InGameCoins)
        {
            GetComponent<EventTrigger>().enabled = false;
            coolDownSlider.value = 0;
            StartCoroutine(WeaponCoolDown());
        }
        
    }


    IEnumerator WeaponCoolDown()
    {

        while (coolDownMaxValue > coolDownSlider.value)
        {
            // set slider 
            coolDownSlider.value++;
            yield return new WaitForSeconds(.5f);
        }
        Debug.Log("finished");
        GetComponent<EventTrigger>().enabled= true;
        StopCoroutine("WeaponCoolDown");

    }
}
