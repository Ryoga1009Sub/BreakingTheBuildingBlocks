using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void Launch(Vector3 velocity)
    {
        rb.useGravity = true;

        rb.AddForce(velocity, ForceMode.Impulse);

        // 発射から5秒後に削除
        Destroy(gameObject, lifeTime);
    }
}