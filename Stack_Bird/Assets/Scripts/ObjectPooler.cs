using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool; // pool�� �߰��� ��

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        // Ǯ���� ��ü ����� �ݺ��Ͽ� ��Ȱ��ȭ�ϰ� ��Ͽ� �߰��մϴ�
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // Spawn Manager�� �ڽ����� ����
        }
    }

    public GameObject GetPooledObject()
    {
        // Ǯ���� Objects(��ü) ��Ͽ� �ִ� ��ü ����ŭ
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // Ǯ���� ��ü�� Ȱ��ȭ���� ���� ��� �ش� ��ü ��ȯ
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // �׷��� ������ null�� ��ȯ
        return null;
    }

    public void SetOrderInLayer()
    {
        SortedList<float, SpriteRenderer> sortedList = new SortedList<float, SpriteRenderer>();

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // Ǯ���� ��ü�� Ȱ��ȭ���� ���� ��� �ش� ��ü ��ȯ
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
