using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private InputField nameField;
    [SerializeField]
    private Toggle privateToggle;
    private TestConnect testConnect;

    private void Awake()
    {
        testConnect = TestConnect.Instance;
    }
    public void OnCLick_CreateRoom()
    {
        testConnect.OnClick_CreateRoom(nameField.text, privateToggle.isOn);
    }
}
