using UnityEngine;
using System.Collections;

public class RocketLauncher : DefaultWeapon
{
    public GameObject TargetObject;
    public GameObject Missile;
    public GameObject Target_;
    public GameObject SelectedObject;
    public int MissileLimit;
    public int MissileCount;
    public GameObject[] Missiles;
    public GameObject[] enemies;

    public override void Start()
    {
        bulletPool = GameManager.Instance.CreatePool(Missile, BullectNumberInUse, maxBullectNumber);
        FireRate = _DefaultFireRate;
    }
    void Update()
    {
        if (enemies.Length < 1)
        {
            SelectedObject = null;
        }

       
        //make a list of all missiles in the scene
        Missiles = GameObject.FindGameObjectsWithTag("Missile");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //find the length of the list of missiles
        MissileCount = Missiles.Length;

        EllapsedTime += Time.deltaTime;
        //check if there arent too many missiles in the scene
        if (MissileCount <= MissileLimit - 1)
            {
            
                //find the object you clicked.
                for (int i = 0; i < enemies.Length; i++)
                {
                    
                    SelectedObject = enemies[i].transform.gameObject;
                    //instantiate the target object that the tracer will follow
                    //TargetObject = (GameObject)Instantiate(Target_, SelectedObject.transform.position, transform.rotation);
                    //set the target object's parent as the selected object
                    TargetObject.transform.position= SelectedObject.transform.position;
                
                
                    //Fire Rate To be Deleted Need to be added in weapon class
                    if (EllapsedTime > 1 / FireRate)
                    {
                        EllapsedTime = 0;

                    Fire();
                  
                }
               
              
            }
            }
            
        

        
    }
    public override void Fire()
    {

        foreach (Transform FirePos in FirePositions)
        {
            currentbulletObj = bulletPool.GetObject();

            if (currentbulletObj)
            {
                currentbulletObj.transform.position = FirePos.position;
                currentbulletObj.transform.forward = FirePos.forward;
                currentbulletObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                currentbulletObj.SetActive(true);
                currentbulletObj.GetComponent<Rigidbody>().AddForce(transform.forward * 1);
              
            }

        }
        
    }
}
