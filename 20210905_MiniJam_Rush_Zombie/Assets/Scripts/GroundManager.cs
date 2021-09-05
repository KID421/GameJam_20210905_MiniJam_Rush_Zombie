using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 地板管理器
/// 1. 控制地板 - 無限延伸
/// 2. 生成 NPC 商店
/// </summary>
public class GroundManager : MonoBehaviour
{
    [Header("地板")]
    public Transform[] traGrounds;
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1;

    public static GroundManager instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        GroundMove();
        CheckGround();
    }

    /// <summary>
    /// 移動地板
    /// </summary>
    private void GroundMove()
    {
        for (int i = 0; i < traGrounds.Length; i++)
        {
            traGrounds[i].Translate(0, 0, -speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 檢查地板
    /// </summary>
    private void CheckGround()
    {
        for (int i = 0; i < traGrounds.Length; i++)
        {
            if (traGrounds[i].position.z <= -50)
            {
                int last = i + 2;
                last = last >= traGrounds.Length ? last - traGrounds.Length : last;
                Vector3 pos = new Vector3(0, 0, traGrounds[last].position.z + 50);
                traGrounds[i].position = pos;
            }
        }
    }
}
