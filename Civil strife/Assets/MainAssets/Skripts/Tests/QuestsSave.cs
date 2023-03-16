using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class QuestsSave : MonoBehaviour
{
    public static void SavePlayerQuests(QuestsInfo QI)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/lolipop1.log";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, QI);
        stream.Close();
        Debug.Log("Save");
    }
    public static void SavePlayerQuests2(QuestsInfo2 QI)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/lolipop2.log";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, QI);
        stream.Close();
        Debug.Log("Save");
    }

}
