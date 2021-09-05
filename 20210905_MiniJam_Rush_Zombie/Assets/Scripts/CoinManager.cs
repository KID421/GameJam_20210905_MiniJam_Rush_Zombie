using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����޲z
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class CoinManager : MonoBehaviour
{
    [Header("��������")]
    public string stringCoin = "COIN ";
    public Text textCoin;
    [Header("��������")]
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
    /// ��s����
    /// </summary>
    /// <param name="addCoin">�n�K�[�������ƶq</param>
    /// <param name="soundCoin">True �ϥΪ������� False �ϥ��ʶR����</param>
    public void UpdateCoin(int addCoin, bool useSoundCoin = true)
    {
        coin += addCoin;
        textCoin.text = stringCoin + coin;
        aud.PlayOneShot(useSoundCoin ? soundCoin : soundBuy, Random.Range(0.7f, 1f));
    }
}
