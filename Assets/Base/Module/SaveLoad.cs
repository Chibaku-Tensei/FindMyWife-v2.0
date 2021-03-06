using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

namespace Base.Module
{
    public static class SaveLoad
    {
        public static void SaveToBinary(object data, string fileName)
        {
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName, 
                FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, data);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                throw;
            }
            stream.Close();
        }

        public static void LoadFromBinary<T>(out T result, string filename)
        {
            string path = Application.persistentDataPath + "/" + filename;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                result = (T) formatter.Deserialize(stream);
                stream.Close();
            }
            else result = default;
        }

        public static bool SaveToJson(object data, string filename)
        {
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);
            try
            {
                string json = JsonUtility.ToJson(data);
                sw.WriteLine(json);
            }
            catch (Exception e)
            {
                return false;
            }
            sw.Close();
            stream.Close();
            return true;
        }

        public static bool LoadFromJson<T>(out T result, string filename)
        {
            string path = Application.persistentDataPath + "/" + filename;
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                StreamReader reader = new StreamReader(stream);
                string jsonStr = reader.ReadLine();
                result = JsonUtility.FromJson<T>(jsonStr);
                reader.Close();
                stream.Close();
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public static void SaveToTextFile(object data, string fileName)
        {
            FileStream fs = new FileStream(Application.persistentDataPath + "/" + fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.WriteLine((string) data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            sw.Close();
            fs.Close();
        }

        public static string LoadTextResource(string filePath)
        {
            try
            {
                var textResouce = Resources.Load<TextAsset>(filePath);
                return textResouce.text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
    }
}

