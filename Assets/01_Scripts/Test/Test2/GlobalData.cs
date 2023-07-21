using UnityEngine;

[CreateAssetMenu(fileName = "GlobalDB")]
public class GlobalData : ScriptableObject
{
    public int playerExp = 0;

    // private static GlobalData instance;
    // public static GlobalData Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             instance = new GlobalData();
    //         }
    //         return instance;
    //     }
    // }
    // 
    // public float playerExp = 0;
}
