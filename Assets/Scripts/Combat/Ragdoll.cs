using UnityEngine;

namespace Combat
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController characterController;

        private Collider[] _allColliders;
        private Rigidbody[] _allRigidbodies;

        private void Start()
        {
            _allColliders = GetComponentsInChildren<Collider>(true);
            _allRigidbodies = GetComponentsInChildren<Rigidbody>(true);

            ToggleRagdoll(false);
        }

        public void ToggleRagdoll(bool isRagdoll)
        {
            foreach (var collider in _allColliders)
            {
                if (collider.gameObject.CompareTag("Ragdoll"))
                {
                    collider.enabled = isRagdoll;
                }
            }
            
            foreach (var rigidbody in _allRigidbodies)
            {
                if (rigidbody.gameObject.CompareTag("Ragdoll"))
                {
                    rigidbody.isKinematic = !isRagdoll;
                    rigidbody.useGravity = isRagdoll;
                }
            }

            characterController.enabled = !isRagdoll;
            animator.enabled = !isRagdoll;
        }
    }
}