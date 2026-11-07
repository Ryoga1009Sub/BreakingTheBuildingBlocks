using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    // 回転速度（度/秒）
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Transform stage;

    private Vector2 moveInput;

    private Quaternion initialRotation;


    // 現在の角度
    private float angle = 0f;

    void Start()
    {
        Vector3 direction = stage.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // 初期の向きを保存
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // 左右入力があるときだけ回転
        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            angle += moveInput.x * rotationSpeed * Time.deltaTime;

            // 0～360°に収める
            if (angle >= 180f)
                angle = 180f;

            if (angle <= 0f)
                angle = 0f;


            t // 位置は変えず、Y軸方向だけ回転
            transform.localRotation =
                initialRotation *
                Quaternion.Euler(0f, angle, 0f);
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
