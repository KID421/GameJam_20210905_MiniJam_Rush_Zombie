using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 開槍控制器
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class FireController : MonoBehaviour
{
    [Header("子彈")]
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
    /// 面向目標物件
    /// </summary>
    private Transform targetLook;

    private void Start()
    {
        instance = this;

        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        targetLook = GameObject.Find("面向目標物件").transform;
        timer = cd;
    }

    private void Update()
    {
        Fire();
        UpdateTargetLookPosition();
    }

    /// <summary>
    /// 開槍
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
                ani.SetTrigger("開槍觸發");
            }
        }
    }

    /// <summary>
    /// 更新面向目標物件的座標為滑鼠位置
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
    /// 更新子彈
    /// </summary>
    /// <param name="add">要添加的值</param>
    public void UpdateBullet(int add)
    {
        countBullet += add;
        textBullet.text = countBullet.ToString();
    }

    /// <summary>
    /// 更新傷害
    /// </summary>
    /// <param name="add">要添加的值</param>
    public void UpdateDamage(int add)
    {
        attack += add;
        textDamage.text = attack.ToString();
    }
}
