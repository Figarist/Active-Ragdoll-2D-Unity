using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D hips, leftLeg, rightLeg;
    [SerializeField] private float jumpForce, playerSpeed, positionRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private float breakForce, breakTorque;

    private Animator _animator;
    private HingeJoint2D _leftLegJoint, _rightLegJoint;
    private HealthBar hp;
    private bool isOnGround, endGame;
    private float timeScale, fixedTimeScale;
    private void Start()
    {
        hp = FindObjectOfType<HealthBar>();
        hp.SetMaxHealth(100);
        _animator = GetComponent<Animator>();
        _leftLegJoint = leftLeg.GetComponent<HingeJoint2D>();
        _rightLegJoint = rightLeg.GetComponent<HingeJoint2D>();
        timeScale = Time.timeScale;
        fixedTimeScale = Time.fixedDeltaTime;
        var colliders = transform.GetComponentsInChildren<Collider2D>();
        for (var i = 0; i < colliders.Length; i++)
        for (var j = i + 1; j < colliders.Length; j++)
            Physics2D.IgnoreCollision(colliders[i], colliders[j]);
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
                hips.AddForce(Vector2.right * (playerSpeed * Time.deltaTime));
            }
            else
            {
                _animator.Play("WalkBack");
                hips.AddForce(Vector2.left * (playerSpeed * Time.deltaTime));
            }
        }
        else
        {
            _animator.Play("Idle");
        }

        isOnGround = Physics2D.OverlapCircle(groundPoint.position, positionRadius, ground);
        if (!isOnGround || !Input.GetKeyDown(KeyCode.Space)) return;
        var calculatedJumpForce = Vector2.up * jumpForce;
        hips.AddForce(calculatedJumpForce * 10);
        if (_leftLegJoint != null) leftLeg.AddForce(calculatedJumpForce);
        if (_rightLegJoint != null) rightLeg.AddForce(calculatedJumpForce);
    }

    public void PartFellOf(Collider2D collider2D)
    {
        hp.SetMinusHealth(20);
        if (hp.GetHealth() <= 0 && !endGame)
        {
            endGame = true;
            print("Game Over");
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.fixedDeltaTime / 10f;
        }
        else StartCoroutine(SlowMotion());

        var colliders = transform.GetComponentsInChildren<Collider2D>();
        foreach (var t in colliders) Physics2D.IgnoreCollision(collider2D, t, false);
    }

    private IEnumerator SlowMotion()
    {
        //TODO SLOWMOTION check pause
        if (endGame) yield break;
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime /= 4;
        yield return new WaitForSeconds(0.2f);
        if (endGame) yield break;
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = fixedTimeScale;
    }
}