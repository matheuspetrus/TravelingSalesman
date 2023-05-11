using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class City : MonoBehaviour
{
    public int idCity;

    public GameObject[] _citysPrefabs;
    
    private void Start()
    {
        idCity = Random.Range(0, 19999);

        int rand = Random.Range(0, _citysPrefabs.Length);
        
       GameObject obj = Instantiate(_citysPrefabs[rand], transform.position,transform.rotation);
       
     
    }
}
