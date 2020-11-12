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
        slider.value = (float)character.Stats.HP / (float)character.Stats.MaxHP.GetValue();
    }
}
