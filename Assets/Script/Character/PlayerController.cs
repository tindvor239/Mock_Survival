using UnityEngine;
using UnityEngine.AI;
public class PlayerController : CharacterController
{
    [SerializeField]
    private GameObject target;
    private NavMeshAgent agent;


    #region AI Movement
    private float movementFactor = 0;
    private float moveAxis = 0;
    public delegate void OnMoveToPoint();
    public event OnMoveToPoint onMoveToPoint;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        onMoveToPoint += OnMove;
    }
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        GetPositionOnClick(Input.GetMouseButtonDown(0));
        MoveToPoint();
    }
    private void MoveToPoint()
    {
        if(target != null)
        {
            if(onMoveToPoint != null && transform.position != target.transform.position)
            {
                MoveAnimation(moveAxis, movementFactor);
                Vector3 direction = target.transform.position - transform.position;
                Rotate(new Vector2(direction.x, direction.z));
            }
        }
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
    private GameObject CreateMovePoint (Vector3 position, string name)
    {
        GameObject newObject = new GameObject();
        newObject.transform.position = position;
        newObject.AddComponent(typeof(MoveTarget));
        newObject.name = name;
        return newObject;
    }
    private void MoveAnimation( float moveAxis, float moveFactor)
    {
        moveAxis += 100f;
        moveFactor += moveAxis * Time.deltaTime * character.MovementSpeed;
        Debug.Log(moveFactor);
        animator.SetFloat("moveSpeed", moveFactor);

    }

    private void OnMove()
    {
    }
}
