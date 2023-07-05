using UnityEngine;

public class Bugai : EnemyV
{
    [SerializeField] private GameObject deadPart;

    protected override void Damage(float damage)
    {
        StopEnemy();
        DizableCombat();
        base.Damage(damage);
    }

    protected override void Death()
    {
        base.Death();
        deadPart.SetActive(true); 
    }
    protected override void EndPain()
    {
        base.EndPain();
        UnFreezeEnemy();
        EnableCombat();
    }
}
