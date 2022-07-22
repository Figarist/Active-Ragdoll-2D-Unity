using UnityEngine;

namespace ActiveRagdoll.Core
{
    public class AntiRotatingText : MonoBehaviour
    {
        private void Update()
        {
            var tr = transform;
            var parent = tr.parent;
            var scaleTmp = tr.localScale;
            var localScale = parent.localScale;
            scaleTmp.x /= localScale.x;
            scaleTmp.y /= localScale.y;
            scaleTmp.z /= localScale.z;
            
            tr.parent = parent;
            tr.localScale = scaleTmp;
            tr.rotation = new Quaternion(0, 0, parent.rotation.z, 0);
            transform.Rotate(Vector3.forward,180);
        }
    }
}
