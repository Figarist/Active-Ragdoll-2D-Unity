using System.Collections;
using ActiveRagdoll.Core;
using ActiveRagdoll.UI;
using UnityEngine;

namespace ActiveRagdoll.Ragdoll
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Rigidbodies")] [SerializeField]
        private Rigidbody2D hips;

        [SerializeField] private Rigidbody2D leftLeg;
        [SerializeField] private Rigidbody2D rightLeg;

        [Header("Stats")] [SerializeField] private float jumpForce;
        [SerializeField] private float playerSpeed;
        [SerializeField] private float breakForce;
        [SerializeField] private float breakTorque;

        [Header("Setup properties")] [SerializeField]
        private LayerMask ground;

        [SerializeField] private Transform groundPoint;
        [SerializeField] [Range(.1f, 1)] private float checkGroundRadius;

        [Header("Slow motion")] [SerializeField] [Range(1, 20)]
        private float dividingScale;

        [SerializeField] private float slowMoTime;

        private Animator _animator;
        private HealthBar _hp;
        private bool _isOnGround;
        private HingeJoint2D _leftLegJoint, _rightLegJoint;

        private void Awake()
        {
            _hp = FindObjectOfType<HealthBar>();
            _animator = GetComponent<Animator>();
            _leftLegJoint = leftLeg.GetComponent<HingeJoint2D>();
            _rightLegJoint = rightLeg.GetComponent<HingeJoint2D>();
        }

        private void Start()
        {
            _hp.SetMaxHealth(100);
            IgnoreCollision();
            var hingeJoints = transform.GetComponentsInChildren<HingeJoint2D>();
            foreach (var joint in hingeJoints)
            {
                joint.breakForce = breakForce;
                joint.breakTorque = breakTorque;
            }
        }

        private void Update()
        {
            Walking();
            Jumping();
        }

        private void Walking()
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
        }

        private void Jumping()
        {
            _isOnGround = Physics2D.OverlapCircle(groundPoint.position, checkGroundRadius, ground);
            if (_isOnGround && Input.GetKeyDown(KeyCode.Space))
            {
                var calculatedJumpForce = Vector2.up * jumpForce;
                hips.AddForce(calculatedJumpForce * 10);
                if (_leftLegJoint != null) leftLeg.AddForce(calculatedJumpForce);
                if (_rightLegJoint != null) rightLeg.AddForce(calculatedJumpForce);
            }
        }

        public void PartFellOf(Collider2D collider2D)
        {
            _hp.SetMinusHealth(25);
            if (_hp.GetHealth() <= 0)
            {
                EventManager.Broadcast(new GameOverEvent {TypeOfGameOver = TypeOfGameOver.Damage});
                StartCoroutine(SlowMotion(true));
            }
            else
            {
                StartCoroutine(SlowMotion(false));
            }

            SetCollision(collider2D);
        }

        public void IgnoreCollision()
        {
            var colliders = transform.GetComponentsInChildren<Collider2D>();
            for (var i = 0; i < colliders.Length; i++)
            for (var j = i + 1; j < colliders.Length; j++)
                Physics2D.IgnoreCollision(colliders[i], colliders[j]);
        }

        public void SetCollision(Collider2D collider1)
        {
            foreach (var collider2 in transform.GetComponentsInChildren<Collider2D>())
                Physics2D.IgnoreCollision(collider1, collider2, false);
        }

        private IEnumerator SlowMotion(bool dead)
        {
            Time.timeScale = dead
                ? GameManager.DefaultTimeScale / (dividingScale * 2)
                : GameManager.DefaultTimeScale / dividingScale;
            Time.fixedDeltaTime = GameManager.DefaultFixedScale / 2;

            yield return new WaitForSeconds(slowMoTime * Time.timeScale);

            Time.timeScale = GameManager.DefaultTimeScale;
            Time.fixedDeltaTime = GameManager.DefaultFixedScale;
        }
    }
}