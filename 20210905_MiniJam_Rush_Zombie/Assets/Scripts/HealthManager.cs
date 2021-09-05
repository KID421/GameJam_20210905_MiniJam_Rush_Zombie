using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 血量管理器
/// </summary>
public class HealthManager : MonoBehaviour
{
    [Header("血量")]
    public float hp = 100;
    [Header("介面")]
    public bool hasUI;
    public string strHp = "HP ";
    public Text? textHp;
    public Image? imgHp;
    [Header("死亡事件")]
    public UnityEvent onDead;
    [Header("死亡動畫參數")]
    public string stringDead = "死亡開關";
    [Header("受傷死亡音效")]
    public AudioClip soundHurt;
    public AudioClip soundDead;
    [Header("死亡後是否刪除")]
    public bool destroyAfterDead;
    public float delayDestroy = 1.5f;
    [Header("死亡後是否更新金幣")]
    public bool updateCoin;
    public int coinAdd = 10;
    [Header("死亡後是否能重新遊戲")]
    public bool canReplay;
    [HideInInspector]
    public float hpMax;
    [Header("過關後死亡")]
    public bool deadWhenPass;

    private Animator ani;
    private AudioSource aud;
    private bool isDead;

    private void Start()
    {
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        hpMax = hp;

        if (deadWhenPass) PassManager.instance.onPass += Damage;
    }

    private void Update()
    {
        Replay();
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收到的傷害</param>
    public void Damage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            hp = 0;
            Dead();
        }
        else aud.PlayOneShot(soundHurt, Random.Range(0.7f, 1.2f));

        if (hasUI)
        {
            textHp.text = strHp + hp;
            imgHp.fillAmount = hp / hpMax;
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        if (isDead) return;

        hp = 0;
        isDead = true;
        ani.SetBool(stringDead, true);
        aud.PlayOneShot(soundDead, Random.Range(0.7f, 1.2f));
        onDead.Invoke();

        if (destroyAfterDead) Destroy(gameObject, delayDestroy);
        if (updateCoin) CoinManager.instance.UpdateCoin(coinAdd);
    }

    /// <summary>
    /// 重新遊戲
    /// </summary>
    private void Replay()
    {
        if (isDead && Input.GetKeyDown(KeyCode.R) && canReplay) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 更新血量
    /// </summary>
    /// <param name="addHp">要增加的值</param>
    public void UpdateHp(float addHp)
    {
        hp += addHp;
        Mathf.Clamp(hp, 0, hpMax);
        textHp.text = strHp + hp;
        imgHp.fillAmount = hp / hpMax;
    }
}
