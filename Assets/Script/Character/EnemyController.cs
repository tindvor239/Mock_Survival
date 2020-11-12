using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Weapon startWeapon;
    protected override void Awake()
    {
        base.Awake();
        character.Equip(startWeapon);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        enemy.transform.localPosition = new Vector3(0, 0, 0);
        if(target != null)
        {
            movementMotor = SetMotor(movementMotor);
            agent.SetDestination(target.position);
            base.Running();
            bool canAttack = movementMotor <= 0 && target != null && target.gameObject.tag == "Player";
            Debug.Log(canAttack);
            if(currentAttackDelay <= 0f)
                Attack(canAttack, "Player");
        }
    }
}
