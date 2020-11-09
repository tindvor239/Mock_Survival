using UnityEngine;

public class Character : MonoBehaviour
{
    #region Member Field
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private ParticleSystem targetParticle;
    [SerializeField]
    private Stats stats;
    [SerializeField]
    private CharacterEquipment equipments;
    [SerializeField]
    private GameObject[] weaponhands = new GameObject[2];
    [SerializeField]
    private WeaponHitDetect[] weaponHitDetects = new WeaponHitDetect[2];
    [SerializeField]
    private bool isAlive = true;
    #endregion
    public delegate void OnAttacking();
    public event OnAttacking onAttacking;
    #region Properties
    public float MovementSpeed { get => movementSpeed; }
    public float RotationSpeed { get => rotationSpeed; }
    public ParticleSystem TargetParticle { get => targetParticle; }
    public Stats Stats { get => stats; }
    public CharacterEquipment Equipments { get => equipments; }
    public WeaponHitDetect[] WeaponHitDetects { get => weaponHitDetects; }
    #endregion
    #region Method
    private void Awake()
    {
        if (targetParticle != null)
            targetParticle.Stop();
    }
    private void Update()
    {
        isAlive = stats.HP <= 0 ? false : true;
    }
    public void Equip(Equipment newEquipment)
    {
        if(newEquipment is Weapon)
        {
            Weapon newWeapon = (Weapon)newEquipment;
            if(newWeapon.WeaponEquipType == WeaponEquipType.oneHanded)
            {
                if(equipments.weapons[(int)newWeapon.WeaponEquipType] == null)
                {
                    GameObject newGameObject = Instantiate(newWeapon.EquipmentModel, weaponhands[(int)newWeapon.WeaponEquipType].transform);
                    newGameObject.name = newWeapon.name;
                    weaponHitDetects[(int)newWeapon.WeaponEquipType] = newGameObject.GetComponent<WeaponHitDetect>();
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
                stats.ArmorRating.Add(newArmor.ArmorRating);
            }
        }
    }
    public void UnEquip(Equipment oldEquipment)
    {
        if (oldEquipment is Weapon)
        {
            Weapon oldWeapon = (Weapon)oldEquipment;
            if (oldWeapon.WeaponEquipType == WeaponEquipType.oneHanded)
            {
                if (equipments.weapons[(int)oldWeapon.WeaponEquipType] != null)
                {
                    equipments.weapons[(int)oldWeapon.WeaponEquipType] = oldWeapon;
                    stats.PhysicalDamage.Remove(oldWeapon.Damage);
                }
            }
        }
        else if (oldEquipment is Armor)
        {
            Armor oldArmor = (Armor)oldEquipment;
            if (equipments.armors[(int)oldArmor.ArmorEquipType] != null)
            {
                equipments.armors[(int)oldArmor.ArmorEquipType] = oldArmor;
                stats.ArmorRating.Remove(oldArmor.ArmorRating);
            }
        }
    }
    public void Attacking()
    {
        if(onAttacking != null)
        {
            onAttacking.Invoke();
        }
    }
    #endregion
}