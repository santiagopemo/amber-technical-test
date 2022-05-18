using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFighter : Character
{
    public float attackPower;
    public float attackRate;
    public string attackToTag;

    [SerializeField] protected Character opponent = null;

    private float _delay;

    protected virtual void Start()
    {
        _delay = attackRate;
    }

    protected virtual void Update()
    {
        Utilities.UpdateTimer(opponent != null, ref _delay, attackRate, MeleeAttack);
    }
    private void MeleeAttack()
    {
        opponent.TakeDamage(attackPower);
    }

    protected virtual void SetOpponentFromCollider(Collider other, bool setIt = true)
    {
        Character character = other.GetComponent<Character>();
        if (character && character.tag.Equals(attackToTag))
        {
            opponent = setIt ? character : null;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        SetOpponentFromCollider(other);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        SetOpponentFromCollider(other, false);
    }
}
