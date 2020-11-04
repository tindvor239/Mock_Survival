using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Equipment/Weapon")]
public class Weapon : Equipment
{
    #region Member Field
    [SerializeField]
    private uint damage;
    [SerializeField]
    private WeaponEquipType weaponEquipType;
    #endregion
    #region Properties
    public uint Damage { get => damage; }
    public WeaponEquipType WeaponEquipType { get => weaponEquipType; }
    #endregion
}
public enum WeaponEquipType {oneHanded, twoHanded}
