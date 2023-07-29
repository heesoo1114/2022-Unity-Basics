using UnityEngine;

[CreateAssetMenu(fileName = "ShareDB")]
public class ShareDB : ScriptableObject
{
    [SerializeField]
    private int sharedValue;

    public int SharedValue
    {
        get { return sharedValue; }
        set { sharedValue = value; }
    }
}
