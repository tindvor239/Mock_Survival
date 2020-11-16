using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private void Start()
    {
        Vector2 offset = Random.insideUnitSphere * 3f;
        Vector3 position = new Vector3(transform.position.x + offset.x, 3, transform.position.z);
        GameObject newPlayer = Instantiate(player, position, Quaternion.identity);
        if (newPlayer.GetComponent<PlayerController>())
            GameManager.Instance.Player = newPlayer.GetComponent<PlayerController>();
        else
            Console.Instance.Print("Your Player Prefab Doesn't Have Player Controller, Please Put Correctly!!", Color.red);
    }
}
