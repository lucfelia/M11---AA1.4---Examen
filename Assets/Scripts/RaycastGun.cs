using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public LineRenderer line;
    public float lineFadeSpeed;
    public LayerMask mask;
    public float knockbackForce = 2;

    void Update()
    {
       line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a - Time.deltaTime * lineFadeSpeed);
       line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, line.endColor.a - Time.deltaTime * lineFadeSpeed);
            
       if (Input.GetButtonDown("Fire1"))
       {
            line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, 1);
            line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, 1);
                
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * 1000);
                
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity,mask))
            {
                line.SetPosition(1, hit.point);
                GameObject hitObject = hit.collider.gameObject;

                EnemyController enemy = hitObject.GetComponent<EnemyController>();
                if (enemy != null) enemy.Kill();
                

                Rigidbody rb = hitObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = hit.point - transform.position;
                    rb.AddForce(forceDirection.normalized * knockbackForce, ForceMode.Impulse);
                }
            }
       }
    }
}
