using UnityEngine;

public class DamageTaken : MonoBehaviour
{
    private HingeJoint2D _joint2D;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _joint2D = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        var currentForce = Mathf.InverseLerp(0, _joint2D.breakForce, _joint2D.GetReactionForce(Time.fixedDeltaTime).magnitude);
        _sprite.color = new Color(1f, 1f-currentForce, 1f-currentForce,1f);;
    }
}