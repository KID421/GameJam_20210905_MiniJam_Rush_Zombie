using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// �ө��޲z
/// �ӤH�X�{�B�ө��B�ʶR�t��
/// </summary>
public class StoreManager : MonoBehaviour
{
    [Header("�ʶR�d��")]
    public float rangeBuy;
    [Header("�ө�����")]
    public GameObject goTip;
    [Header("���ʳt��"), Range(1, 100)]
    public float speed = 1;
    [Header("�ө�")]
    public GameObject goStore;
    [Header("�H���^��D�����")]
    public float randomBackToRoad = 5;
    /// <summary>
    /// �w�]���T�����@
    /// </summary>
    public int randomProbability = 3;
    [Header("�ӫ~")]
    public Product[] products;
    [Header("���a����q�t��")]
    public HealthManager playerHealth;

    /// <summary>
    /// �O�_���ʶR
    /// </summary>
    private bool canBuy;
    private float timer;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeBuy);
    }

    private void Start()
    {
        Physics.IgnoreLayerCollision(6, 9);
        Physics.IgnoreLayerCollision(8, 9);
    }

    private void Update()
    {
        CheckRangeBuy();
        Move();
        OpenStore();
        RandomBackToRoad();
        Buy();
    }

    /// <summary>
    /// �ˬd�ʶR�d��G
    /// ���a�i�J����ܴ��ܨåB�]�w���i�H�ʶR
    /// </summary>
    private void CheckRangeBuy()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, rangeBuy, 1 << 8);

        canBuy = hit.Length > 0;
        goTip.SetActive(canBuy);

        if (goStore.activeInHierarchy) goStore.SetActive(canBuy);
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Move()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// ���}�ө�
    /// </summary>
    private void OpenStore()
    {
        if (canBuy && Input.GetKeyDown(KeyCode.E))
        {
            goStore.SetActive(!goStore.activeInHierarchy);
        }
    }

    /// <summary>
    /// �H���^��D���e��
    /// </summary>
    private void RandomBackToRoad()
    {
        if (transform.position.z < -40)
        {
            if (timer < randomBackToRoad)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                int random = Random.Range(0, randomProbability);        // �T�����@���v�^��D��

                if (random == 0)
                {
                    transform.position = new Vector3(0, 0, 50);
                }
            }
        }
    }

    /// <summary>
    /// �ʶR
    /// </summary>
    private void Buy()
    {
        if (!canBuy) return;
        if (!goStore.activeInHierarchy) return;

        for (int i = 0; i < products.Length; i++)
        {
            if (Input.GetKeyDown(products[i].kcKeyboard))
            {
                if (CoinManager.instance.coin >= products[i].price)
                {
                    

                    switch (products[i].name)
                    {
                        case "�l�u":
                            FireController.instance.UpdateBullet(products[i].addValue);
                            CoinManager.instance.UpdateCoin(-products[i].price, false);
                            break;
                        case "��q":
                            if (playerHealth.hp < playerHealth.hpMax)
                            {
                                playerHealth.UpdateHp(products[i].addValue);
                                CoinManager.instance.UpdateCoin(-products[i].price, false);
                            }
                            break;
                        case "�ˮ`":
                            FireController.instance.UpdateDamage(products[i].addValue);
                            CoinManager.instance.UpdateCoin(-products[i].price, false);
                            break;
                    }
                }
            }
        }
    }
}