using UnityEngine;
using UnityEngine.AI;
public class PlayerController : CharacterController
{
    [SerializeField]
    private GameObject target;
    private NavMeshAgent agent;


    #region AI Movement
    private float movementFactor = 0;
    private bool isGetDistance = false;
    private float distance = 1;
    private bool isOnMouseMode = false;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            isOnMouseMode = false;
        GetPositionOnClick(Input.GetMouseButtonDown(0));
        if (isOnMouseMode == false)
            base.Update();
        else
            MoveToPoint();
    }
    private void MoveToPoint()
    {
        if(target != null)
        {
            float currentDistance = Vector3.Magnitude(target.transform.position - transform.position);
            if (Vector.Epsilon(transform.position, target.transform.position) == false)
            {
                if (isGetDistance == false)
                {
                    distance = Vector3.Magnitude(target.transform.position - transform.position);
                    isGetDistance = true;
                }
                movementFactor = Mathf.Lerp(0, 1, movementFactor += Time.deltaTime);
                Vector3 direction = target.transform.position - transform.position;
                if (currentDistance <= distance / 2)
                    target.GetComponent<MoveTarget>().SelfDestruct();
                Rotate(new Vector2(direction.x, direction.z));
                Running(movementFactor, movementFactor);
            }
        }
        else
        {
            movementFactor = Mathf.Lerp(0, 1, movementFactor -= Time.deltaTime);
            isGetDistance = false;
            Running(movementFactor, movementFactor);
            if (movementFactor <= 0)
                isOnMouseMode = false;
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
                isOnMouseMode = true;
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

    private void OnMove()
    {
    }
}
