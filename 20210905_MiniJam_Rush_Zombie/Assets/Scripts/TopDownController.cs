using UnityEngine;

/// <summary>
/// 上往下視野控制器
/// </summary>
public class TopDownController : MonoBehaviour
{
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1;

    private Rigidbody rig;
    private Animator ani;
    private float horizontal;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        if (!PassManager.instance.isPass) rig.velocity = -Vector3.right * horizontal * speed;
        else transform.Translate(0, 0, speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// 移動輸入
    /// </summary>
    private void MoveInput()
    {
        horizontal = Input.GetAxis("Vertical");
    }
}
