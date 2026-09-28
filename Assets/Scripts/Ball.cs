using System;
using NUnit.Framework;
using Unity.AI.Navigation.LowLevel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class Ball : MonoBehaviour
{

    private Rigidbody rigidbody;
    private Vector3 dragStartposition;
    private float powerRatioY = 0.02f;
    private float powerRatioX = 0.04f;

    private bool isPressing = false;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 画面をクリックした瞬間
        if (Mouse.current.leftButton.wasPressedThisFrame && !isPressing)
        {
            OnDragStart();
        }

        // クリックを離した瞬間
        if (isPressing && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            OnDragEnd();
        }
    }


    private void OnDragStart()
    {
        Debug.Log("マウスドラッギング");
        isPressing = true;
        dragStartposition = Mouse.current.position.ReadValue();
    }

    private void OnDragEnd()
    {
        Debug.Log("マウスリリース");
        isPressing = false;

        Vector3 dragEndPosition = Mouse.current.position.ReadValue();

        // 引っ張ったベクトルを計算（開始位置 - 終了位置）
        Vector3 dragVector = dragStartposition - dragEndPosition;

        rigidbody.useGravity = true;

        Vector3 launchForce = new Vector3(dragVector.x * powerRatioX, dragVector.y * powerRatioY, 15f);
        Debug.Log($"{launchForce}");
        // ボールに瞬間的な力を加える（3DのImpulseモード）
        rigidbody.AddForce(launchForce, ForceMode.Impulse);
    }
}
