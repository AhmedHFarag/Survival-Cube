using UnityEngine;
using System.Collections;

public class MaterialLerping : MonoBehaviour {
    public Material material1;
    public Material material2;
    public float duration = 2.0F;
    private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = material1;
    }
    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.Lerp(material1, material2, lerp);
    }
}
