using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask mask;
    public float launchForce;

    public float timer = 0.3f;
    public float radius;
    public float explosionForce;

    public float explosionUpwardsModifier = 1f;

    public GameObject particles;

    void Start()
    {

        float randomRange = Random.Range(-1f, 1f);

        rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
        
        Vector3 randomRotationForce = new Vector3(randomRange, randomRange, randomRange);

        rb.AddTorque(randomRotationForce * launchForce, ForceMode.Impulse);

        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(timer);
        Explode();
    }

    private void Explode()
    {
        Instantiate(particles, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, mask);

        foreach (Collider collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.Kill();
            }

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius, explosionUpwardsModifier, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
