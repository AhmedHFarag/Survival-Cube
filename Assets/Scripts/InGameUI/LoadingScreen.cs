using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour {
    public string[] Hints;
    public Text hint;
    public Animator TextAnim;
	// Use this for initialization
	void Start () {
        if(TextAnim==null)
        {
            TextAnim = hint.gameObject.GetComponent<Animator>();
        }
        StartCoroutine("HintRoll");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator HintRoll()
    {
        int index = Random.Range(0, Hints.Length);
        hint.text = Hints[index];
        while(true)
        {
            yield return new WaitForSeconds(5);


            hint.text = Hints[Random.Range(0, Hints.Length)];

        }
        yield return null;
    }
}
