using UnityEngine;
using System.Collections;

/// <summary>
/// 追蹤目標、攻擊目標
/// 受傷
/// </summary>
public class Zombie : MonoBehaviour
{
    [Header("目標物件標籤")]
    public string tagTarget = "Player";
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10;
    [Header("往目標移動距離與攻擊距離")]
    public float rangeMoveToTarget = 2;
    public float rangeAttack = 2;
    [Header("攻擊數值")]
    public float attack = 20;
    public float cd = 1.5f;
    [Header("攻擊玩家區域")]
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
    /// 與目標的距離
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
    /// 檢查狀態
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
    /// 追蹤
    /// </summary>
    private void Track()
    {
        if (state == StateZombie.track || state == StateZombie.attack) rig.velocity = transform.forward * speed;
    }

    /// <summary>
    /// 檢查攻擊範圍
    /// </summary>
    private void CheckAttackRange()
    {
        if (state == StateZombie.dead) return;

        if (distanceWithTarget <= rangeMoveToTarget) state = StateZombie.attack;
        else state = StateZombie.track;
    }

    /// <summary>
    /// 攻擊
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
                ani.SetTrigger("攻擊觸發");
                StopAllCoroutines();
                StartCoroutine(DelayAttackDamage());
            }
        }
    }

    /// <summary>
    /// 延遲傳送攻擊傷害
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
    /// 死亡後移動
    /// </summary>
    private void MoveWhenDeath()
    {
        if (ani.GetBool("死亡開關"))
        {
            state = StateZombie.dead;
            transform.Translate(Vector3.back * GroundManager.instance.speed * Time.deltaTime / 2, Space.World);
        }
    }
}

/// <summary>
/// 殭屍狀態
/// </summary>
public enum StateZombie
{
    track, attack, dead
}