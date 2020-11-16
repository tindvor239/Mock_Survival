using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFillBar : FillBar
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(character == null && GameManager.Instance.Player != null)
        {
            if (GameManager.Instance.Player.GetComponentInChildren<Character>())
                character = GameManager.Instance.Player.GetComponentInChildren<Character>();
            else if (GameManager.Instance.Player.GetComponent<Character>())
                character = GameManager.Instance.Player.GetComponent<Character>();
            else
                Console.Instance.Print("Error: Didn't Found Any Character Class In GameManager, Please Check Again!", Color.red);
        }
        else
            GetCharacterStatToFill(character.Stats.HP, character.Stats.MaxHP.GetValue());
    }
}
