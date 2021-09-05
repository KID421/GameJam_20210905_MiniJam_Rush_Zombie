using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PassManager : MonoBehaviour
{
    [Header("結束畫面")]
    public GameObject goFinal;
    public Text textFinalTitle;
    public string stringFinal = "YOU LEAVE THE ZOMBIE TOWN!";
    [Header("移動速度"), Range(0, 10)]
    public float speed = 1;
    [Header("過關事件")]
    public UnityEvent onPassEvent;
    [HideInInspector]
    public  bool isPass;

    public delegate void DelegatePass(float value);
    public event DelegatePass onPass;

    public static PassManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Move();

        if (isPass && Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") Pass();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);
    }

    /// <summary>
    /// 過關
    /// </summary>
    private void Pass()
    {
        isPass = true;
        onPass(100);
        onPassEvent.Invoke();
        goFinal.SetActive(true);
        textFinalTitle.text = stringFinal;
    }

    /// <summary>
    /// 停止移動
    /// </summary>
    public void StopMove()
    {
        speed = 0;
    }
}
