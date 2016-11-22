using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(LineRenderer))]

public class LaserBehaviourScript : MonoBehaviour
{

    public float laserWidth = 1.0f;
    public float noise = 1.0f;
    public float maxLength = 50.0f;
    public Color color = Color.red;
    public int Damage=5;

    float ElapsedTime = 0;
    LineRenderer lineRenderer;
    int length;
    Vector3[] positions;
    //Cache any transforms here
    Transform myTransform;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(laserWidth, laserWidth);
        myTransform = transform;
        offset = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RenderLaser();
    }

    void RenderLaser()
    {

        //Shoot our laserbeam forwards!
        UpdateLength();

        ElapsedTime += Time.deltaTime;
        lineRenderer.SetColors(color, color);
        //Move through the Array
        for (int i = 0; i < length; i++)
        {
            //Set the position here to the current location and project it in the forward direction of the object it is attached to
            offset.x = myTransform.position.x + i * myTransform.forward.x / 2 + Random.Range(-noise, noise);
            offset.y = myTransform.position.y + i * myTransform.forward.y / 2 + Random.Range(-noise, noise);
            offset.z = myTransform.position.z + i * myTransform.forward.z / 2 + Random.Range(-noise, noise); ;
            positions[i] = offset;
            positions[0] = myTransform.position;

            lineRenderer.SetPosition(i, positions[i]);
        }
    }

    void UpdateLength()
    {
        //Raycast from the location of the cube forwards
        RaycastHit[] hit;
        hit = Physics.RaycastAll(myTransform.position, myTransform.forward, maxLength);
        int i = 0;
        while (i < hit.Length)
        {
            //Check to make sure we aren't hitting triggers but colliders
            if (!hit[i].collider.isTrigger && hit[i].collider.tag != "Bullet")
            {
                length = (int)Mathf.Round(hit[i].distance * 2) + 2;
                positions = new Vector3[length];
                //Move our End Effect particle system to the hit point and start playing it

                lineRenderer.SetVertexCount(length);

                if (hit[i].collider.tag == "Enemy")
                {
                    hit[i].collider.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
                }
                if (hit[i].collider.tag == "Item")
                {
                    hit[i].collider.gameObject.GetComponent<UpgradeBuffs>().Hit();
                }
                return;
            }
            i++;
        }
        length = (int)maxLength;
        positions = new Vector3[length];
        lineRenderer.SetVertexCount(length);


    }
}