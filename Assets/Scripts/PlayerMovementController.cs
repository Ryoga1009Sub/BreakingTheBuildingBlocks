using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("上下回転させる砲身")]
    [SerializeField] private Transform cannon;

    [Header("左右回転")]
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float minYawAngle = 0f;
    [SerializeField] private float maxYawAngle = 180f;

    [Header("上下回転")]
    [SerializeField] private float pitchSpeed = 45f;
    [SerializeField] private float minPitchAngle = 0f;
    [SerializeField] private float maxPitchAngle = 45f;

    [Header("回転方向")]
    [SerializeField] private bool invertPitchInput = false;
    [SerializeField] private bool invertPitchRotation = false;

    private Vector2 moveInput;

    private float yawAngle;
    private float pitchAngle;

    private Quaternion yawBaseRotation;
    private Quaternion cannonBaseRotation;

    private void Start()
    {
        if (cannon == null)
        {
            Debug.LogError("Cannonが設定されていません！");
            enabled = false;
            return;
        }

        // 左右の初期角度
        Vector3 euler = transform.localEulerAngles;

        yawAngle = Mathf.Clamp(
            euler.y,
            minYawAngle,
            maxYawAngle
        );

        yawBaseRotation = Quaternion.Euler(
            euler.x,
            0f,
            euler.z
        );

        // Cannonの初期回転を保存
        cannonBaseRotation = cannon.localRotation;

        pitchAngle = 0f;

        UpdateRotation();
    }

    private void Update()
    {
        // 左右回転
        yawAngle +=
            moveInput.x * rotationSpeed * Time.deltaTime;

        yawAngle = Mathf.Clamp(
            yawAngle,
            minYawAngle,
            maxYawAngle
        );

        // 上下回転
        float pitchInput = invertPitchInput
            ? -moveInput.y
            : moveInput.y;

        pitchAngle +=
            pitchInput * pitchSpeed * Time.deltaTime;

        pitchAngle = Mathf.Clamp(
            pitchAngle,
            minPitchAngle,
            maxPitchAngle
        );

        UpdateRotation();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void UpdateRotation()
    {
        // 大砲全体の左右回転
        transform.localRotation =
            yawBaseRotation *
            Quaternion.Euler(0f, yawAngle, 0f);

        // 砲身だけをローカルY軸で上下回転
        float rotationSign =
            invertPitchRotation ? -1f : 1f;

        cannon.localRotation =
            cannonBaseRotation *
            Quaternion.AngleAxis(
                pitchAngle * rotationSign,
                Vector3.up
            );
    }
}