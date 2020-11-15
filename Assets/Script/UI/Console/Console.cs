using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    [SerializeField]
    private GameObject consoleText;
    #region Singleton
    private static Console instance;
    private void Awake()
    {
        instance = this;
    }
    public static Console Instance { get => instance; }
    #endregion
    public void Print(string message, Color color)
    {
        GameObject newConsoleText = Instantiate(consoleText, instance.gameObject.transform);
        newConsoleText.GetComponent<Text>().text = message;
        newConsoleText.GetComponent<Text>().color = color;
    }
}
