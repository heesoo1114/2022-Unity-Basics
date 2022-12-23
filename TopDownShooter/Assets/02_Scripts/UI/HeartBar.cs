using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{
    private List<HeartImage> _imageList = new List<HeartImage>();

    private void Awake()
    {
        transform.GetComponentsInChildren<HeartImage>(_imageList);
    }

    public void SetHeartBar(int hp)
    {
        for (int i = 0; i < _imageList.Count; i++)
        {
            _imageList[i].SetHeartImage(i < hp);
        }
    }
}
