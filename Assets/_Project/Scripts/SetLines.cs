using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SetLines : MonoBehaviour
{

    [SerializeField] private TMP_Text _textGeneration;
    [SerializeField] private TMP_Text _textMutations;
    [SerializeField] private TMP_Text _textBestRoute;
    [SerializeField] private TMP_Text _textBestSecondBestRoute;
    
    [SerializeField] private TMP_Text _textSliderMutations;
    [SerializeField] private TMP_Text _textNumberOfRouteTests;

    [SerializeField] private Slider _sliderMutations;
    [SerializeField] private Slider _sliderNumberOfRouteTests;
    
    [SerializeField] private int numberOfRouteTests;
    private int currentTestRoute;
    
    
    public List<Transform> points;
    
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
    
    [SerializeField] private bool _isStart = false;
    private float mutation;
    
    void Start()
    {
    //    Time.timeScale = 20f;
    }

    private void Update()
    {
        chanceOfMutation = _sliderMutations.value;
        numberOfRouteTests = (int)_sliderNumberOfRouteTests.value;

        _textSliderMutations.text = $"Mutação: {chanceOfMutation}%";
        _textNumberOfRouteTests.text = $"Testes: {numberOfRouteTests}";
        
        
        timeNewTest = (numberOfRouteTests * 4) /10;
        
        if (currentTestRoute<numberOfRouteTests  && points.Count >0)
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

        if (currentTestRoute>=numberOfRouteTests &&_isStart && currentTimeNewTest>=timeNewTest)
        {
            StartTestRoutes();
            currentTimeNewTest = 0;
        }
    }

    public void AddPoint(Transform newCity)
    {
        points.Add(newCity);
    }
    

    [ContextMenu("Initializing")]
    public void Initializing()
    {
        mutations = 0;
        timeNewTest = (numberOfRouteTests * 4) /10;
        _isStart = false;
        generation = 0;

        _textGeneration.text = generation.ToString();
        
        currentTestRoute = numberOfRouteTests;
        RandomListInitializing();
        CheckDistance();
        line.SetUpline(points);

        bestDistance = distance;
        secondBestDistance = distance;
        
        _textBestRoute.text = bestDistance.ToString();
        _textBestSecondBestRoute.text = secondBestDistance.ToString();
        Debug.Log($"Initializing");
    }
    [ContextMenu("ResetTests")]
    public void ResetTests()
    {
        line.Clear();
        
        mutations = 0;
        timeNewTest = (numberOfRouteTests * 4) /10;
        _isStart = false;
        generation = 0;
        
        _textGeneration.text = generation.ToString();
        currentTestRoute = numberOfRouteTests;
        bestDistance = 0;
        secondBestDistance = 0;

        _textBestRoute.text = bestDistance.ToString();
        _textBestSecondBestRoute.text = secondBestDistance.ToString();
         points=new List<Transform>();
         bestRoute=new List<Transform>();
         secondBestRoute=new List<Transform>();
         newGeneration=new List<Transform>();
         Debug.Log($"ResetTests");
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
            _textGeneration.text = generation.ToString();
            currentTestRoute = 0;
        }
  
    }

    public void NewRandomRoutes()
    {
        RandomList();
        CheckDistance();
        line.SetUpline(points);
        Debug.Log($"NewRandomRoutes");
    }
    public void NewRoutes()
    {
        GenerationList();
        CheckDistance();
        line.SetUpline(points);
        Debug.Log($"NewRoutes");
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
            _textMutations.text = mutations.ToString();
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
            _textBestRoute.text = bestDistance.ToString();
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
            _textBestRoute.text = secondBestDistance.ToString();
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
        Debug.Log($"ShowBestRoute");
    }
    [ContextMenu("Show Second Best Route")]
    public void ShowSecondBestRoute()
    {
        points = secondBestRoute;
        
        CheckDistance();
        line.SetUpline(points);
        Debug.Log($"ShowSecondBestRoute");
    }
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
        
       newGeneration.Add(points[0]);
    }
}
