using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements.Experimental;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private GameObject stage;
    [SerializeField] float speed = 5f;
    [SerializeField] float radius = 5f;
    // ランチャーの初期位置
    private float initialDistance;
    private Vector2 moveInput;

    private Vector3 stageSize;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageSize = stage.GetComponent<Renderer>().bounds.size;

        initialDistance = stage.transform.position.z + transform.position.z;

        Debug.Log($"ステージサイズ{stageSize}  {initialDistance}");
    }

    // Update is called once per frame
    void Update()
    {
        // 左右だけ使用
        float input = moveInput.x;

        if (Mathf.Abs(input) < 0.01f)
            return;

        float diffX = 0f;
        float diffZ = 0f;

        Debug.Log($"ステージサイズ{stage.transform.position}  {transform.transform.position}");

        if (stage.transform.position.x <= transform.position.x || )
        {
            diffX = input * speed * Time.deltaTime;
        }

        // if (stage.transform.position.x + (stageSize.x / 2) <= transform.position.x ||
        //  (stage.transform.position.x + (stageSize.x / 2) <= transform.position.x) && (transform.position.x <= stage.transform.position.x + (stageSize.x / 2) + radius))
        // {
        //     diffX = input * speed * Time.deltaTime;
        // }
        // else if ((stage.transform.position.x - radius <= transform.position.x) && (transform.position.x <= stage.transform.position.x))
        // {
        //     diffX = -(input * speed * Time.deltaTime);
        // }


        if (stage.transform.position.z <= transform.position.z || (stage.transform.position.z < transform.position.z) && (transform.position.z <= stage.transform.position.z + radius))
        {

            diffZ = input * speed * Time.deltaTime;
        }
        else if ((stage.transform.position.z - radius <= transform.position.z) && (transform.position.z <= stage.transform.position.z))
        {
            diffZ = -(input * speed * Time.deltaTime);
        }


        Debug.Log($"diffX: {diffX}  diffZ: {diffZ}");

        // X方向に少しずつ移動
        transform.position += new Vector3(diffX, 0, diffZ);

        print($"位置 {transform.position}");
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
