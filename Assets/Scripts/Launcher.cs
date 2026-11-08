using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Launcher : MonoBehaviour
{
    [SerializeField] private BallManager ballManager;
    [SerializeField] private TrajectoryRenderer trajectoryRenderer;

    [Header("発射設定")]
    [SerializeField] private float maxDragDistance = 400f;
    [SerializeField] public float maxForce = 25f;

    private Vector2 dragStartPosition;
    private bool isPressing;
    private int activeTouchId = -1;

    private void Update()
    {
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            HandleTouch();
        }
        else if (activeTouchId >= 0)
        {
            HandleTouch();
        }
        else if (Mouse.current != null)
        {
            HandleMouse();
        }
    }

    private void HandleMouse()
    {
        var mouse = Mouse.current;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (IsPointerOverUI(-1)) return;

            StartDrag(mouse.position.ReadValue());
        }

        if (isPressing)
        {
            UpdateDrag(mouse.position.ReadValue());

            if (mouse.leftButton.wasReleasedThisFrame)
                EndDrag(mouse.position.ReadValue());
        }
    }

    private void HandleTouch()
    {
        var touchscreen = Touchscreen.current;
        var touch = touchscreen.primaryTouch;

        int touchId = touch.touchId.ReadValue();

        if (touch.press.wasPressedThisFrame)
        {
            if (IsPointerOverUI(touchId)) return;

            activeTouchId = touchId;
            StartDrag(touch.position.ReadValue());
        }

        if (isPressing && activeTouchId == touchId)
        {
            UpdateDrag(touch.position.ReadValue());

            if (touch.press.wasReleasedThisFrame)
            {
                EndDrag(touch.position.ReadValue());
                activeTouchId = -1;
            }
        }
    }

    private void StartDrag(Vector2 position)
    {
        if (isPressing) return;

        isPressing = true;
        dragStartPosition = position;

        trajectoryRenderer.SetDragging(true, 0f);
    }

    private void UpdateDrag(Vector2 position)
    {
        float force = CalculateForce(position);
        trajectoryRenderer.SetDragging(true, force);
    }

    private void EndDrag(Vector2 position)
    {
        float force = CalculateForce(position);

        if (force > 0f)
            ballManager.SpawnAndLaunchBall(force);

        isPressing = false;
        trajectoryRenderer.SetDragging(false, 0f);
    }

    private float CalculateForce(Vector2 position)
    {
        float distance = Vector2.Distance(
            dragStartPosition,
            position
        );

        return Mathf.Clamp01(distance / maxDragDistance) * maxForce;
    }

    private bool IsPointerOverUI(int pointerId)
    {
        if (EventSystem.current == null) return false;

        return EventSystem.current.IsPointerOverGameObject(pointerId);
    }
}