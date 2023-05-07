using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetLines : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    
    
    // Start is called before the first frame update
    void Start()
    {
        RandomList();
        
        line.SetUpline(points);
    }

    void RandomList()
    {
        for (int i = 1; i < points.Length; i++)
        {
            // Gera um índice aleatório
            int randomIndex = Random.Range(1, points.Length);

            // Troca os valores entre as posições correspondentes aos índices sorteados
            Transform  temp = points[i];
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }
        
        Array.Resize(ref points, points.Length+1);
        
        points[points.Length-1] = points[0];
    }

}
