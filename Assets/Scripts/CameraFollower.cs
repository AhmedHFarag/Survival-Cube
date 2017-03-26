using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    public Transform Player;
    Vector3 startpos;
    Vector3 currentpos;
    float offsetX;
    float offsetZ;
	// Use this for initialization
	void Start () {
        startpos = transform.position;
        offsetX = transform.position.x - Player.position.x;
        offsetZ = transform.position.z - Player.position.z;
        currentpos = new Vector3(startpos.x - offsetX, startpos.y, startpos.z - offsetZ);

    }

    // Update is called once per frame
    void Update ()
    {
        
        transform.position = new Vector3(Mathf.Lerp(currentpos.x, Player.position.x+offsetX,Time.deltaTime*2),startpos.y,Mathf.Lerp(currentpos.z,Player.position.z+offsetZ,Time.deltaTime*2));
        currentpos = transform.position;
            //if (Player.position != currentpos)
        //{
        //    Vector3 pos = Player.position + Player.forward;
        //    transform.position = Vector3.Lerp(currentpos,new Vector3(pos.x, startpos.y, pos.z),Time.deltaTime);
            

        //}
        //else
        //{
        //    currentpos = Player.position;
        //}
	}
}
