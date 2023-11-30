using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [Header("Player Attributes:")]
    [SerializeField] private float baseWalkSpeed = 1.0f;
    [SerializeField] private float crossHairDistance = 1.0f;
    [SerializeField] private float spawnProjectileDistance = 0.6f;
    [SerializeField] private float baseAimSpeedPenalty = 1.0f;
    
    [Header("Player Stats:")] 
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 lastDirection;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool endOfAiming;
    [SerializeField] private bool isAiming;
    [SerializeField] private bool hasFired;

    public Vector2 ShootingDirection { get; private set; }

    [Header("References:")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject projectileSpawn;

    [Header("Prefabs:")] 
    [SerializeField] private GameObject projectilePrefab;
    
    // Animation Parameters
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessInputs();
        Move();
        Animate();
        Aim();
        Shoot();
    }

    void ProcessInputs()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        movementSpeed = Mathf.Clamp(direction.magnitude, 0.0f, 1.0f);
        isWalking = (direction.magnitude > 0);
        isAiming = Input.GetButton("Fire1");
        endOfAiming = Input.GetButtonUp("Fire1");
        
        /*
         * TODO Adjust aiming and shooting to match game
         * If the game depends on movement then aiming should always be on and shooting should be near instant
         * If game is slower and dependent on resource mgmt then require player the aim and fire
         * TODO Add Controller and Mouse and Keyboard Support
         */
        if (isAiming)
        {
            movementSpeed *= baseAimSpeedPenalty;
            hasFired = false;
        }
        
        
    }

    void Move()
    {
        body.velocity = direction * (movementSpeed * baseWalkSpeed);
    }

    void Animate()
    {
        if (isWalking)
        {
            animator.SetFloat(Horizontal, direction.x);
            animator.SetFloat(Vertical, direction.y);
            lastDirection = direction;
        }
        else
        {
            animator.SetFloat(Horizontal, lastDirection.x);
            animator.SetFloat(Vertical, lastDirection.y);
        }
        
        
        animator.SetFloat(Speed, movementSpeed);
        animator.SetBool(IsWalking, isWalking);
    }

    void Aim()
    {
        // TODO Split between mouse or standard aiming
        if (direction != Vector2.zero)
        {
            crosshair.transform.localPosition = direction * crossHairDistance;
            projectileSpawn.transform.localPosition = (direction * spawnProjectileDistance);
        }
    }

    void Shoot()
    {
        // TODO Projectile Recoil
        ShootingDirection = crosshair.transform.localPosition;
        ShootingDirection.Normalize();

        if (endOfAiming && !hasFired)
        {
            Instantiate(projectilePrefab, projectileSpawn.transform.position, Quaternion.identity, transform);
            hasFired = true;
        }
    }

    void Melee()
    {
        /*
         * If you are not shooting
         *      Spawn a ray cast in front of the player and check if enemy is hit.
         *      If so, kill the enemy (in the future subtract hp)
         */
    }
}
