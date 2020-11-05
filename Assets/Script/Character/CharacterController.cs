using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public abstract class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected Character character;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected Transform target;
    [SerializeField]
    protected GameManager gameManager;
    #region Movement Member
    [SerializeField]
    protected new Rigidbody rigidbody;
    protected float movementMotor;
    protected bool isRolling;
    protected NavMeshAgent agent;
    public float currentDistance { get => GetDistance(); }
    #endregion
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        if (GetComponentInChildren<Character>() != null)
            character = GetComponentInChildren<Character>();
        if(GetComponentInChildren<Animator>() != null)
            animator = GetComponentInChildren<Animator>();
        agent.speed = (character.MovementSpeed - rigidbody.drag) / 2;
        character.onAttacking += OnAttacking;
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
    protected virtual void Roll()
    {
        if(isRolling)
        {
            rigidbody.AddForce(transform.forward * 50, ForceMode.Impulse);
            animator.SetTrigger("rolling");
            isRolling = false;
        }
    }
    protected virtual void Attack()
    {
        //to do: attack animation
    }
    protected void Rotate(Vector2 targetPosition)
    {
        Vector3 movementVector = new Vector3(targetPosition.x, 0, targetPosition.y);
        if (targetPosition.x != 0 || targetPosition.y != 0)
            transform.forward = movementVector;
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
    private float GetDistance()
    {
        if(target != null)
        {
            return Vector3.Magnitude(target.transform.position - transform.position);
        }
        return 0;
    }
    private void OnAttacking()
    {
        if(target.gameObject.tag == "Enemy")
        {
            Character enemy = target.GetComponent<Character>();
            enemy.Stats.HP -= character.Stats.PhysicalDamage.GetValue();
        }
    }
}
