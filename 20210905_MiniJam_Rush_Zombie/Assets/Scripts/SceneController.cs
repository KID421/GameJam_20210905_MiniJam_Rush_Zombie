using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// ���J�C������
    /// </summary>
    public void LoadGame()
    {
        Invoke("DelayLoadGame", 1f);
    }

    /// <summary>
    /// ������J
    /// </summary>
    private void DelayLoadGame()
    {
        SceneManager.LoadScene("�C������");
    }
}
