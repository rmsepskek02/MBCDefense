using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defend.Enemy;
public class DayNightTimeCheck : MonoBehaviour
{
    [Header("��ī�� �ڽ�3��, Light �ֱ�")]
    [SerializeField] Material skybox1; // ��ħ
    [SerializeField] Material skybox2; // ����/������ ��
    [SerializeField] Material skybox3; // ��
    [SerializeField] Material skybox4; // 

    //[SerializeField] GameObject Lamp;

    [SerializeField] GameObject directionalLight; // ��ħ���� Ȱ��ȭ

    [SerializeField] int stageWave = 1; // �������� ���̺� (1, 2, 3 �� �ϳ��� ����)

    [SerializeField] private GameObject listSpawnManagerObject; // ListSpawnManager�� ���� GameObject�� �巡�׷� ����
    private ListSpawnManager listSpawnManager;

    void Start()
    {
        if (listSpawnManagerObject != null)
        {
            // Ư�� GameObject���� ListSpawnManager�� ������
            listSpawnManager = listSpawnManagerObject.GetComponent<ListSpawnManager>();

            if (listSpawnManager == null)
            {
                Debug.LogError("ListSpawnManager ������Ʈ�� ã�� �� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogError("ListSpawnManager�� ���� GameObject�� �������� �ʾҽ��ϴ�!");
        }
    }

    private void Update()
    {
        stageWave = listSpawnManager.waveCount;
        UpdateEnvironment(stageWave);
    }

    /*// �������� ���̺꿡 ���� ȯ�� ����
    public void SetStageWave(int newStageWave)
    {
        stageWave = newStageWave;
        UpdateEnvironment();
    }*/

    void UpdateEnvironment(int stageWave)
    {
        switch (stageWave)
        {
            case 1: // ��ħ
                //Lamp.SetActive(false);
                directionalLight.SetActive(true);
                RenderSettings.skybox = skybox1;
                break;

            case 2: // ����
                //Lamp.SetActive(false);
                directionalLight.SetActive(true);
                RenderSettings.skybox = skybox2;
                break;

            case 3: // ��
                //Lamp.SetActive(true);
                directionalLight.SetActive(false);
                RenderSettings.skybox = skybox3;
                break;

            case 4: // ��
                //Lamp.SetActive(true);
                directionalLight.SetActive(false);
                RenderSettings.skybox = skybox4;
                break;

            default:
                Debug.LogWarning("Invalid stageWave value: " + stageWave);
                break;
        }
    }
}
