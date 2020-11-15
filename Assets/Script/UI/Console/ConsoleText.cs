using UnityEngine;
using UnityEngine.UI;
public class ConsoleText : MonoBehaviour
{
    private float fadingTime = 2f, currentFadingTime;
    private Text text;
    private void Awake()
    {
        currentFadingTime = fadingTime;
        text = GetComponent<Text>();
    }

    private void Update()
    {
        Mathf.Clamp(currentFadingTime -= Time.deltaTime, 0, fadingTime);
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a * currentFadingTime);
        if(currentFadingTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
