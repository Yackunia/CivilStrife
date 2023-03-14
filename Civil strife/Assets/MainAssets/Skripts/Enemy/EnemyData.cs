

[System.Serializable]
public class EnemyData
{
    public bool[] isAliveEnemy;


    public EnemyData(EnemySaving pl)
    {
        isAliveEnemy = pl.isAliveEnemy;
    }

}
