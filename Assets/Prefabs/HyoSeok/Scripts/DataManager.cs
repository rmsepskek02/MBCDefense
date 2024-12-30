using UnityEngine;
using System.IO;
namespace Defend.Manager
{
    /// <summary>
    /// ���̺� �ε� ��� 
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        static GameObject container;

        //�̱���
        static DataManager instance;
        public static DataManager Instance
        {

            get
            {
                if (!instance)
                {
                    container = new GameObject();
                    container.name = "DataManager";
                    instance = container.AddComponent(typeof(DataManager)) as DataManager;
                    if (GameObject.Find("Managers") != null)
                    {
                        container.transform.SetParent(GameObject.Find("Managers").transform);
                    }
                    DontDestroyOnLoad(container);
                }

                return instance;
            }
        }

        //���� ������ �����̸� .json
        string GameDataFileName = "GameData.json";

        //�����ϱ�

        public void SaveGameData(Data data)
        {
            // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
            //�⺻�� false �� �鿩���� ���� �ȵ�����
            string ToJsonData = JsonUtility.ToJson(data, true);
            //���ã��
            string dataPath = Application.persistentDataPath + "/" + GameDataFileName;
            Debug.Log(dataPath);
            // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
            File.WriteAllText(dataPath, ToJsonData);
            //Debug.Log(data.money);
            // �ùٸ��� ����ƴ��� Ȯ�ο�
            //Debug.Log("���� �Ϸ�");

        }

        //�ҷ�����
        public Data LoadGameData()
        {
            //�ʱ�ȭ ��

            Data data = null;

            //������: C:/ Users /< User >/ AppData / LocalLow /< CompanyName >/< ProductName > ��� ��ȯ.
            //�ȵ���̵�: /data/data/<package_name>/files/ ��� ��ȯ.
            //iOS: ���� ����ڽ� ���丮 ���� ���� ������ ��� ��ȯ.
            string dataPath = Application.persistentDataPath + "/" + GameDataFileName;
            
            // ����� ������ �ִٸ�
            if (File.Exists(dataPath))
            {

                // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
                string FromJsonData = File.ReadAllText(dataPath);
                data = JsonUtility.FromJson<Data>(FromJsonData);
                print($"�ҷ����� �Ϸ�");
            }
            else
            {
                data = new Data();
            }

            return data;
        }
    }
}
