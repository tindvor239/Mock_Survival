using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    protected float movementSpeed;

    protected new Rigidbody rigidbody;
    protected Animator animator;
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        float h = Input.GetAxis("Horizontal") * movementSpeed;
        float v = Input.GetAxis("Vertical") * movementSpeed;
        Running(h, v);
        Rotate(h, v);
    }
    protected void Running(float horizontalInput, float verticalInput)
    {
        float thursting = (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)) / movementSpeed;
        animator.SetFloat("moveSpeed", thursting);
    }
    protected void Rotate(float horizontalInput, float verticalInput)
    {
        Vector3 movementVector = new Vector3(horizontalInput, rigidbody.velocity.y, verticalInput);
        if (horizontalInput != 0 || verticalInput != 0)
            transform.forward = movementVector;
    }
}
