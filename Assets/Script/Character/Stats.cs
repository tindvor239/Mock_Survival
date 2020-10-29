using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField]
    private uint hp = 1;
    [SerializeField]
    private Stat maxHP;
    [SerializeField]
    private uint mp = 1;
    [SerializeField]
    private Stat maxMP;
    [SerializeField]
    private Stat strength; // physical dmg, maxhp.
    [SerializeField]
    private Stat intelligence; // magic dmg, max mana.
    [SerializeField]
    private Stat agility; // movement speed, attack speed.

    #region Properties
    public uint HP
    {
        get => hp;
        set => hp = value;
    }
    public uint MP
    {
        get => mp;
        set => mp = value;
    }
    public Stat MaxHP { get => maxHP; }
    public Stat MaxMP { get => maxMP; }
    public Stat Strength { get => strength; }
    public Stat Intelligence { get => intelligence; }
    public Stat Agility { get => agility; }
    #endregion
    #region Method

    #endregion
}
