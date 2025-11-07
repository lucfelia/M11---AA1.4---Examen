using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float speedRotation = 1f;
    public float stoppingDistance = 0.5f;

    public float normalSpeed = 10f;
    public float acceleration = 0.5f;
    private float actualVelocity;
    private bool bIsKilled = false;

    private Transform player;
    private Animator animator;
    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
       
        if (playerObject != null) player = playerObject.transform;
        
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        LookAt();
        FollowPlayer(distanceToPlayer);
    }

    private void LookAt()
    {
        if (player == null) return;

        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * speedRotation);
    }

    private void FollowPlayer(float playerDistance)
    {
        if (playerDistance > stoppingDistance)
        {
            actualVelocity = Mathf.Min(actualVelocity + acceleration * Time.deltaTime, normalSpeed);

        } else actualVelocity = Mathf.Max(actualVelocity - acceleration * Time.deltaTime, 0f);
       
        
        transform.Translate(Vector3.forward * actualVelocity * Time.deltaTime);

        if (animator != null) animator.SetFloat("Velocity", actualVelocity / normalSpeed);
       
    }

    public void Kill()
    {
        if (bIsKilled == false)
        {
            bIsKilled = true;

            Rigidbody enemyRb = GetComponent<Rigidbody>();
            if (enemyRb != null) enemyRb.isKinematic = true;

            animator.enabled = false;
            
            Collider enemyCollider = GetComponent<Collider>();
            if (enemyCollider != null) enemyCollider.enabled = false;
            
            Rigidbody[] allRb = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in allRb)
            {   rb.isKinematic = false;
                rb.useGravity = true;
            }
            Destroy(gameObject, 2);
            Destroy(this);
        }
    }
}
