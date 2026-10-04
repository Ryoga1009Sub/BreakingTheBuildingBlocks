using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Transform stageCenter;
    [SerializeField] float speed = 5f;
    [SerializeField] private float radius = 2f;
    private Vector2 moveInput;
    private float angle;
    // ランチャーの初期位置
    private Vector3 initialPosition;

    // ステージ中心から見た初期位置
    private Vector3 initialOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ★ ゲーム開始時の位置をそのまま保存
        initialPosition = transform.position;

        // ステージ中心から初期位置への差
        initialOffset = initialPosition - stageCenter.position;

        // 初期位置の角度
        angle = Mathf.Atan2(
            initialOffset.z,
            initialOffset.x
        );
    }

    // Update is called once per frame
    void Update()
    {
        // 左右だけ使用
        float input = moveInput.x;

        // スティックを離しているなら何もしない
        if (Mathf.Abs(input) < 0.01f)
            return;

        // 現在の角度から少しだけ移動
        angle += input * speed * Time.deltaTime;

        // 円周上の位置
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // ★ Yは初期位置のYをそのまま使用
        transform.position = new Vector3(
            stageCenter.position.x + x,
            initialPosition.y,
            stageCenter.position.z + z
        );
        print($"位置 {transform.position}");
    }

    public void OnMove(InputValue value)
    {
        print($"controller {value.Get<Vector2>()}");
        moveInput = value.Get<Vector2>();
    }
}
