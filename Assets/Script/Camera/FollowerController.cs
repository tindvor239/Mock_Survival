using UnityEngine;

public class FollowerController : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    protected Transform target;
    protected Vector3 offset;
    #region Properties
    public Transform Target { get => target; }
    public Vector3 Offset { get => offset; }
    #endregion
    protected virtual void Start()
    {
        target = GameManager.Instance.Player.transform;
        offset = transform.position - target.position;
    }

    protected virtual void Update()
    {
        Following();
    }
    // Update is called once per frame
    protected void Following()
    {
        transform.position = Vector3.Slerp(transform.position, target.position + offset, moveSpeed);
    }
}
