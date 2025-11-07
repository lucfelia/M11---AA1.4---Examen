using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public GameObject grenade;
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(grenade, transform.position, transform.rotation);
        }
    }
}
