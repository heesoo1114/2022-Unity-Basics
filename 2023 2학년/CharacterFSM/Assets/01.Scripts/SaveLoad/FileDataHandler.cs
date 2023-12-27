
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class FileDataHandler
{
    private string _directoryPath = "";
    private string _filename = "";

    private bool _isEncrypt;

    public FileDataHandler(string directoryPath, string filename, bool isEncrypt)
    {
        _directoryPath = directoryPath;
        _filename = filename;
        _isEncrypt = isEncrypt;
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(_directoryPath, _filename);

        try
        {
            Directory.CreateDirectory(_directoryPath);
            string dataToStore = JsonUtility.ToJson(gameData, true); //¿¹»Ú°Ô Ãâ·ÂµÅ

            if(_isEncrypt)
            {
                dataToStore = EncryptAndDecryptData(dataToStore);
            }

            using(FileStream writeStream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.Write(dataToStore);
                }
            }
        }catch (Exception ex)
        {
            Debug.LogError($"Error on trying to save data to file {fullPath} \n");
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine( _directoryPath, _filename);
        GameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream readStream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(readStream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (_isEncrypt)
                {
                    dataToLoad = EncryptAndDecryptData(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception ex)
            {
                Debug.LogError($"Error on trying to load data to file {fullPath} \n");
            }
        }

        return loadedData;
    }

    public void DeleteSaveData()
    {
        string fullPath = Path.Combine(_directoryPath, _filename);

        if(File.Exists(fullPath) )
        {
            try
            {
                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error on trying to delete file {fullPath} \n");
            }
        }
    }

    private string _codeWord = "ggm_highschool";

    private string EncryptAndDecryptData(string data)
    {
        StringBuilder sBuilder = new StringBuilder();

        for(int i = 0; i < data.Length; ++i)
        {
            sBuilder.Append((char)(data[i] ^ _codeWord[i % _codeWord.Length]));
        }

        return sBuilder.ToString();
    }

}
