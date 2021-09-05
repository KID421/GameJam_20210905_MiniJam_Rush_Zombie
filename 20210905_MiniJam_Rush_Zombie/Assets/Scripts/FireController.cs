using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �}�j���
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class FireController : MonoBehaviour
{
    [Header("�l�u")]
    public GameObject goBullet;
    public Transform traPoint;
    [Range(0, 1000)]
    public float speed = 800;
    public float attack = 20;
    [Range(0.01f, 1)]
    public float cd = 0.5f;
    public AudioClip soundFire;
    public Text textBullet;
    public Text textDamage;

    public static FireController instance;

    private int countBullet = 200;
    private AudioSource aud;
    private Animator ani;
    private float timer;
    /// <summary>
    /// ���V�ؼЪ���
    /// </summary>
    private Transform targetLook;

    private void Start()
    {
        instance = this;

        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        targetLook = GameObject.Find("���V�ؼЪ���").transform;
        timer = cd;
    }

    private void Update()
    {
        Fire();
        UpdateTargetLookPosition();
    }

    /// <summary>
    /// �}�j
    /// </summary>
    private void Fire()
    {
        if (Input.GetKey(KeyCode.Mouse0) && countBullet > 0)
        {
            if (timer <= cd)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                aud.PlayOneShot(soundFire, Random.Range(0.7f, 1.1f));
                GameObject tempBullet = Instantiate(goBullet, traPoint.position, Quaternion.identity);
                tempBullet.GetComponent<Rigidbody>().AddForce(traPoint.forward * speed);
                tempBullet.GetComponent<Bullet>().attack = attack;
                countBullet--;
                textBullet.text = countBullet.ToString();
                ani.SetTrigger("�}�jĲ�o");
            }
        }
    }

    /// <summary>
    /// ��s���V�ؼЪ��󪺮y�Ь��ƹ���m
    /// </summary>
    private void UpdateTargetLookPosition()
    {
        Vector3 posMouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(posMouse);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, 1 << 3))
        {
            Vector3 posHit = hit.point;
            posHit.y = 0;
            targetLook.position = posHit;
        }

        transform.LookAt(targetLook);
    }

    /// <summary>
    /// ��s�l�u
    /// </summary>
    /// <param name="add">�n�K�[����</param>
    public void UpdateBullet(int add)
    {
        countBullet += add;
        textBullet.text = countBullet.ToString();
    }

    /// <summary>
    /// ��s�ˮ`
    /// </summary>
    /// <param name="add">�n�K�[����</param>
    public void UpdateDamage(int add)
    {
        attack += add;
        textDamage.text = attack.ToString();
    }
}
