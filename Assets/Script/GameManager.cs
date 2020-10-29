using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    private Camera mainCamera;
    private static float mouseSpeed = 20.0f;
    #region Singleton
    private static GameManager instance;
    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
    }
    #endregion
    #region Properties
    public Camera MainCamera { get => mainCamera; }
    public PlayerController Player { get => player; }
    public static GameManager Instance { get => instance;}
    #endregion
    // Update is called once per frame
    private void Update()
    {
        
    }
}
