using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 商店管理
/// 商人出現、商店、購買系統
/// </summary>
public class StoreManager : MonoBehaviour
{
    [Header("購買範圍")]
    public float rangeBuy;
    [Header("商店提示")]
    public GameObject goTip;
    [Header("移動速度"), Range(1, 100)]
    public float speed = 1;
    [Header("商店")]
    public GameObject goStore;
    [Header("隨機回到道路秒數")]
    public float randomBackToRoad = 5;
    /// <summary>
    /// 預設為三分之一
    /// </summary>
    public int randomProbability = 3;
    [Header("商品")]
    public Product[] products;
    [Header("玩家的血量系統")]
    public HealthManager playerHealth;

    /// <summary>
    /// 是否能購買
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
    /// 檢查購買範圍：
    /// 玩家進入後顯示提示並且設定為可以購買
    /// </summary>
    private void CheckRangeBuy()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, rangeBuy, 1 << 8);

        canBuy = hit.Length > 0;
        goTip.SetActive(canBuy);

        if (goStore.activeInHierarchy) goStore.SetActive(canBuy);
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// 打開商店
    /// </summary>
    private void OpenStore()
    {
        if (canBuy && Input.GetKeyDown(KeyCode.E))
        {
            goStore.SetActive(!goStore.activeInHierarchy);
        }
    }

    /// <summary>
    /// 隨機回到道路前方
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
                int random = Random.Range(0, randomProbability);        // 三分之一機率回到道路

                if (random == 0)
                {
                    transform.position = new Vector3(0, 0, 50);
                }
            }
        }
    }

    /// <summary>
    /// 購買
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
                        case "子彈":
                            FireController.instance.UpdateBullet(products[i].addValue);
                            CoinManager.instance.UpdateCoin(-products[i].price, false);
                            break;
                        case "血量":
                            if (playerHealth.hp < playerHealth.hpMax)
                            {
                                playerHealth.UpdateHp(products[i].addValue);
                                CoinManager.instance.UpdateCoin(-products[i].price, false);
                            }
                            break;
                        case "傷害":
                            FireController.instance.UpdateDamage(products[i].addValue);
                            CoinManager.instance.UpdateCoin(-products[i].price, false);
                            break;
                    }
                }
            }
        }
    }
}