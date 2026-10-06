using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform launchPoint;
    private Ball currentBall;

    void Start()
    {
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Ball GetCurrentBall()
    {
        return currentBall;
    }

    private void SpawnBall()
    {
        currentBall = Instantiate(
            ballPrefab,
            launchPoint.position,
            Quaternion.identity
        );
        currentBall.OnDestroyed += OnBallDestroyed;
    }


    public void LaunchCurrentBall(Vector3 velocity)
    {
        if (currentBall == null)
        {
            return;
        }

        Ball ball = currentBall;

        // 手元のボールを空にする
        currentBall = null;

        // 発射
        ball.Launch(velocity);

        // ボールが消えたら次のボールを作る
        Destroy(ball.gameObject, 5f);
    }

    private void OnBallDestroyed(Ball ball)
    {
        // 次のボールを生成
        SpawnBall();
    }
}
