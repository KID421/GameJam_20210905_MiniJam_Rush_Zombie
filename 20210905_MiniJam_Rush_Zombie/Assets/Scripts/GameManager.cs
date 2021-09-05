using UnityEngine;

/// <summary>
/// 遊戲管理器
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("出現過關的時間")]
    public float timePass = 270;
    [Header("過關區域")]
    public GameObject goPassArea;

    private void Start()
    {
        goPassArea.SetActive(false);
        Invoke("ShowPassArea", timePass);
    }

    /// <summary>
    /// 顯示過關區域
    /// </summary>
    private void ShowPassArea()
    {
        goPassArea.SetActive(true);
    }
}
