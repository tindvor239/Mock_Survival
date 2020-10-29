using UnityEngine;
public abstract class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected Character character;

    protected new Rigidbody rigidbody;
    protected Animator animator;
    protected GameManager gameManager;
    protected virtual void Awake()
    {
        if(GetComponent<Rigidbody>() != null)
            rigidbody = GetComponent<Rigidbody>();
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
    }
    protected virtual void Update()
    {
        float h = Input.GetAxis("Horizontal") * character.MovementSpeed;
        float v = Input.GetAxis("Vertical") * character.MovementSpeed;
        Running(h, v);
        Rotate(h, v);
    }
    protected void Running(float horizontalInput, float verticalInput)
    {
        float thursting = (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)) / character.MovementSpeed;
        animator.SetFloat("moveSpeed", thursting);
    }
    protected void Rotate(float horizontalInput, float verticalInput)
    {
        Vector3 movementVector = new Vector3(horizontalInput, rigidbody.velocity.y, verticalInput);
        if (horizontalInput != 0 || verticalInput != 0)
            transform.forward = movementVector;
    }
}
