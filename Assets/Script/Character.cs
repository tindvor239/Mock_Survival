using System.Collections.Generic;
using UnityEngine;
public class Character
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    private Stats stats = new Stats();
}

public class Stats
{
    [SerializeField]
    private ulong hp;
    [SerializeField]
    private Stat maxHp;
    [SerializeField]
    private ulong mana;
    [SerializeField]
    private Stat maxMana;
    [SerializeField]
    private Stat strength;
    [SerializeField]
    private Stat intellengent;
    [SerializeField]
    private Stat agility;
}

[System.Serializable]
public class Stat
{
    [SerializeField]
    private uint baseStat;
    [SerializeField]
    private List<uint> modifiers;

    public uint GetStat()
    {
        uint finalStat = baseStat;
        foreach (uint modifier in modifiers)
        {
            finalStat += modifier;
        }
        return finalStat;
    }
}