using UnityEngine;

public class Bugai : EnemyV
{
    [SerializeField] private GameObject deadPart;

    [SerializeField] private AudioSource pain2;

    protected override void Damage(float damage)
    {
        pain2.Play();

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
