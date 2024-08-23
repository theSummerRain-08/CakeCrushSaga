using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviourSingletonPersistent<ObjectPooling>
{
    [SerializeField] Transform holder; // Holder for pooled objects
    [SerializeField] protected GameObject[] objectToSpawn; 
    [SerializeField] protected List<GameObject> poolObj = new List<GameObject>(); 
    
    public GameObject GetObjectFromPool(GameObject prefab, Vector3 spawnPoint, Quaternion spawnRotation, Transform parent)
    {
        foreach (GameObject obj in poolObj)
        {
            if (obj.name == prefab.name)
            {
                poolObj.Remove(obj);
                obj.transform.position = spawnPoint;
                obj.transform.rotation = spawnRotation;
                obj.SetActive(true);
                obj.transform.parent = parent;
                return obj;
            }
        }
        
        GameObject newObject = Instantiate(prefab, spawnPoint, spawnRotation, parent);
        newObject.name = prefab.name;
        return newObject;
    }
    
    public void DeSpawn(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = holder;
        poolObj.Add(obj);
    }
}