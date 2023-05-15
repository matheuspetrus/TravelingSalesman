using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawsCitys : MonoBehaviour
{
    [SerializeField] private GameObject _cityPrefab;
    [SerializeField] private SetLines _set;
    public int countCitys;
    [SerializeField] private TMP_InputField _inputFieldCitys;
    public float MaxZ;
    public float MinZ;

    public float MaxX;
    public float MinX;
    
    [ContextMenu("CreateCitys")]
    public void CreateCitys()
    {
        if (!string.IsNullOrWhiteSpace(_inputFieldCitys.text))
        {
            countCitys = int.Parse(_inputFieldCitys.text);
        }
        else
        {
            countCitys = 10;
        }
      
        
        for (int i = 0; i < countCitys; i++)
        {
          
            GameObject obj = Instantiate(_cityPrefab, new Vector3(Random.Range(MinX, MaxX), 0, Random.Range(MinZ, MaxZ)),
                _cityPrefab.transform.rotation);

            if (i==0)
            {
                obj.GetComponent<City>().StartFlag();
            }
            
            _set.AddPoint(obj.transform);
        }
        
        _set.Initializing();
    }
    [ContextMenu("DestroyAllCitys")]
    public void DestroyAllCitys()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
        //
        // for (int i = 0; i < _set.points.Count; i++)
        // {
        //     int t = i;
        //     Destroy(_set.points[t].gameObject);
        // }
        //
        //
        // _set.points = new List<Transform>();
        // _set.ResetTests();
    }
    
}
