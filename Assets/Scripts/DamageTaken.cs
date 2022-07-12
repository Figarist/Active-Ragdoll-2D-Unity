using System.Globalization;
using UnityEngine;

public class DamageTaken : MonoBehaviour
{
    private DamageText _damageText;
    private bool _haveJoint = true;
    private HingeJoint2D _joint2D;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _joint2D = GetComponent<HingeJoint2D>();
        var list = FindObjectsOfType<DamageText>();
        foreach (var damageText in list)
            if (damageText.targetName == gameObject.name)
                _damageText = damageText;
    }

    private void Update()
    {
        if (!_haveJoint) return;
        var currentMaxForce = _joint2D.GetReactionForce(Time.fixedDeltaTime).magnitude >
                              _joint2D.GetReactionTorque(Time.fixedDeltaTime)
            ? _joint2D.GetReactionForce(Time.fixedDeltaTime).magnitude
            : _joint2D.GetReactionTorque(Time.fixedDeltaTime);
        currentMaxForce -= 15;
        if (currentMaxForce > 0)
        {
            var currentForce = Mathf.InverseLerp(0, _joint2D.breakForce, currentMaxForce);
            _sprite.color = new Color(1f, 1f - currentForce, 1f - currentForce, 1f);
            _damageText.SetText(currentForce,
                Mathf.Ceil(_joint2D.GetReactionForce(Time.fixedDeltaTime).magnitude)
                    .ToString(CultureInfo.CurrentCulture));
        }
        else
        {
            _damageText.DeleteAllText();
        }
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        JointBreak();
    }

    public void Disconnect()
    {
        JointBreak();
        Destroy(_joint2D);
    }

    private void JointBreak()
    {
        _damageText.SetText(1, _joint2D.breakForce.ToString(CultureInfo.CurrentCulture));
        _sprite.color = new Color(1f, 0f, 0f, 1f);
        _haveJoint = false;
        var t = transform;
        t.parent = null;
        t.position = new Vector3(t.position.x, t.position.y, 0);
        FindObjectOfType<PlayerController>().PartFellOf(GetComponent<Collider2D>());
        Destroy(GetComponent<Balance>());
        Destroy(this);
    }
}