using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private InputField nameField;
    [SerializeField]
    private Toggle privateToggle;
    private TestConnect testConnect;

    private void Start()
    {
        testConnect = TestConnect.Instance;
    }
    public void OnClick_CreateRoom()
    {
        if(nameField.text != "")
            testConnect.OnClick_CreateRoom(nameField.text, privateToggle.isOn);
    }

    public void OnClick_Close()
    {
        gameObject.SetActive(false);
    }
}
