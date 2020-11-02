using UnityEngine;
public class PlayerController : CharacterController
{
    [SerializeField]
    private GameObject player;
    #region AI Movement
    private bool isUsingButtonMovement = false;
    #endregion
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        player.transform.localPosition = new Vector3(0, 0, 0);
        base.Update();
        isUsingButtonMovement = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        if (isUsingButtonMovement)
        {
            agent.ResetPath();
            Running();
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
    protected override void Running()
    {
        rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * character.MovementSpeed, rigidbody.velocity.y, Input.GetAxis("Vertical") * character.MovementSpeed);
        movementMotor = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));
        base.Running();
    }
    private void MoveToPoint()
    {
        if (target != null)
        {
            if (Vector.Epsilon(transform.position, target.transform.position) == false)
            {
                agent.SetDestination(target.transform.position);
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
        GameObject newObject = new GameObject();
        newObject.transform.position = position;
        newObject.AddComponent(typeof(MoveTarget));
        newObject.name = name;
        return newObject.transform;
    }

    private void OnMove()
    {
    }
}
