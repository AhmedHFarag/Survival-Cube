using UnityEngine;
using System.Collections;

public class MaterialLerping : MonoBehaviour {
    public Material material1;
    public Material material2;
    public float duration = 2.0F;
    public Renderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material = material1;
    }
	
	// Update is called once per frame
	void Update () {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.Lerp(material1, material2, lerp);
    }
}
