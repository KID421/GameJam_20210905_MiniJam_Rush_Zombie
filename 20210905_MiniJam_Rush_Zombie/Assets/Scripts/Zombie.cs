using UnityEngine;
using System.Collections;

/// <summary>
/// �l�ܥؼСB�����ؼ�
/// ����
/// </summary>
public class Zombie : MonoBehaviour
{
    [Header("�ؼЪ������")]
    public string tagTarget = "Player";
    [Header("���ʳt��"), Range(0, 1000)]
    public float speed = 10;
    [Header("���ؼв��ʶZ���P�����Z��")]
    public float rangeMoveToTarget = 2;
    public float rangeAttack = 2;
    [Header("�����ƭ�")]
    public float attack = 20;
    public float cd = 1.5f;
    [Header("�������a�ϰ�")]
    public Vector3 attackAreaOffset;
    public float attackAreaRadius = 0.5f;
    public float delayAttackDamage;

    private float timer;
    private Rigidbody rig;
    private Animator ani;
    private Transform target;
    private StateZombie state;
    private float speedOriginal;

    /// <summary>
    /// �P�ؼЪ��Z��
    /// </summary>
    public float distanceWithTarget
    {
        get
        {
            return Vector3.Distance(transform.position, target.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeMoveToTarget);

        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeAttack);

        Gizmos.color = new Color(0, 1, 0.3f, 0.3f);
        Gizmos.DrawSphere(
            transform.position +
            transform.right * attackAreaOffset.x +
            transform.up * attackAreaOffset.y +
            transform.forward * attackAreaOffset.z,
            attackAreaRadius);
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();

        target = GameObject.FindWithTag(tagTarget).transform;

        speedOriginal = speed;
    }

    private void Update()
    {
        CheckState();
        CheckAttackRange();
        MoveWhenDeath();
    }

    private void FixedUpdate()
    {
        Track();
    }

    /// <summary>
    /// �ˬd���A
    /// </summary>
    private void CheckState()
    {
        switch (state)
        {
            case StateZombie.track:
                break;
            case StateZombie.attack:
                Attack();
                break;
            case StateZombie.dead:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �l��
    /// </summary>
    private void Track()
    {
        if (state == StateZombie.track || state == StateZombie.attack) rig.velocity = transform.forward * speed;
    }

    /// <summary>
    /// �ˬd�����d��
    /// </summary>
    private void CheckAttackRange()
    {
        if (state == StateZombie.dead) return;

        if (distanceWithTarget <= rangeMoveToTarget) state = StateZombie.attack;
        else state = StateZombie.track;
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Attack()
    {
        transform.LookAt(target);

        if (distanceWithTarget > rangeAttack)
        {
            speed = speedOriginal;
        }
        else
        {
            speed = 0;

            if (timer < cd)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                ani.SetTrigger("����Ĳ�o");
                StopAllCoroutines();
                StartCoroutine(DelayAttackDamage());
            }
        }
    }

    /// <summary>
    /// ����ǰe�����ˮ`
    /// </summary>
    private IEnumerator DelayAttackDamage()
    {
        yield return new WaitForSeconds(delayAttackDamage);

        Collider[] hit = Physics.OverlapSphere(
            transform.position +
            transform.right * attackAreaOffset.x +
            transform.up * attackAreaOffset.y +
            transform.forward * attackAreaOffset.z,
            attackAreaRadius, 1 << 8);

        if (hit.Length > 0) hit[0].GetComponent<HealthManager>().Damage(attack);
    }

    /// <summary>
    /// ���`�Ჾ��
    /// </summary>
    private void MoveWhenDeath()
    {
        if (ani.GetBool("���`�}��"))
        {
            state = StateZombie.dead;
            transform.Translate(Vector3.back * GroundManager.instance.speed * Time.deltaTime / 2, Space.World);
        }
    }
}

/// <summary>
/// �L�ͪ��A
/// </summary>
public enum StateZombie
{
    track, attack, dead
}