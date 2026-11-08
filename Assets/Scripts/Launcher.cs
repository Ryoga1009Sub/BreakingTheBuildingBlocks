using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Launcher : MonoBehaviour
{
    [SerializeField] private BallManager ballManager;

    [Header("発射設定")]
    [SerializeField] private float maxDragDistance = 400f;
    [SerializeField] private float maxForce = 25f;

    private Vector2 dragStartPosition;
    private bool isPressing = false;

    private void Update()
    {
        if (Mouse.current == null) return;

        // ドラッグ開始
        if (Mouse.current.leftButton.wasPressedThisFrame && !isPressing)
        {
            // UI上で押した場合は発射操作を開始しない
            if (IsPointerOverUI()) return;

            isPressing = true;
            dragStartPosition = Mouse.current.position.ReadValue();
        }

        // 指を離した瞬間
        if (isPressing && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            OnDragEnd();
        }
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;

        return EventSystem.current.IsPointerOverGameObject();
    }

    private void OnDragEnd()
    {
        Vector2 dragEndPosition =
            Mouse.current.position.ReadValue();

        // ドラッグ距離だけ使用
        float dragDistance =
            Vector2.Distance(dragStartPosition, dragEndPosition);

        dragDistance = Mathf.Min(dragDistance, maxDragDistance);

        float power = dragDistance / maxDragDistance;
        float force = power * maxForce;

        if (force > 0f)
        {
            ballManager.SpawnAndLaunchBall(force);
        }

        isPressing = false;
    }
}