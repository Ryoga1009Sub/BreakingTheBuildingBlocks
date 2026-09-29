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
    private float maxDragDistance = 400f;
    private float maxForce = 25f;


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

        // 力加減計算
        // 引っ張った距離
        float dragDistance = dragVector.magnitude;
        // 最大距離で制限
        dragDistance = Mathf.Min(dragDistance, maxDragDistance);
        // 0～1に変換
        float power = dragDistance / maxDragDistance;
        // 実際の発射速度
        float force = power * maxForce;


        Debug.Log($"{dragStartposition},  {dragEndPosition} {force}");

        rigidbody.useGravity = true;

        Vector3 launchForce = new Vector3(dragVector.x * powerRatioX, dragVector.y * powerRatioY, force);
        Debug.Log($"{launchForce}");
        // ボールに瞬間的な力を加える（3DのImpulseモード）
        rigidbody.AddForce(launchForce, ForceMode.Impulse);
    }
}
