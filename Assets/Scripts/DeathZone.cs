using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private TypeDamage _typeDamage;

    private void OnCollisionStay2D(Collision2D col)
    {
        var gm = col.gameObject;
        if (gm.TryGetComponent(out Grab grab)) return;
        if (_typeDamage == TypeDamage.Fire)
        {
            if (gm.TryGetComponent(out AlreadyBurn burn))
            {
                //TODO fire system;
            }
            else
            {
                gm.AddComponent<AlreadyBurn>();
                var fire = Instantiate(FindObjectOfType<GameManager>().fire, col.contacts[0].point,
                    Quaternion.identity);
                fire.AddComponent<FollowTrasform>().target = gm.transform;
                StartCoroutine(DeleteFireAfterAmountOfTime(fire));
                if (gm.TryGetComponent(out DamageTaken dmg)) dmg.Disconnect();
            }
        }
    }

    private static IEnumerator DeleteFireAfterAmountOfTime(GameObject fire)
    {
        yield return new WaitForSeconds(5f);
        var bodyPart = fire.GetComponent<FollowTrasform>().target;
        if (bodyPart.TryGetComponent(out HingeJoint2D joint) ||
            bodyPart.TryGetComponent(out FixedJoint2D fixedJoint))
        {
            Destroy(bodyPart.GetComponent<AlreadyBurn>());
            Destroy(fire);
        }
    }

    private enum TypeDamage
    {
        Fire,
        Explosion
    }
}