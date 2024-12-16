using UnityEngine;
using System.Collections.Generic;

public class TreeSpawner2 : MonoBehaviour
{
    // ������ų ������
    public GameObject treePrefab;

    // ������ų ��ġ �ټ� ����
    public Transform[] spawnPosition = new Transform[5];

    List<int> numberList = new List<int> { 0, 1, 2, 3, 4 };
    List<int> randomNumberList = new List<int>();

    void Start()
    {
        Debug.Log($"Initial numberList Count: {numberList.Count}");

        // ���� ������ ��迭
        while (numberList.Count > 0)
        {
            int randomIndex = Random.Range(0, numberList.Count);
            randomNumberList.Add(numberList[randomIndex]);
            numberList.RemoveAt(randomIndex);

            Debug.Log($"Added {randomNumberList[randomNumberList.Count - 1]} to randomNumberList.");
            Debug.Log($"numberList Count after removal: {numberList.Count}");
        }

        Debug.Log($"Random Number List Count: {randomNumberList.Count}");

        // �ε����� ���� ���
        for (int i = 0; i < randomNumberList.Count; i++)
        {
            Debug.Log($"Random Number at index {i}: {randomNumberList[i]}");
        }
    }

    void Update()
    {

    }
}
