using UnityEngine;

namespace ActiveRagdoll.Ragdoll
{
    public enum Side
    {
        Left,
        Right
    }

    public class Grab : MonoBehaviour
    {
        [SerializeField] private Side side;
        [SerializeField] private KeyCode mouseButton;
        private bool _isHold;

        private void Update()
        {
            if (Input.GetKey(mouseButton))
            {
                _isHold = true;
            }
            else
            {
                _isHold = false;
                if (TryGetComponent(out FixedJoint2D fixedJoint2D))
                    Destroy(fixedJoint2D);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!_isHold) return;
            if (col.transform.TryGetComponent(out Rigidbody2D rigidbody))
            {
                var joint = transform.gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = rigidbody;
            }
            else
            {
                transform.gameObject.AddComponent<FixedJoint2D>();
            }
        }
    }
}