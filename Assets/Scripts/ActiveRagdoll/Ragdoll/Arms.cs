using System;
using UnityEngine;

namespace ActiveRagdoll.Ragdoll
{
    public class Arms : MonoBehaviour
    {
        [SerializeField] private KeyCode mouseButton;
        [SerializeField] private int speed = 300;
        [SerializeField] private bool isGrabArm;
        [SerializeField] private Arms secondArm;
        public Side side;
        private Camera _camera;

        private Rigidbody2D _rigidbody;
        private bool _isNotBreak = true;

        private void Awake()
        {
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!Input.GetKey(mouseButton)) return;
            var distanceToMousePoint = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var rotationAngle = Mathf.Atan2(distanceToMousePoint.x, -distanceToMousePoint.y) * Mathf.Rad2Deg;
            _rigidbody.MoveRotation(Mathf.LerpAngle(_rigidbody.rotation, rotationAngle, speed * Time.fixedDeltaTime));
        }

        private void OnJointBreak2D(Joint2D brokenJoint)
        {
            _isNotBreak = false;
            if (isGrabArm)
            {
                if (secondArm._isNotBreak)
                {
                    var grabTransform = transform.GetChild(0);
                    var localPosition = grabTransform.localPosition;
                    grabTransform.parent = secondArm.transform;
                    grabTransform.localPosition = localPosition;
                    grabTransform.GetComponent<HingeJoint2D>().connectedBody = secondArm.GetComponent<Rigidbody2D>();
                }
                Destroy(this, 2f);
            }
            else
            {
                Destroy(secondArm, 2f);
                Destroy(this, 2f);
            }
        }
    }
}