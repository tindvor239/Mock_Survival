using UnityEngine;

public class FollowerController : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    protected Transform target;
    [SerializeField]
    protected Vector3 offset;
    #region Properties
    public Transform Target { get => target; }
    public Vector3 Offset { get => offset; }
    #endregion
    protected virtual void Start()
    {
        target = GameManager.Instance.Player.transform;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            if (target == null)
            {
                target = GameManager.Instance.Player.transform;
            }
            Following();
        }
    }
    // Update is called once per frame
    protected void Following()
    {
        transform.position = Vector3.Slerp(transform.position, target.position + offset, moveSpeed);
    }
}
