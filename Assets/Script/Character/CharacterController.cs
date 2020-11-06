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
    #region Combat Member
    [SerializeField]
    protected float attackDelayTime = 1.0f;
    protected float currentAttackDelay;
    protected bool isAttacking = false;
    #endregion
    #region Movement Member
    [SerializeField]
    protected Rigidbody rigidbody;
    protected float movementMotor;
    protected bool isRolling = false;
    protected NavMeshAgent agent;
    [SerializeField]
    protected float rollDelayTime = 1.0f;
    protected float currentRollDelay;
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
        agent.speed = character.MovementSpeed - (rigidbody.drag / 2);
        currentRollDelay = rollDelayTime;
        character.onAttacking += OnAttacking;
        currentAttackDelay = attackDelayTime;
    }
    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
    }
    protected virtual void Update()
    {
        currentRollDelay -= Time.deltaTime;
        currentAttackDelay -= Time.deltaTime;
    }
    protected virtual void Running()
    {
        if(animator != null)
        {
            animator.SetFloat("moveSpeed", movementMotor);
        }
    }
    protected virtual void Roll(in bool input)
    {
        if (currentRollDelay <= 0 && input)
        {
            isRolling = true;
            Debug.Log(isRolling);
            currentRollDelay = rollDelayTime;
        }
        if(isRolling)
        {
            rigidbody.AddForce(transform.forward * (character.MovementSpeed * 3), ForceMode.Impulse);
            animator.SetTrigger("rolling");
            if(target != null)
            {
                Destroy(target.gameObject);
            }
            isRolling = false;
        }
    }
    protected virtual void Attack(in bool input)
    {
        if(currentAttackDelay <= 0f && input)
        {
            if (target != null && target.gameObject.tag == "Enemy")
            {
                Vector3 direction = target.position - transform.position;
                Rotate(new Vector2(direction.x, direction.z));
            }
            isAttacking = true;
            currentAttackDelay = attackDelayTime;
        }
        else
        {
            animator.ResetTrigger("attacking");
        }
        if (isAttacking)
        {
            animator.SetTrigger("attacking");
            isAttacking = false;
        }
    }
    protected void Rotate(Vector2 targetPosition)
    {
        Vector3 movementVector = new Vector3(targetPosition.x, 0, targetPosition.y);
        if (targetPosition.x != 0 || targetPosition.y != 0)
            transform.forward = movementVector;
    }
    protected float SetMotor(float value)
    {
        if (currentDistance <= agent.stoppingDistance + 2.0f)
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
            enemy.Stats.HP -= (int)character.Stats.PhysicalDamage.GetValue();
        }
    }
}
