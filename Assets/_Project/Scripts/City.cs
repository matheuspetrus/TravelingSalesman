using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class City : MonoBehaviour
{
    public int idCity;

    private void Start()
    {
        idCity = Random.Range(0, 19999);
    }
}
