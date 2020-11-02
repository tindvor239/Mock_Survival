using UnityEngine;
public abstract class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected Character character;
    [SerializeField]
    protected new Rigidbody rigidbody;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected Transform target;
    protected GameManager gameManager;
    private float movementMotor;
    protected float MovementMotor
    {
        get => movementMotor;
        set => movementMotor = SetMotor(value);
    }
    public float currentDistance { get => GetDistance(); }
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
        Debug.Log(movementMotor);
    }
    protected void Running(float horizontalInput, float verticalInput)
    {
        float thursting = (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)) / character.MovementSpeed;
        rigidbody.velocity = new Vector3(horizontalInput, rigidbody.velocity.y, verticalInput);
        if(animator != null)
        {
            animator.SetFloat("moveSpeed", thursting);
            Debug.Log(thursting);
        }
    }
    protected void Rotate(Vector2 targetPosition)
    {
        Vector3 movementVector = new Vector3(targetPosition.x, 0, targetPosition.y);
        if (targetPosition.x != 0 || targetPosition.y != 0)
            transform.forward = movementVector;
    }
    private float GetDistance()
    {
        if(target != null)
        {
            return Vector3.Magnitude(target.transform.position - transform.position);
        }
        return 0;
    }
    private float SetMotor(float value)
    {
        if (currentDistance <= 2.0f)
        {
            return Mathf.Lerp(0, 1, value -= Time.deltaTime);
        }
        else
        {
            return Mathf.Lerp(0, 1, value += Time.deltaTime);
        }
    }
}
