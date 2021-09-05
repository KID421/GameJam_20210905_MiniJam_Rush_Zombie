using UnityEngine;

/// <summary>
/// ¸I¼²«á¶Ç»¼¶Ë®`
/// </summary>
public class Bullet : MonoBehaviour
{
    public float attack;
    public LayerMask layer;

    private void Start()
    {
        Physics.IgnoreLayerCollision(7, 7);
        Physics.IgnoreLayerCollision(7, 8);
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (1 << collision.gameObject.layer == layer)
        {
            collision.gameObject.GetComponent<HealthManager>().Damage(attack);
        }

        Destroy(gameObject);
    }
}
