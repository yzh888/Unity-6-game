using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float turnSpeed = 12f;
    [SerializeField] private LayerMask groundLayer;

    private Transform followTarget;
    private float footOffset;

    public bool IsCollected => followTarget != null;
    public Transform FollowTarget => followTarget;

    private void Awake()
    {
        Collider c = GetComponent<Collider>();
        footOffset = c != null ? transform.position.y - c.bounds.min.y : 0.2f;
    }

    public void Collect(Transform target)
    {
        followTarget = target;
    }

    private void Update()
    {
        if (followTarget != null)
        {
            Vector3 desired = followTarget.position;
            transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);

            Vector3 dir = desired - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.01f)
            {
                Quaternion look = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, look, turnSpeed * Time.deltaTime);
            }
        }

        StickToGround();
    }

    private void StickToGround()
    {
        Vector3 origin = transform.position + Vector3.up * 5f;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 20f, groundLayer))
        {
            Vector3 p = transform.position;
            p.y = hit.point.y + footOffset;
            transform.position = p;
        }
    }
}