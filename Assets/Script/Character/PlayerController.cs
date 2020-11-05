using UnityEngine;
public class PlayerController : CharacterController
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Weapon startWeapon;
    [SerializeField]
    private GameObject particleTarget;
    private float shiftDelayTime = 1.0f;
    private float shiftCurrentDelayTime = 0.0f;
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
        player.transform.localPosition = new Vector3(0, 0, 0);
        isUsingButtonMovement = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        shiftCurrentDelayTime -= Time.deltaTime;
        if (shiftCurrentDelayTime <= 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRolling = true;
            shiftCurrentDelayTime = 1f;
        }
        if(isRolling)
        {
            Roll();
            agent.ResetPath();
            if (target != null)
            {
                target.GetComponent<MoveTarget>().SelfDestruct();
            }
        }
        else
        {
            if (isUsingButtonMovement && shiftCurrentDelayTime <= 0)
            {
                Rotate(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
                Running();
                agent.ResetPath();
                if (target != null)
                {
                    target.GetComponent<MoveTarget>().SelfDestruct();
                }
            }
            else
            {
                GetPositionOnClick(Input.GetMouseButtonDown(0));
                MoveToPoint();
            }
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
    private void GetPositionOnClick(bool isInput)
    {
        if(isInput)
        {
            RaycastHit hit;
            Ray ray = gameManager.MainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if (target != null)
                {
                    if(target.GetComponent<MoveTarget>() != null)
                    {
                        target.GetComponent<MoveTarget>().SelfDestruct();
                    }
                }
                target = CreateMovePoint(hit.point, "Point");
            }
        }
    }
    private Transform CreateMovePoint (Vector3 position, string name)
    {
        GameObject newObject = Instantiate(particleTarget);
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
