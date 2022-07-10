using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    [SerializeField] private KeyCode mouseButton;
    [SerializeField] private int speed = 300;
    
    private Rigidbody2D _rigidbody;
    private Camera _camera;

    private void Start()
    {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        var distance = mousePosition - transform.position;
        var rotation = Mathf.Atan2(distance.x, -distance.y) * Mathf.Rad2Deg;
        if(Input.GetKey(mouseButton)) _rigidbody.MoveRotation(Mathf.LerpAngle(_rigidbody.rotation,rotation,speed * Time.fixedDeltaTime));
        
    }
}
