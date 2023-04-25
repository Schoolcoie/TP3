using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void SaveData(InventoryManager inventoryManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string saveDataPath = Application.persistentDataPath + "/game.save";

        FileStream stream = new FileStream(saveDataPath, FileMode.Create);

        GameData data = new GameData(inventoryManager);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/game.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;

            return data;
        }
        else
        {
            return null;
        }
    }
}
