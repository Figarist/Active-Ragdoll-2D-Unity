using UnityEngine;

namespace ActiveRagdoll.Ragdoll
{
    public class DamageTaken : MonoBehaviour
    {
        private DamageText _damageText;
        private HingeJoint2D _joint2D;
        private SpriteRenderer _sprite;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _joint2D = GetComponent<HingeJoint2D>();
        }

        private void Start()
        {
            var list = FindObjectsOfType<DamageText>();
            foreach (var damageText in list)
                if (damageText.targetName == gameObject.name)
                    _damageText = damageText;
        }

        private void Update()
        {
            //get max force in frame
            var currentMaxForce = _joint2D.GetReactionForce(Time.fixedDeltaTime).magnitude >
                                  _joint2D.GetReactionTorque(Time.fixedDeltaTime)
                ? _joint2D.GetReactionForce(Time.fixedDeltaTime).magnitude
                : _joint2D.GetReactionTorque(Time.fixedDeltaTime);
            currentMaxForce -= 15;
            
            if (currentMaxForce > 0)
            {
                var normalizeForce = Mathf.InverseLerp(0, _joint2D.breakForce, currentMaxForce);
                _sprite.color = new Color(1f, 1f - normalizeForce, 1f - normalizeForce, 1f);
                _damageText.SetText(normalizeForce, Mathf.Ceil(currentMaxForce).ToString());
            }
            else _damageText.DeleteAllText();
        }

        private void OnJointBreak2D(Joint2D brokenJoint)
        {
            CustomJointBreak();
        }

        public void Disconnect()
        {
            CustomJointBreak();
            Destroy(_joint2D);
        }

        private void CustomJointBreak()
        {
            _damageText.SetText(1, _joint2D.breakForce.ToString());
            _sprite.color = Color.red;
            var t = transform;
            t.parent = null;
            t.position = new Vector3(t.position.x, t.position.y, 0);
            FindObjectOfType<PlayerController>().PartFellOf(GetComponent<Collider2D>());
            Destroy(GetComponent<Balance>());
            Destroy(this);
        }
    }
}