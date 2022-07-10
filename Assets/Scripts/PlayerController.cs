using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce, playerSpeed, positionRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private float breakForce, breakTorque;
    
    private Animator _animator;
    private bool isOnGround;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        var colliders = transform.GetComponentsInChildren<Collider2D>();
        for (var i = 0; i < colliders.Length; i++)
        {
            for (var j = i+1; j < colliders.Length; j++)
            {
                Physics2D.IgnoreCollision(colliders[i],colliders[j]);
            }
        }
        var hingeJoints = transform.GetComponentsInChildren<HingeJoint2D>();
        foreach (var joint in hingeJoints)
        {
            joint.breakForce = breakForce;
            joint.breakTorque = breakTorque;
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                _animator.Play("Walk");
                rb.AddForce(Vector2.right*(playerSpeed * Time.deltaTime));
            }
            else
            {
                _animator.Play("WalkBack");
                rb.AddForce(Vector2.left*(playerSpeed * Time.deltaTime));
            }
        }
        else _animator.Play("Idle");

        isOnGround = Physics2D.OverlapCircle(groundPoint.position, positionRadius, ground);
        if (isOnGround && Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector2.up * (jumpForce * Time.deltaTime));
    }

    public void PartFellOf(Collider2D collider2D)
    {
        var colliders = transform.GetComponentsInChildren<Collider2D>();
        foreach (var t in colliders) { Physics2D.IgnoreCollision(collider2D,t,false); }
    }
}