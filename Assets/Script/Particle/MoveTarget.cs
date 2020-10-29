using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    [SerializeField]
    private float selfDestructTime;
    private float currentDestructTime;
    private void Awake()
    {
        currentDestructTime = selfDestructTime;
    }
    private void Update()
    {
        currentDestructTime -= Time.deltaTime;
        if (currentDestructTime <= 0.0f)
            SelfDestruct();
    }
    private void SelfDestruct()
    {
        DestroyImmediate(gameObject);
    }
}
