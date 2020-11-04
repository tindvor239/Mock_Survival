using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Item/Equipment")]
public abstract class Equipment : Item
{
    #region Member Field
    [SerializeField]
    protected GameObject equipmentModel;
    #endregion
    #region Properties
    public GameObject EquipmentModel { get => equipmentModel; }
    #endregion
}