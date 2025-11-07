using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask mask;
    public float launchForce;
    public float timer;
    public float radius;
    public float explosionForce;
    public GameObject particles;
    void Start()
    {

    }
}
