using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("照準設定")]
    [SerializeField] private float rotationSpeed = 90f;

    [Header("回転制限")]
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 180f;

    private Vector2 moveInput;

    // 現在のY軸回転角度
    private float angle;

    // 初期のX・Z回転を保持
    private float initialX;
    private float initialZ;

    private void Start()
    {
        // Inspectorで設定した初期角度を取得
        Vector3 euler = transform.localEulerAngles;

        initialX = euler.x;
        initialZ = euler.z;

        // 初期Y角度を取得（90°を想定）
        angle = Mathf.Clamp(euler.y, minAngle, maxAngle);

        UpdateRotation();
    }

    private void Update()
    {
        // 左右入力があるときだけ回転
        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            angle += moveInput.x * rotationSpeed * Time.deltaTime;

            print($"aaaaaa {angle}");


            // 0～180°の範囲に制限
            angle = Mathf.Clamp(angle, minAngle, maxAngle);

            UpdateRotation();
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void UpdateRotation()
    {
        transform.localRotation = Quaternion.Euler(
            initialX,
            angle,
            initialZ
        );
    }
}