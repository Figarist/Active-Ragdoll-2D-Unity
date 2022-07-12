using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public enum Side
    {
        Left,
        Right
    }

    [SerializeField] private Side side;
    [SerializeField] private KeyCode mouseButton;
    private bool isHold;

    private void Update()
    {
        if(Input.GetKey(mouseButton))isHold = true;
        else
        {
            isHold = false;
            Destroy(GetComponent<FixedJoint2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isHold)
        {
            Rigidbody2D rigidbody = col.transform.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                var joint = transform.gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = rigidbody;
            }
            else
            {
                var joint = transform.gameObject.AddComponent<FixedJoint2D>();
            }
        }
    }

}
