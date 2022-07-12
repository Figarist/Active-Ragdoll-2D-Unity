using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrasform : MonoBehaviour
{
    public  Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool enableRotation;

    private void Update()
    {
        transform.position = target.position;
        if (enableRotation)
        {
            transform.rotation = target.rotation;
        }
    }
}
