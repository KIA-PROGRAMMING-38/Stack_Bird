using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool; // pool에 추가할 양

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        // 풀링된 개체 목록을 반복하여 비활성화하고 목록에 추가합니다
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // Spawn Manager의 자식으로 설정
        }
    }

    public GameObject GetPooledObject()
    {
        // 풀링된 Objects(개체) 목록에 있는 개체 수만큼
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // 풀링된 개체가 활성화되지 않은 경우 해당 개체 반환
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // 그렇지 않으면 null을 반환
        return null;
    }

    public void SetOrderInLayer()
    {
        SortedList<float, SpriteRenderer> sortedList = new SortedList<float, SpriteRenderer>();

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // 풀링된 개체가 활성화되지 않은 경우 해당 개체 반환
            if (!pooledObjects[i].activeInHierarchy)
                continue;

            sortedList.Add(pooledObjects[i].transform.position.y, pooledObjects[i].GetComponent<SpriteRenderer>());
        }

        int count = 0;
        foreach (var playerClone in sortedList)
        {
            playerClone.Value.sortingOrder = ++count;
        }
    }
}
