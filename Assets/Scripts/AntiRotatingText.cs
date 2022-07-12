using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRotatingText : MonoBehaviour
{
    private Vector3 _startScale;
    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startScale = _rectTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        var parent = transform.parent;
        Vector3 scaleTmp = transform.localScale;
        scaleTmp.x /= parent.localScale.x;
        scaleTmp.y /= parent.localScale.y;
        scaleTmp.z /= parent.localScale.z;
        transform.parent = parent;
        transform.localScale = scaleTmp;
        
        var angle = parent.rotation.z;
        transform.rotation = new Quaternion(0, 0, angle, 0);
        transform.Rotate(Vector3.forward,180);
    }
}
