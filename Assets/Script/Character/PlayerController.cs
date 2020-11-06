using UnityEngine;
public class PlayerController : CharacterController
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Weapon startWeapon;
    [SerializeField]
    private GameObject movePointParticle;
    #region AI Movement
    private bool isUsingButtonMovement = false;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        player = gameObject.GetComponentInChildren<Animator>().gameObject;
        character.Equip(startWeapon);
    }
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        player.transform.localPosition = new Vector3(0, 0, 0);
        isUsingButtonMovement = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        Roll(Input.GetKeyDown(KeyCode.LeftShift));
        agent.ResetPath();
        if(isRolling == false)
        {
            if (isUsingButtonMovement && currentRollDelay <= 0)
            {
                Rotate(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
                Running();
                agent.ResetPath();
                if (target != null)
                {
                    if (target.GetComponent<MoveTarget>() != null)
                        target.GetComponent<MoveTarget>().SelfDestruct();
                    else
                        target = null;
                }
            }
            else
            {
                GetTargetOnClick(Input.GetMouseButtonDown(0));
                MoveToPoint();
            }
            bool canAttack = movementMotor <= 0 && target != null && target.gameObject.tag == "Enemy";
            Debug.Log(canAttack);
            Attack(canAttack);
        }
    }
    protected override void Running()
    {
        Vector3 moveDirection = new Vector3();
        if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            moveDirection.x = Input.GetAxis("Horizontal") * (character.MovementSpeed / 2);
            moveDirection.z = Input.GetAxis("Vertical") * (character.MovementSpeed / 2);
        }
        else
        {
            moveDirection.x = Input.GetAxis("Horizontal") * character.MovementSpeed;
            moveDirection.z = Input.GetAxis("Vertical") * character.MovementSpeed;
        }
        moveDirection.y = rigidbody.velocity.y;
        rigidbody.velocity = moveDirection;
        movementMotor = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));
        base.Running();
    }
    private void MoveToPoint()
    {
        movementMotor = SetMotor(movementMotor);
        if (target != null)
        {
            if (Vector.Epsilon(transform.position, target.transform.position) == false)
            {
                agent.SetDestination(target.transform.position);
                base.Running();
            }
            else
                target.GetComponent<MoveTarget>().SelfDestruct();
        }
        else
        {
            agent.ResetPath();
        }
        if (animator != null)
            animator.SetFloat("moveSpeed", movementMotor);
    }
    private void GetTargetOnClick(bool isInput)
    {
        if(isInput)
        {
            RaycastHit hit;
            Ray ray = gameManager.MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (target != null)
                {
                    if (target.tag != "Enemy" && target.GetComponent<MoveTarget>() != null)
                    {
                        target.GetComponent<MoveTarget>().SelfDestruct();
                    }
                }
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    agent.stoppingDistance = 2;
                    target = hit.transform;
                    target.GetComponent<Character>().TargetParticle.Play();
                }
                else
                {
                    agent.stoppingDistance = 0;
                    if(target != null && target.gameObject.tag == "Enemy")
                        target.GetComponent<Character>().TargetParticle.Stop();
                    target = CreateMovePoint(hit.point, "Point");
                }    
            }
        }
    }
    private Transform CreateMovePoint (Vector3 position, string name)
    {
        GameObject newObject = Instantiate(movePointParticle);
        position = new Vector3(position.x, position.y + 0.2f, position.z);
        newObject.transform.position = position;
        newObject.AddComponent(typeof(MoveTarget));
        if(newObject.GetComponent<ParticleSystem>())
        {
            newObject.GetComponent<ParticleSystem>().Play();
        }
        newObject.name = name;
        return newObject.transform;
    }
}
