using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject target;
    [SerializeField] private EnemySpawner spawner;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<TopDownController>().gameObject;
        spawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Ouch");
        if (other.gameObject.CompareTag("Projectile"))
        {
            spawner.DecrementEnemyCount();
            Destroy(other.gameObject);
            Destroy(gameObject);
            
        }
    }
    
}
