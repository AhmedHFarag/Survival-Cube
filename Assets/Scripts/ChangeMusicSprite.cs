using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusicSprite : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public Sprite mute;
    public Sprite play;
    Image myImageComponent;
    // Use this for initialization
    void Start () {
    
        myImageComponent = GetComponent<Image>();
      
    }
    public void change(Sprite differentSprite)
    {
        spriteRenderer.sprite = differentSprite; //sets sprite renderers sprite
    }
    // Update is called once per frame
    void Update () {
        if (AudioListener.volume == 0)
        {
            myImageComponent.sprite = mute;
        }
        else
            myImageComponent.sprite = play;

    }
}
