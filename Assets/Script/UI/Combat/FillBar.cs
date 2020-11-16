using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class FillBar : MonoBehaviour
{
    [SerializeField]
    protected Character character;
    protected Slider slider;
    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }
    protected virtual void Update()
    {
        if (character != null)
            GetCharacterStatToFill(character.Stats.HP, character.Stats.MaxHP.GetValue());
        else
            Console.Instance.Print(string.Format("Error: Don't Have Any Character In {0}, Are You Missing Something?", name), Color.red);
    }
    protected void GetCharacterStatToFill(float stat, float maxStat)
    {
        slider.value = stat / maxStat;
    }
}
