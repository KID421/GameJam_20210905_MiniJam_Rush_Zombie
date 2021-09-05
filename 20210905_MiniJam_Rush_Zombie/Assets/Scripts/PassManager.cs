using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PassManager : MonoBehaviour
{
    [Header("�����e��")]
    public GameObject goFinal;
    public Text textFinalTitle;
    public string stringFinal = "YOU LEAVE THE ZOMBIE TOWN!";
    [Header("���ʳt��"), Range(0, 10)]
    public float speed = 1;
    [Header("�L���ƥ�")]
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
    /// ����
    /// </summary>
    private void Move()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);
    }

    /// <summary>
    /// �L��
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
    /// �����
    /// </summary>
    public void StopMove()
    {
        speed = 0;
    }
}
