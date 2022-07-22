using UnityEngine;

namespace ActiveRagdoll.Core
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField] public  Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private bool enableRotation;

        private void Update()
        {
            transform.position = target.position + offset;
            if (enableRotation)
                transform.rotation = target.rotation;
        }
    }
}
