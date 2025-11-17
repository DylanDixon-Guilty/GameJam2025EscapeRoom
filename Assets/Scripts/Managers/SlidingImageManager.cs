using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SlidingImageManager : MonoBehaviour
{
    public List<GameObject> SlidingImage = new List<GameObject>();
    public List<GameObject> SlidingImageSolution = new List<GameObject>();
    public GameObject Group_SlidingImage;

    private void Awake()
    {
        Shuffle();
    }

    public void Shuffle()
    {
        List<int> indexes = new List<int>{0, 1, 2, 3, 4, 5, 6, 7, 8};


        for (int i = 0; i < SlidingImage.Count; i++)
        {
            if(i == SlidingImage.Count)
            {
                break;
            }
            int index = UnityEngine.Random.Range(0, indexes.Count);
            SlidingImage.Insert(index, SlidingImage[i]);
            SlidingImage.Remove(SlidingImage[i]);
            SlidingImage[i].transform.SetSiblingIndex(index);
            if(i == SlidingImage.Count)
            {
                break;
            }
        }
        SlidingImage.Clear();
        for (int i = 0; i < Group_SlidingImage.gameObject.transform.childCount; i++)
        {
            GameObject child = Group_SlidingImage.gameObject.transform.GetChild(i).gameObject;
            SlidingImage.Add(child);
        }
    }
}
