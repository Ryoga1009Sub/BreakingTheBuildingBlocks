using System;
using NUnit.Framework;
using Unity.AI.Navigation.LowLevel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class Ball : MonoBehaviour
{

    private float lifeTime = 3f; // ボールの生存時間

    public event Action<Ball> OnDestroyed;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {

    }

    public void Launch(Vector3 velocity)
    {

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(velocity, ForceMode.Impulse);

        rb.useGravity = true;
        // 射出された瞬間から5秒後に消す
        Invoke(nameof(DestroyBall), lifeTime);
    }

    private void DestroyBall()
    {
        OnDestroyed?.Invoke(this);

        Destroy(gameObject);
    }
}
