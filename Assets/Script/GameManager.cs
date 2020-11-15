using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    private static Camera mainCamera;
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
    public void LoadScene(byte index)
    {
        SceneManager.LoadScene(index);
    }
    public void Quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    private void Update()
    {
        
    }
}
