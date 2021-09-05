using UnityEngine;

[CreateAssetMenu(menuName = "KID/商品資訊", fileName = "商品資訊")]
public class Product : ScriptableObject
{
    [Header("商品按鍵")]
    public KeyCode kcKeyboard;
    [Header("商品價格")]
    public int price;
    [Header("商品增加的值")]
    public int addValue;
}
