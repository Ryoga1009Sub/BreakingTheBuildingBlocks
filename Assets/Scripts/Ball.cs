using System;
using Unity.AI.Navigation.LowLevel;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        Debug.Log("マウスクリック");
        rigidbody.useGravity = true;
        rigidbody.AddForce(new Vector3(0,5,13), ForceMode.VelocityChange);
    }
}
