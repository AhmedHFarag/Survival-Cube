using UnityEngine;
using System.Collections;

public class SlowTime : TempWeapon {
    public float SlowAmount=0.8f;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject enemy in Enemies_Manager.Instance.activeEnemies)
        {
            enemy.GetComponent<Enemy>().ChangeSpeed(SlowAmount);
        }
    }
    IEnumerator RestoreSpeed()
    {
        yield return new WaitForSeconds(LifeTime);
        foreach (GameObject enemy in Enemies_Manager.Instance.activeEnemies)
        {
            enemy.GetComponent<Enemy>().ChangeSpeed(1/SlowAmount);
        }
        gameObject.SetActive(false);
    }
}
