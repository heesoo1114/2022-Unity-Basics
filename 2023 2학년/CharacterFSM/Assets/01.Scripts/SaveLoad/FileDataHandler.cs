using System;
using System.IO;
using System.Text;
using UnityEngine;

public class FileDataHandler
{
    private string _directoryPath = "";
    private string _filename = "";

    private bool _isEncrypt;
    private bool _isBase64;

    private CryptoModule _cryptoModule;

    public FileDataHandler(string directoryPath, string filename, bool isEncrypt, bool isBase64 = false)
    {
        _directoryPath = directoryPath;
        _filename = filename;
        _isEncrypt = isEncrypt;
        _isBase64 = isBase64;
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(_directoryPath, _filename);

        try
        {
            Directory.CreateDirectory(_directoryPath);
            string dataToStore = JsonUtility.ToJson(gameData, true); //예쁘게 출력돼

            if(_isEncrypt)
            {
                dataToStore = _cryptoModule.AESEncrypt256(dataToStore);
                // dataToStore = EncryptAndDecryptData(dataToStore);
            }

            // if (_isBase64)
            // {
            //     dataToStore = Base64Process(dataToStore, true);   
            // }

            using(FileStream writeStream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
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
                    dataToLoad = _cryptoModule.Decrypt(dataToLoad);
                    // dataToLoad = EncryptAndDecryptData(dataToLoad);
                }

                // if (_isBase64)
                // {
                //     dataToLoad = Base64Process(dataToLoad, false);
                // }

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

    // 인코딩 -> 표현하는 거        디코딩 => 원래대로 돌려주는 거
    private string Base64Process(string data, bool encoding)
    {
        if (encoding)
        {
            byte[] dataByteArr = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(dataByteArr); // base64 문자열로 나타낸다.
        }
        else
        {
            byte[] dataByteArr = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(dataByteArr); // 
        }
    }

}
