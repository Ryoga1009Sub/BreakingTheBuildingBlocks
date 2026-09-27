using System;
using Unity.AI.Navigation.LowLevel;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,5,13), ForceMode.VelocityChange);
    }
}
