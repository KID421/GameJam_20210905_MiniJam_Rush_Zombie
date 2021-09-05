using UnityEngine;

/// <summary>
/// �C���޲z��
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("�X�{�L�����ɶ�")]
    public float timePass = 270;
    [Header("�L���ϰ�")]
    public GameObject goPassArea;

    private void Start()
    {
        goPassArea.SetActive(false);
        Invoke("ShowPassArea", timePass);
    }

    /// <summary>
    /// ��ܹL���ϰ�
    /// </summary>
    private void ShowPassArea()
    {
        goPassArea.SetActive(true);
    }
}
