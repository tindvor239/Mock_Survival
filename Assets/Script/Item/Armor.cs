using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Equipment/Armor")]
public class Armor : Equipment
{
    #region Member Field
    [SerializeField]
    private uint armor;
    [SerializeField]
    private ArmorEquipType armorEquipType;
    #endregion
    #region Properties
    public ArmorEquipType ArmorEquipType { get => armorEquipType; }
    #endregion
}
public enum ArmorEquipType { helmet, torso, gauntlets, boots }
