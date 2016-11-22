using UnityEngine;
using System.Collections;

public class TempWeapon : MonoBehaviour {
    public int Damage;
    public int Cost;
    public int LifeTime = 5;
    void OnEnable()
    {
        StartCoroutine(DistroyAfter(LifeTime));
    }
    void Start () {
	
	}
	void Update () {
	
	}
    public virtual void SelfInitialize(GameObject _obj)
    {
        transform.rotation = _obj.transform.rotation;
        transform.position = Player_Controller.Instance.WeaponPos.position;
        transform.parent = _obj.transform;
        gameObject.SetActive(true);
    }
    IEnumerator DistroyAfter(int LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Player_Controller.Instance.TempInUse = false;
        Destroy(gameObject);

    }
}
