using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Stats stats;
    #region Properties
    public float MovementSpeed { get => movementSpeed; }
    public float RotationSpeed { get => rotationSpeed; }
    public Stats Stats { get => stats; }
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
    public uint BaseStat { get => baseStat; }
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
        foreach (uint modifier in modifiers)
        {
            finalStat += modifier;
        }
        return finalStat;
    }
    public void SetBaseStat(uint stat)
    {
        baseStat = stat;
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
    public List<uint> SetModifier(List<uint> modifier)
    {
        List<uint> result = new List<uint>();
        for(int i = 0; i < modifiers.Count; i++)
        {
            result.Add(modifier[i]);
        }
        return result;
    }
    #endregion
}