using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 金幣管理
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class CoinManager : MonoBehaviour
{
    [Header("金幣介面")]
    public string stringCoin = "COIN ";
    public Text textCoin;
    [Header("金幣音效")]
    public AudioClip soundCoin;
    public AudioClip soundBuy;
    [HideInInspector]
    public int coin = 100;

    private AudioSource aud;

    public static CoinManager instance;

    private void Start()
    {
        instance = this;

        aud = GetComponent<AudioSource>();

        textCoin.text = stringCoin + coin;
    }

    /// <summary>
    /// 更新金幣
    /// </summary>
    /// <param name="addCoin">要添加的金幣數量</param>
    /// <param name="soundCoin">True 使用金幣音效 False 使用購買音效</param>
    public void UpdateCoin(int addCoin, bool useSoundCoin = true)
    {
        coin += addCoin;
        textCoin.text = stringCoin + coin;
        aud.PlayOneShot(useSoundCoin ? soundCoin : soundBuy, Random.Range(0.7f, 1f));
    }
}
