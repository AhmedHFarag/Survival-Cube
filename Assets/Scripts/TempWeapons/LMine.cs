using UnityEngine;
using System.Collections;

public class LMine : BulletBehavior {
    public Light LED;
    public ParticleSystem Explosion;
    public float explosionRadius=10;
    public float explosionForce = 10000.0f;
    float Ellapsedtime = 0;
    void OnEnable()
    {
    }
    void Start () {
        MyRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Ellapsedtime>=1)
        {
            Ellapsedtime = 0;
            LED.enabled = !LED.enabled;
        }
        Ellapsedtime += Time.deltaTime;
	}
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            StartCoroutine(SelfDestory());
            if (sound)
                AudioSource.PlayClipAtPoint(sound, transform.position, soundVolume);
            if (Explosion)
                Explosion.Play();

            
            Collider[] objects = UnityEngine.Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider h in objects)
            {
                Rigidbody r = h.GetComponent<Rigidbody>();
                if (r != null && h.CompareTag("Enemy"))
                {
                    r.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    h.gameObject.GetComponent<Enemy>().TakeDamage((int)Mathf.Floor(Damage * Player_Controller.Instance.DamageMultiplier));
                }

            }
        }
    }
    IEnumerator SelfDestory()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(timeForSelfDestory);
        Destroy(gameObject);
    }
}
