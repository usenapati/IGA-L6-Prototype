using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private float walkSpeed;

    [SerializeField] private Vector2 direction;
    [SerializeField] private bool isWalking;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        isWalking = (direction.magnitude > 0);
        body.velocity = direction * walkSpeed;
        
        animator.SetFloat(Horizontal, direction.x);
        animator.SetFloat(Vertical, direction.y);
        animator.SetBool(IsWalking, isWalking);
    }
}
