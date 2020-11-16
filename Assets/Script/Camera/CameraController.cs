using UnityEngine;
public class CameraController : FollowerController
{
    [SerializeField]
    private float distance;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        distance = offset.magnitude;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            if (target == null)
                target = GameManager.Instance.Player.transform;
            Orbiting();
            Following();
            transform.LookAt(target);
        }
    }

    private Quaternion TurnVelocity()
    {
        return Quaternion.AngleAxis(Input.GetAxis("Mouse X") * moveSpeed, Vector3.up);
    }

    private void Orbiting()
    {
        if (Input.GetMouseButton(0))
        {
            Quaternion mouseVelocityX;
            mouseVelocityX = TurnVelocity();
            offset = mouseVelocityX * offset;
        }
    }
}
