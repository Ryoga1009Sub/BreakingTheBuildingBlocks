using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Launcher launcher;
    [SerializeField] private Rigidbody ballRigidbody;

    [Header("軌道設定")]
    [SerializeField] private int pointCount = 40;
    [SerializeField] private float timeStep = 0.05f;

    private LineRenderer lineRenderer;

    private bool isAiming;
    private bool isDragging;
    private float dragForce;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
    }

    private void LateUpdate()
    {
        if (!isAiming && !isDragging)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;

        // ドラッグ中は現在の威力、それ以外は最大威力
        float force = isDragging ? dragForce : launcher.maxForce;

        DrawTrajectory(force);
    }

    public void SetAiming(bool aiming)
    {
        isAiming = aiming;
    }

    public void SetDragging(bool dragging, float force)
    {
        isDragging = dragging;
        dragForce = force;
    }

    private void DrawTrajectory(float force)
    {
        Vector3 startPosition = launchPoint.position;

        // ForceMode.Impulse に合わせて質量を考慮
        Vector3 velocity =
            launchPoint.forward * (force / ballRigidbody.mass);

        Vector3 gravity = Physics.gravity;

        lineRenderer.positionCount = pointCount;

        for (int i = 0; i < pointCount; i++)
        {
            float t = i * timeStep;

            Vector3 position =
                startPosition +
                velocity * t +
                0.5f * gravity * t * t;

            lineRenderer.SetPosition(i, position);
        }
    }
}