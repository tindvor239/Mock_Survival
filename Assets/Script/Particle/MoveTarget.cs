using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    private void Awake()
    {
        
    }
    private void Update()
    {

    }
    public void SelfDestruct()
    {
        DestroyImmediate(gameObject);
    }
}
