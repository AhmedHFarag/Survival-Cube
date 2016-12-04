using UnityEngine;
using System.Collections;

public class Rocket : BulletBehavior {

    public ParticleSystem Explosion;
    public float explosionRadius = 10;
    public float explosionForce = 10000.0f;
    void OnEnable()
    {
        StartCoroutine(SelfDestory());
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    // Use this for initialization
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            StopCoroutine(SelfDestory());
            StartCoroutine(Destory());
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
    IEnumerator Destory()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(timeForSelfDestory);
        MyRigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

}
