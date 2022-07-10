using UnityEngine;

public class DamageTaken : MonoBehaviour
{
    private HingeJoint2D _joint2D;
    private SpriteRenderer _sprite;
    private bool _haveJoint = true;
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _joint2D = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        if (!_haveJoint) return;
        var currentForce = Mathf.InverseLerp(0, _joint2D.breakForce,
            _joint2D.GetReactionForce(Time.fixedDeltaTime).magnitude);
        _sprite.color = new Color(1f, 1f - currentForce, 1f - currentForce, 1f);
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        print($"Part fell of - {gameObject.name}");
        _haveJoint = false;
        var t = transform;
        t.parent = null;
        t.position = new Vector3(t.position.x,t.position.y,0);
        FindObjectOfType<PlayerController>().PartFellOf(GetComponent<Collider2D>());
    }
} 