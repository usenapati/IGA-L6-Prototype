using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float baseProjectileSpeed = 1.0f;
    [SerializeField] private Vector2 shootingDirection;
    // Start is called before the first frame update
    void Start()
    {
        shootingDirection = GetComponentInParent<TopDownController>().ShootingDirection;
        GetComponent<Rigidbody2D>().velocity = shootingDirection * baseProjectileSpeed;
        transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
        Destroy(gameObject,2);
    }

    // Update is called once per frame
}
