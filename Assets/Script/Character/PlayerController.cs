using UnityEngine;
using UnityEngine.AI;
public class PlayerController : CharacterController
{
    [SerializeField]
    private GameObject target;
    private NavMeshAgent agent;
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
        base.Update();
        GetPositionOnClick(Input.GetMouseButtonDown(0));
        MoveToPoint();
    }
    private void MoveToPoint()
    {
        if(target != null)
        {
            agent.SetDestination(target.transform.position);
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
}
