using UnityEngine;
using UnityEngine.InputSystem;

public class Launcher : MonoBehaviour
{
    [SerializeField] private BallManager ballManager;

    [Header("引っ張り設定")]
    [SerializeField] private float powerRatioY = 0.02f;
    [SerializeField] private float powerRatioX = 0.04f;
    [SerializeField] private float maxDragDistance = 400f;
    [SerializeField] private float maxForce = 25f;

    private Vector3 dragStartPosition;
    private bool isPressing = false;

    private void Update()
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
        // ボールがなければ操作しない
        if (ballManager.GetCurrentBall() == null)
        {
            return;
        }

        isPressing = true;

        dragStartPosition = Mouse.current.position.ReadValue();
    }

    private void OnDragEnd()
    {
        Vector3 dragEndPosition = Mouse.current.position.ReadValue();

        // =========================================
        // ① 今までと同じ「引っ張った方向」を取得
        // =========================================

        Vector3 dragVector = dragStartPosition - dragEndPosition;


        // =========================================
        // ② 引っ張った距離から威力を計算
        // =========================================

        float dragDistance = dragVector.magnitude;

        dragDistance = Mathf.Min(dragDistance, maxDragDistance);

        float power = dragDistance / maxDragDistance;

        float force = power * maxForce;


        // =========================================
        // ③ Launcher基準の発射方向を作る
        //
        // X → Launcherの左右
        // Y → Launcherの上下
        // Z → Launcherの正面
        // =========================================

        Vector3 localLaunchForce = new Vector3(
            dragVector.x * powerRatioX,
            dragVector.y * powerRatioY,
            force
        );


        // =========================================
        // ④ Launcherの現在の向きを反映
        //
        // Launcherが360°回っても、
        // 「Launcherから見た方向」で飛ぶ
        // =========================================

        Vector3 worldLaunchForce = transform.TransformDirection(localLaunchForce);


        // =========================================
        // ⑤ 発射
        // =========================================

        ballManager.LaunchCurrentBall(worldLaunchForce);

        isPressing = false;
    }
}