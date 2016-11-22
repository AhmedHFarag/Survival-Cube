using UnityEngine;
using System.Collections;

public class RocketLauncher : DefaultWeapon {
	public GameObject TargetObject;
	public GameObject Missile;
	public GameObject Target_;
	public GameObject SelectedObject;
	public int MissileLimit;
	public int MissileCount;
	public GameObject[] Missiles;
    public GameObject[] enemies;
    public bool isCreated;
    private Improved_Tracer_Rocket rocket;
    bool firstoneDone;
    public GameObject[] TargetCloned;
    public override void Start()
    {
        bulletPool = GameManager.Instance.CreatePool(Missile, BullectNumberInUse, maxBullectNumber);
        FireRate = _DefaultFireRate;
        TargetCloned = new GameObject[100];
}
    void Update (){
        rocket = FindObjectOfType<Improved_Tracer_Rocket>();
        if (enemies.Length < 1)
        {
            SelectedObject = null;
           
        }
        //make a list of all missiles in the scene
        Missiles = GameObject.FindGameObjectsWithTag("Missile");
       
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
       
        //find the length of the list of missiles
       
		MissileCount = Missiles.Length;

        //setup raycast with the screen

        //check if there arent too many missiles in the scene
        EllapsedTime += Time.deltaTime;
        if (MissileCount <= MissileLimit - 1){
                    //find the object you clicked.
                    for (int i = 0; i < enemies.Length; i++)
                    {
         
                    SelectedObject = enemies[i].transform.gameObject;


                TargetObject = SelectedObject; // (GameObject)Instantiate(Target_, enemies[i].transform.position , transform.rotation);

                //set the target object's parent as the selected object
              
                    //TargetObject.transform.parent = SelectedObject.transform;
                
                //TargetObject = SelectedObject;
                //instantiate the target object that the tracer will follow

                //set the target object's parent as the selected object
               //TargetObject.transform.position = SelectedObject.transform.position;

                //print that something just happened to the console

                //Fire Rate To be Deleted Need to be added in weapon class
                if (EllapsedTime > 1 / FireRate)
                {
                    EllapsedTime = 0;
                    Fire();
                    //Destroy(TargetObject);
                    break;

                }
               
               // SelectedObject = null;
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
