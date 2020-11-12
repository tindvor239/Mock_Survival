using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHitDetect : MonoBehaviour
{
    public List<Character> hitTargets;
    [SerializeField]
    private new Collider collider;
    public Collider Collider { get => collider; }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "Player")
        {
            Character character = null;
            if(other.GetComponent<Character>() != null)
                character = other.GetComponent<Character>();
            else if(other.GetComponentInChildren<Character>() != null)
                character = other.GetComponentInChildren<Character>();
            hitTargets.Add(character);
            hitTargets = hitTargets.Distinct().ToList();
            Debug.Log(other.tag == "Player");
        }
    }
}
