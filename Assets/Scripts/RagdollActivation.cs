using UnityEngine;

public class RagdollActivation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody[] rigidbodies;
    private Vector3[] positions;
    private Quaternion[] rotations;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        positions = new Vector3[rigidbodies.Length];
        rotations = new Quaternion[rigidbodies.Length];

        for(int i = 0; i < rigidbodies.Length; i++)
        {
            positions[i] = rigidbodies[i].gameObject.transform.position;
            rotations[i] = rigidbodies[i].gameObject.transform.rotation;
        }

            SetRagdollActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            ActivateRagdoll();
        }
    }

    void ActivateRagdoll()
    {
        SetRagdollActive(true);

        animator.enabled = false;
    }

    void SetRagdollActive(bool isActive)
    {
        foreach (var rb in rigidbodies)
        {
            switch (rb.name)
            {
                case "Stickman": rb.gameObject.GetComponent<BoxCollider>().enabled = !isActive; break;
                case "pelvis":   
                case "spine_01": rb.gameObject.GetComponent<BoxCollider>().enabled = isActive; rb.isKinematic = !isActive; break;
                case "upperarm_l": 
                case "lowerarm_l": 
                case "upperarm_r": 
                case "lowerarm_r":
                case "thigh_l": 
                case "calf_l": 
                case "thigh_r": 
                case "calf_r": rb.gameObject.GetComponent<CapsuleCollider>().enabled = isActive; rb.isKinematic = !isActive; break;
                case "neck_01": rb.gameObject.GetComponent<SphereCollider>().enabled = isActive; rb.isKinematic = !isActive; break;
                default: Debug.Log("defaultSetRagdollActive"); break;
            }
        }
    }

    public void Reset()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].gameObject.transform.position = positions[i];
            rigidbodies[i].gameObject.transform.rotation = rotations[i];
            
            SetRagdollActive(false);
            animator.enabled = true;
        }
    }
}
