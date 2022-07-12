using System;
using UnityEngine;

public class Arms : MonoBehaviour
{
    [SerializeField] private KeyCode mouseButton;
    [SerializeField] private int speed = 300;
    [SerializeField] private bool isGrabArm;
    [SerializeField] private Arms secondArm;
    private bool isNotBreak = true;
    public Grab.Side side;
    
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

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        isNotBreak = false;
        if (isGrabArm)
        {
            if (secondArm.isNotBreak)
            {
                var grabTransform = transform.GetChild(0);
                var localPosition = grabTransform.localPosition;
                grabTransform.parent = secondArm.transform;
                grabTransform.localPosition = localPosition;
                grabTransform.GetComponent<HingeJoint2D>().connectedBody = secondArm.GetComponent<Rigidbody2D>();
            }
            Destroy(this,2f);
        }
        else
        {
            Destroy(secondArm,2f);
            Destroy(this,2f);
        }
        
    }
}
