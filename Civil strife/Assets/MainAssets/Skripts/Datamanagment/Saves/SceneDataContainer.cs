[System.Serializable]
public class SceneDataContainer 
{
    public bool isRespawn;

    public bool[] withRespawn;
    public bool[] withoutRespawn;

    public int idOfScene;

    public SceneDataContainer(SceneData data)
    {
        isRespawn = data.isRespawn;

        withRespawn = data.withRespawn;
        withoutRespawn = data.withoutRespawn;

        idOfScene = data.idOfScene;
    }
}
