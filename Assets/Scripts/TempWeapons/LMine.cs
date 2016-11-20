using UnityEngine;
using System.Collections;

public class LMine : BulletBehavior {
    public ParticleSystem Explosion;
    public float explosionRadius=5.0f;
    public float explosionForce = 500.0f;
    // Use this for initialization
    void Start () {
        MyRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;
        if (sound)
            AudioSource.PlayClipAtPoint(sound, transform.position, soundVolume);

        Destroy(gameObject);

        if (Explosion)
            Instantiate(Explosion, transform.position, transform.rotation);

        Collider[] objects = UnityEngine.Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
