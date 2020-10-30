using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    private static float selfDestructTime = 2.0f;
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
    public void SelfDestruct()
    {
        DestroyImmediate(gameObject);
    }
}
