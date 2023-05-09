using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawsCitys : MonoBehaviour
{
    [SerializeField] private GameObject _cityPrefab;
    [SerializeField] private SetLines _set;
    public int countCitys;

    public float MaxZ;
    public float MinZ;

    public float MaxX;
    public float MinX;
    
    [ContextMenu("CreateCitys")]
    public void CreateCitys()
    {
        for (int i = 0; i < countCitys; i++)
        {
            GameObject obj = Instantiate(_cityPrefab, new Vector3(Random.Range(MinX, MaxX), 0, Random.Range(MinZ, MaxZ)),
                Quaternion.identity);
            
            _set.AddPoint(obj.transform);
        }
        
        _set.Initializing();
    }
    [ContextMenu("DestroyAllCitys")]
    public void DestroyAllCitys()
    {
        for (int i = 0; i < _set.points.Count; i++)
        {
            Destroy(_set.points[i].gameObject);
        }
        _set.points = new List<Transform>();
        _set.ResetTests();
    }
    
}
