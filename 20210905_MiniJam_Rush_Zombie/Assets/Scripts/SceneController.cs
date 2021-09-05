using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// 載入遊戲場景
    /// </summary>
    public void LoadGame()
    {
        Invoke("DelayLoadGame", 1f);
    }

    /// <summary>
    /// 延遲載入
    /// </summary>
    private void DelayLoadGame()
    {
        SceneManager.LoadScene("遊戲場景");
    }
}
