using UnityEngine;
using System.Collections;

public class SlowTime : TempWeapon {
    public float SlowAmount=0.8f;

    void OnEnable()
    {
        StartCoroutine(RestoreSpeed());
    }
	void FixedUpdate () {
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
            enemy.GetComponent<Enemy>().ChangeSpeed(-1);
        }
        Destroy(gameObject);
        Debug.Log("Slow time destroued");
    }
}
