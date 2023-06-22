using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Assets.Scripts.SaveData;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
public class FileManager
{
    public static void WriteToFile(string fileName, string data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(data);
            }

            Debug.Log("File written successfully : " + filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error writing file: " + ex.Message);
        }
    }

    public static int ReadScoreFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Score.txt");
        int score = -1;
        using (StreamReader reader = new StreamReader(filePath))
        {
            string data = reader.ReadToEnd();
            score = int.Parse(data);
        }
        return score;
    }

    public static void SavePlayerData()
    {
        string fileName = "PlayerData.json";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log($"Lưu Player Data tại vị trí: {filePath}");

        PlayerData playerData = new PlayerData(GameManager.instance.player);
        string json = JsonConvert.SerializeObject(playerData, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);

        Debug.Log("Dữ liệu người chơi đã được lưu thành công.");

    }

    public static void SaveEnemiesData()
    {
        string fileName = "EnemyData.json";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log($"Lưu Enemies Data tại vị trí: {filePath}");

        List<EnemyData> enemyDatas = EnemyData.ListEnemiesData(GameManager.instance.CurEnemies);
        Debug.Log($"Số lượng enemies: {enemyDatas.Count}");
        string json = JsonConvert.SerializeObject(enemyDatas, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);

        Debug.Log("Dữ liệu Enemies đã được lưu thành công.");
    }
    public static List<Enemies> ReadEnemiesData()
    {
        string fileName = "EnemyData.json";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        List<Enemies> enemies= null;
        Debug.Log($"Đọc Enemies Data tại vị trí : {filePath}");
        // Kiểm tra xem tệp tin có tồn tại không
        if (File.Exists(filePath))
        {
            // Đọc nội dung từ tệp tin
            string json = File.ReadAllText(filePath);
            List<EnemyData> enemyDatas = JsonConvert.DeserializeObject<List<EnemyData>>(json);
            enemies = EnemyData.ListEnemies(enemyDatas);
        }
        else
        {
            Debug.Log("Tệp tin không tồn tại");
        }
       
        return enemies;

    }

    public static PlayerData ReadPlayerData()
    {
        string fileName = "PlayerData.json";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        PlayerData playerData = null;
        Debug.Log($"Đoc Player Data tại vị trí : {filePath}");
        // Kiểm tra xem tệp tin có tồn tại không
        if (File.Exists(filePath))
        {
            // Đọc nội dung từ tệp tin
            string json = File.ReadAllText(filePath);
            playerData = JsonConvert.DeserializeObject<PlayerData>(json);
        }
        else
        {
            Debug.Log("Tệp tin không tồn tại");
        }


        return playerData;
    }

 
}
