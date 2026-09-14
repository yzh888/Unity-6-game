using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float groundOffset = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    public float MoveSpeed => moveSpeed;

    public void SetSpeed(float value)
    {
        moveSpeed = value;
    }

    private void Update()
    {
        Vector2 input = ReadInput();
        Vector3 dir = new Vector3(input.x, 0f, input.y).normalized;

        if (dir.sqrMagnitude > 0.01f)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;

            Quaternion target = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, turnSpeed * Time.deltaTime);
        }
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

        StickToGround();
    }

    private void StickToGround()
    {
        Vector3 origin = transform.position + Vector3.up * 5f;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 20f, groundLayer))
        {
            Vector3 p = transform.position;
            p.y = hit.point.y + groundOffset;
            transform.position = p;
        }
    }

    private Vector2 ReadInput()
    {
        Keyboard k = Keyboard.current;
        if (k == null) return Vector2.zero;

        Vector2 v = Vector2.zero;
        if (k.wKey.isPressed || k.upArrowKey.isPressed)    v.y += 1f;
        if (k.sKey.isPressed || k.downArrowKey.isPressed)  v.y -= 1f;
        if (k.aKey.isPressed || k.leftArrowKey.isPressed)  v.x -= 1f;
        if (k.dKey.isPressed || k.rightArrowKey.isPressed) v.x += 1f;
        return v;
    }
}