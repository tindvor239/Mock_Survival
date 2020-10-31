using UnityEngine;
public abstract class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected Character character;

    protected Rigidbody rigidbody;
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
        Rotate(new Vector2(h, v));
    }
    protected void Running(float horizontalInput, float verticalInput)
    {
        float thursting = (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)) / character.MovementSpeed;
        animator.SetFloat("moveSpeed", thursting);
    }
    protected void Rotate(Vector2 targetPosition)
    {
        Vector3 movementVector = new Vector3(targetPosition.x, 0, targetPosition.y);
        if (targetPosition.x != 0 || targetPosition.y != 0)
            transform.forward = movementVector;
    }
}
