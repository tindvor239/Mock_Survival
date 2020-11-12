using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Stats
{
    [SerializeField]
    private int hp = 1;
    [SerializeField]
    private Stat maxHP;
    [SerializeField]
    private int mp = 1;
    [SerializeField]
    private Stat maxMP;
    [SerializeField]
    private Stat strength; // physical 1 dmg, 3 maxhp.
    [SerializeField]
    private Stat intelligence; // magic dmg, max mana.
    [SerializeField]
    private Stat agility; // movement speed, attack speed.
    [SerializeField]
    private Stat physicalDamage;
    [SerializeField]
    private Stat magicalDamage;
    [SerializeField]
    private Stat armorRating;
    #region Properties
    public int HP
    {
        get => hp;
        set => hp = Mathf.Clamp(value, 0, (int)maxHP.GetValue());
    }
    public int MP
    {
        get => mp;
        set => mp = Mathf.Clamp(value, 0, (int)maxMP.GetValue());
    }
    public Stat MaxHP
    {
        get
        {
            if(maxHP.Modifiers.Count != 0)
                maxHP.Modifiers[0] = strength.GetValue() * 3;
            else
                maxHP.Modifiers.Add(strength.GetValue() * 3);
            return maxHP;
        }
    }
    public Stat MaxMP { get => maxMP; }
    public Stat Strength { get => strength; }
    public Stat Intelligence { get => intelligence; }
    public Stat Agility { get => agility; }
    public Stat PhysicalDamage
    {
        get
        {
            physicalDamage.BaseStat = strength.GetValue();
            return physicalDamage;
        }
    }
    public Stat MagicalDamage
    {
        get
        {
            magicalDamage.BaseStat = intelligence.GetValue();
            return magicalDamage;
        }
    }
    public Stat ArmorRating { get => armorRating; }
    #endregion
}

[System.Serializable]
public class Stat
{
    [SerializeField]
    private uint baseStat = 1;
    [SerializeField]
    private List<uint> modifiers = new List<uint>();
    #region Properties
    public uint BaseStat { get => baseStat; set => baseStat = value; }
    public List<uint> Modifiers { get => modifiers; }
    #endregion
    #region Constructor
    public Stat()
    {
        baseStat = 1;
        modifiers = null;
    }
    public Stat(uint baseStat, List<uint> modifiers)
    {
        this.baseStat = baseStat;
        this.modifiers = modifiers;
    }
    #endregion
    #region Bahavior
    public uint GetValue()
    {
        uint finalStat = baseStat;
        if(modifiers.Count != 0)
        {
            foreach (uint modifier in modifiers)
            {
                finalStat += modifier;
            }
        }
        return finalStat;
    }
    public void Add(uint stat)
    {
        if (stat != 0)
            modifiers.Add(stat);
    }
    public void Remove(uint stat)
    {
        if (stat != 0)
            modifiers.Remove(stat);
    }
    #endregion
}