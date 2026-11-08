using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform launchPoint;

    public void SpawnAndLaunchBall(float force)
    {
        // 発射する瞬間にボールを生成
        Ball ball = Instantiate(
            ballPrefab,
            launchPoint.position,
            launchPoint.rotation
        );

        // 砲身の向きに発射
        Vector3 direction = launchPoint.forward.normalized;

        ball.Launch(direction * force);
    }
}