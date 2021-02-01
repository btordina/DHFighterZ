using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    StateManager states;

    public HandleDamageColliders.DamageType damageType;


    void Start()
    {
        states = GetComponentInParent<StateManager>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<StateManager>())
        {
            StateManager oState = other.GetComponentInParent<StateManager>();

            if (oState != states)
            {
                if (!oState.currentlyAttacking)
                {

                    oState.TakeDamage(10, damageType);
                }

            }

        }

    }

    
}
