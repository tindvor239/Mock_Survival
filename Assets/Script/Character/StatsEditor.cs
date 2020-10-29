using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
/*
[CustomEditor(typeof(Stats))]
public class StatsEditor : Editor
{
    private Stats stats;
    private int maxHP, maxMP;
    private Stat strength, agility, intelligence;
    private void OnEnable()
    {
        stats = (Stats)target;
        strength = new Stat(stats.Strength.BaseStat, stats.Strength.Modifiers);
        agility = new Stat(stats.Agility.BaseStat, stats.Agility.Modifiers);
        intelligence = new Stat(stats.Intelligence.BaseStat, stats.Intelligence.Modifiers);
    }

    public override void OnInspectorGUI()
    {
        #region HP Int Field
        maxHP = EditorGUILayout.IntField("Max HP", maxHP);
        if(maxHP < 1)
        {
            maxHP = 1;
        }
        stats.MaxHP.SetBaseStat((uint)maxHP);
        #endregion
        #region MP Int Field
        maxMP = EditorGUILayout.IntField("Max MP ", maxMP);
        if (maxMP < 1)
        {
            maxMP = 1;
        }
        stats.MaxMP.SetBaseStat((uint)maxMP);
        #endregion
        #region HP slider
        Rect rectangle = EditorGUILayout.BeginVertical();
        stats.HP = (uint)EditorGUI.IntSlider(rectangle, "HP", (int)stats.HP, 0, (int)stats.MaxHP.GetValue());
        EditorGUILayout.Space(18);
        EditorGUILayout.EndVertical();
        #endregion
        #region MP slider
        rectangle = EditorGUILayout.BeginVertical();
        stats.MP = (uint)EditorGUI.IntSlider(rectangle, "MP", (int)stats.MP, 0, (int)stats.MaxMP.GetValue());
        EditorGUILayout.Space(18);
        EditorGUILayout.EndVertical();
        #endregion
        #region Strength Stat
        stats.Strength.SetBaseStat((uint)EditorGUILayout.IntField("Strength", (int)strength.BaseStat));
        sbyte count = (sbyte)EditorGUILayout.IntField("Size", strength.Modifiers.Count);
        for (int i = 0; i < strength.Modifiers.Count; i++)
        {
            string name = i.ToString();
            stats.Strength.Modifiers[i] = (uint)EditorGUILayout.IntField(name, (int)strength.Modifiers[i]);
        }
        stats.Strength.SetModifier(strength.Modifiers);
        #endregion
    }
}*/
