using UnityEngine;
using UnityEngine.InputSystem;


public class Launcher : MonoBehaviour
{
    [SerializeField] private BallManager ballManager;

    private Vector3 dragStartposition;
    private float powerRatioY = 0.02f; // 引っ張る方向Y計算用　倍率
    private float powerRatioX = 0.04f; // 引っ張る方向X計算用　倍率
    private bool isPressing = false; // ドラッグ中かどうか
    private float maxDragDistance = 400f; // 発射速度計算用　引っ張る距離の上限
    private float maxForce = 25f; // 最大発射速度

    void Start()
    {

    }

    // Update is called once per frame
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
        // ボールがなければ操作なし
        if (ballManager.GetCurrentBall() == null)
        {
            return;
        }
        isPressing = true;
        dragStartposition = Mouse.current.position.ReadValue();
    }

    private void OnDragEnd()
    {
        Debug.Log("マウスリリース");

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

        Vector3 launchForce = new Vector3(dragVector.x * powerRatioX, dragVector.y * powerRatioY, force);

        // ボールマネージャーにボールを発射させる
        ballManager.LaunchCurrentBall(launchForce);
        isPressing = false;
    }

}
