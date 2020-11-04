using UnityEngine;

public class Character : MonoBehaviour
{
    #region Member Field
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Stats stats;
    [SerializeField]
    private CharacterEquipment equipments;
    #endregion
    #region Properties
    public float MovementSpeed { get => movementSpeed; }
    public float RotationSpeed { get => rotationSpeed; }
    public Stats Stats { get => stats; }
    #endregion
    #region Method
    public void Equip(Equipment newEquipment)
    {
        if(newEquipment is Weapon)
        {
            Weapon newWeapon = (Weapon)newEquipment;
            if(newWeapon.WeaponEquipType == WeaponEquipType.oneHanded)
            {
                if(equipments.weapons[(int)newWeapon.WeaponEquipType] == null)
                {
                    equipments.weapons[(int)newWeapon.WeaponEquipType] = newWeapon;
                    stats.PhysicalDamage.Add(newWeapon.Damage);
                }
            }
        }
        else if(newEquipment is Armor)
        {
            Armor newArmor = (Armor)newEquipment;
            if(equipments.armors[(int)newArmor.ArmorEquipType] == null)
            {
                equipments.armors[(int)newArmor.ArmorEquipType] = newArmor;
            }
        }
    }
    public void UnEquip(Equipment oldEquipment)
    {
        //remove item.
    }
    #endregion
}