using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �a�O�޲z��
/// 1. ����a�O - �L������
/// 2. �ͦ� NPC �ө�
/// </summary>
public class GroundManager : MonoBehaviour
{
    [Header("�a�O")]
    public Transform[] traGrounds;
    [Header("���ʳt��"), Range(0, 100)]
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
    /// ���ʦa�O
    /// </summary>
    private void GroundMove()
    {
        for (int i = 0; i < traGrounds.Length; i++)
        {
            traGrounds[i].Translate(0, 0, -speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// �ˬd�a�O
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
