using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// ��q�޲z��
/// </summary>
public class HealthManager : MonoBehaviour
{
    [Header("��q")]
    public float hp = 100;
    [Header("����")]
    public bool hasUI;
    public string strHp = "HP ";
    public Text? textHp;
    public Image? imgHp;
    [Header("���`�ƥ�")]
    public UnityEvent onDead;
    [Header("���`�ʵe�Ѽ�")]
    public string stringDead = "���`�}��";
    [Header("���˦��`����")]
    public AudioClip soundHurt;
    public AudioClip soundDead;
    [Header("���`��O�_�R��")]
    public bool destroyAfterDead;
    public float delayDestroy = 1.5f;
    [Header("���`��O�_��s����")]
    public bool updateCoin;
    public int coinAdd = 10;
    [Header("���`��O�_�୫�s�C��")]
    public bool canReplay;
    [HideInInspector]
    public float hpMax;
    [Header("�L���ᦺ�`")]
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
    /// ����
    /// </summary>
    /// <param name="damage">�����쪺�ˮ`</param>
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
    /// ���`
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
    /// ���s�C��
    /// </summary>
    private void Replay()
    {
        if (isDead && Input.GetKeyDown(KeyCode.R) && canReplay) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ��s��q
    /// </summary>
    /// <param name="addHp">�n�W�[����</param>
    public void UpdateHp(float addHp)
    {
        hp += addHp;
        Mathf.Clamp(hp, 0, hpMax);
        textHp.text = strHp + hp;
        imgHp.fillAmount = hp / hpMax;
    }
}
