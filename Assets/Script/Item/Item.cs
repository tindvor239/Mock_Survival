using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    #region Member Field
    [SerializeField]
    protected new string name;
    [SerializeField]
    protected Sprite icon;
    #endregion
    #region Properties
    public string Name { get => name; }
    public Sprite Icon { get => icon; }
    #endregion
}
