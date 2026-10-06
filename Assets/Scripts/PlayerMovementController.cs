using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private GameObject stage;
    // 回転速度（度/秒）
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] float distance = 5f;
    private Vector2 moveInput;

    // 現在の角度
    private float angle = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        // 左右入力があるときだけ回転
        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            angle += moveInput.x * rotationSpeed * Time.deltaTime;

            // 0～360°に収める
            if (angle >= 360f)
                angle -= 360f;

            if (angle < 0f)
                angle += 360f;

            UpdatePosition();
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void UpdatePosition()
    {
        float radian = angle * Mathf.Deg2Rad;

        // コンパスのようにステージ中心を基準に円周上を移動
        float x = Mathf.Sin(radian) * distance;
        float z = -Mathf.Cos(radian) * distance;

        transform.position = stage.transform.position + new Vector3(
            x,
            transform.position.y,
            z
        );

        // 常にステージの中心を見る
        transform.LookAt(stage.transform.position);
    }
}
