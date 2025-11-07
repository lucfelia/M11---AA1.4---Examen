using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   public float speed;
   public float speedRotation;

   public Transform target;
   private string enemyTag = "Enemy";


   void Update()
   {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float vertialMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0, vertialMovement);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        LookAt();
   }

    private void LookAt()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform closest = null;

        float bestDist = Mathf.Infinity;

        foreach (var e in enemies)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < bestDist)
            {
                bestDist = distance;
                closest = e.transform;
            }
        }

        if (closest != null)
        {
            Vector3 direction = closest.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * speedRotation);
            }
            if (target != null) target.position = closest.position;
        }
        else
        {
            if (target != null) target.position = transform.position;
        }
    }
}