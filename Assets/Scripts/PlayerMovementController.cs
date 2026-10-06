using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private GameObject stage;
    [SerializeField] float speed = 5f;
    [SerializeField] float distance = 5f;
    private Vector2 moveInput;

    private Vector3 stageSize;

    // 現在の移動方向
    private float directionX = 1f;
    private float directionZ = 1f;

    // 最大可動範囲
    private float maxX;
    private float minX;
    private float maxZ;
    private float minZ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageSize = stage.GetComponent<Renderer>().bounds.size;


        maxX = stage.transform.position.x + stageSize.x + distance;
        minX = stage.transform.position.x - stageSize.x - distance;

        maxZ = stage.transform.position.z + stageSize.z + distance;
        minZ = stage.transform.position.z - stageSize.z - distance;
    }

    // Update is called once per frame
    void Update()
    {
        // 左右だけ使用
        float input = moveInput.x;

        // 入力中のみ移動させる
        if (Mathf.Abs(input) < 0.01f)
            return;


        Debug.Log($"ステージサイズ{stage.transform.position}  {transform.transform.position}");

        // X座標の往復移動
        float newX = transform.position.x + directionX * speed * input;
        if (newX >= maxX)
        {
            newX = maxX;
            directionX = -1f;
        }
        else if (newX <= minX)
        {
            newX = minX;
            directionX = 1f;
        }

        // Z座標の往復移動
        float newZ = transform.position.z + directionZ * speed * input;
        if (newZ >= maxZ)
        {
            newZ = maxZ;
            directionZ = -1f;
        }
        else if (newZ <= minZ)
        {
            newZ = minZ;
            directionZ = 1f;
        }


        transform.position = new Vector3(
            newX,
            transform.position.y,
            newZ
        );

        transform.LookAt(stage.transform.position);

        print($"位置 {transform.position}");
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
