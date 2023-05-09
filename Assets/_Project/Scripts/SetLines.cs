using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetLines : MonoBehaviour
{
    [SerializeField] private int numberOfRouteTests;
    private int currentTestRoute;
    
    
    [SerializeField] private List<Transform> points;
    
    [SerializeField] private List<Transform> bestRoute;
    [SerializeField] private List<Transform> secondBestRoute;
    
    [SerializeField] private List<Transform> newGeneration;
    
    [SerializeField] private LineController line;
    private float distance;

    [SerializeField] private float bestDistance;
    [SerializeField] private float secondBestDistance;

    [SerializeField] private int generation;
    [SerializeField] private int mutations;
    [SerializeField] private float chanceOfMutation;
    
    private float timeNewTest;
    private float currentTimeNewTest;
    
    [SerializeField] private bool _isStart;
    private float mutation;
    
    void Start()
    {
        mutations = 0;
        timeNewTest = (numberOfRouteTests * 4) /10;
        _isStart = false;
        generation = 0;
        currentTestRoute = numberOfRouteTests;
        RandomListInitializing();
        CheckDistance();
        line.SetUpline(points);

        bestDistance = distance;
        secondBestDistance = distance;
        
        //InvokeRepeating("StartTestRoutes", 3.0f, 3.0f);
    }

    private void Update()
    {
        timeNewTest = (numberOfRouteTests * 3) /10;
        
        if (currentTestRoute<numberOfRouteTests)
        {
            if (generation==1)
            {
                NewRandomRoutes();
            }
            else
            {
                NewRoutes();
            }
          
            currentTestRoute++;
        }

        currentTimeNewTest += Time.deltaTime;

        if (currentTimeNewTest>timeNewTest &&_isStart)
        {
            StartTestRoutes();
            currentTimeNewTest = 0;
        }
    }
    
    [ContextMenu("Start Test Routes")]
    public void StartTests()
    {
        _isStart = true;
    }
    [ContextMenu("Stop Test Routes")]
    public void StopTests()
    {
        _isStart = false;
    }


    public void StartTestRoutes()
    {
        if (_isStart)
        {
            generation ++;
            currentTestRoute = 0;
        }
  
    }

    public void NewRandomRoutes()
    {
        RandomList();
        CheckDistance();
        line.SetUpline(points);
    }
    public void NewRoutes()
    {
        GenerationList();
        CheckDistance();
        line.SetUpline(points);
    }

    void RandomListInitializing()
    {
        for (int i = 1; i < points.Count; i++)
        {
            // Gera um índice aleatório
            int randomIndex = Random.Range(1, points.Count);

            // Troca os valores entre as posições correspondentes aos índices sorteados
            Transform  temp = points[i];
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }
        
        //Array.Resize(ref points, points.Length+1);
        
        points.Add(points[0]);
    }

    void RandomList()
    {

        for (int i = 1; i < points.Count-1; i++)
        {
            // Gera um índice aleatório
            int randomIndex = Random.Range(1, points.Count-1);

            // Troca os valores entre as posições correspondentes aos índices sorteados
            Transform  temp = points[i];
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }
       // points[points.Count]=points[0];
    }

    private void GenerationList()
    {
        NewGeneration();
        points =new List<Transform>();

        for (int i = 0; i < newGeneration.Count; i++)
        {
            Transform  temp = newGeneration[i];
            points.Add(temp);
        }

        mutation = Random.Range(0, 100);

        if (mutation<=chanceOfMutation)
        {
            //Debug.Log($"mutation");

            mutations++;            
            
            int randomIndex = Random.Range(1, points.Count-1);
            int newRandomIndex = Random.Range(1, points.Count-1);
            Transform  temp = points[newRandomIndex];
            points[newRandomIndex] = points[randomIndex];
            points[randomIndex] = temp;
        }
    }

    void CheckDistance()
    {
        distance = 0;
        
        for (int i = 0; i < points.Count-1; i++)
        {
            distance += Vector3.Distance(points[i].position, points[i + 1].position);
        }

        if (distance<bestDistance&&distance>0f)
        {
            bestDistance = distance;
            bestRoute = new List<Transform>();
            
            for (int i = 0; i < points.Count; i++)
            {
                Transform  temp = points[i];
                bestRoute.Add(temp);
            }

       

        }else if (distance<secondBestDistance && distance>bestDistance &&distance>0f)
        {
            secondBestRoute = new List<Transform>();
            
            secondBestDistance = distance;
            for (int i = 0; i < points.Count; i++)
            {
                Transform  temp = points[i];
                secondBestRoute.Add(temp);
            }
        }
    }
    [ContextMenu("Show Best Route")]
    public void ShowBestRoute()
    {
        points = bestRoute;
        
        CheckDistance();
        line.SetUpline(points);
    }
    [ContextMenu("Show Second Best Route")]
    public void ShowSecondBestRoute()
    {
        points = secondBestRoute;
        
        CheckDistance();
        line.SetUpline(points);
    }
    [ContextMenu("New Generation")]
    private void NewGeneration()
    {
        bool isCityfree = true;
        
        newGeneration  = new List<Transform>();
        
        for (int i = 0; i < bestRoute.Count/2; i++)
        {
            Transform  temp = bestRoute[i];
            newGeneration.Add(temp);
        }

        for (int i = 0; i < secondBestRoute.Count; i++)
        {
       
            for (int j = 0; j < newGeneration.Count; j++)
            {
                isCityfree = true;
                if (newGeneration[j].GetComponent<City>().idCity == secondBestRoute[i].GetComponent<City>().idCity )
                {
                    isCityfree = false;
                    break;
                }
            }

            if (isCityfree)
            {
                Transform  temp = secondBestRoute[i];
                newGeneration.Add(temp);
            }
          
        }
        
       newGeneration.Add(bestRoute[0]);
    }
}
