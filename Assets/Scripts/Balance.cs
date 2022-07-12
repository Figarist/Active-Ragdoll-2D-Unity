using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] private float targetRotation,force;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (targetRotation == 0)
        {
            targetRotation = _rb.rotation;
        }
    }
    
    private void Update()
    {
        _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation,targetRotation,force * Time.fixedDeltaTime));
    }
}
