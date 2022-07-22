using System;
using UnityEngine;

namespace ActiveRagdoll.Ragdoll
{
    public class Balance : MonoBehaviour
    {
        [SerializeField] private float targetRotation, force;
        private Rigidbody2D _rb;

        private void Awake() { _rb = GetComponent<Rigidbody2D>(); }

        private void Start()
        {
            if (targetRotation == 0) targetRotation = _rb.rotation;
        }

        private void FixedUpdate()
        {
            _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, targetRotation, force * Time.fixedDeltaTime));
        }
    }
}