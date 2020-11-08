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
        if(other.tag == "Enemy" && other.GetComponent<Character>() != null)
        {
            hitTargets.Add(other.GetComponent<Character>());
            hitTargets = hitTargets.Distinct().ToList();
        }
    }
}
