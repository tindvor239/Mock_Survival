using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
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
    protected NavMeshAgent agent;
    protected GameManager gameManager;
    protected float movementMotor;
    public float currentDistance { get => GetDistance(); }
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = character.MovementSpeed;
    }
    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
    }
    protected virtual void Update()
    {

    }
    protected virtual void Running()
    {
        if(animator != null)
        {
            animator.SetFloat("moveSpeed", movementMotor);
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
    protected float SetMotor(float value)
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
