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
                    DontDestroyOnLoad(container);
                }

                return instance;
            }
        }

        //���� ������ �����̸� .json
        string GameDataFileName = "GameData.json";



        //�ҷ�����
        public void LoadGameData()
        {
            Data data = null;

            // ��� ���� //Application.dataPath ������Ʈ/���� ���� ����
            // �����츸 ��
            string dataPath = Application.dataPath + "/" + GameDataFileName;
            // ����� ������ �ִٸ�

            if (File.Exists(dataPath))
            {

                // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
                string FromJsonData = File.ReadAllText(dataPath);
                data = JsonUtility.FromJson<Data>(FromJsonData);
                Debug.Log(data.money);
                print("�ҷ����� �Ϸ�");
            }
            else
            {
                data = new Data(); // �⺻ ������ ����
            }

        }

        //�����ϱ�

        public void SaveGameData(Data data)
        {
            // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
            //�⺻�� false �� �鿩���� ���� �ȵ�����
            string ToJsonData = JsonUtility.ToJson(data, true);
            //���ã��
            string dataPath = Application.dataPath + "/" + GameDataFileName;

            // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
            File.WriteAllText(dataPath, ToJsonData);
            Debug.Log(data.money);
            // �ùٸ��� ����ƴ��� Ȯ�ο�
            Debug.Log("���� �Ϸ�");

        }


        public void DeleteGameData(Data data)
        {
            data = new Data();
            // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
            //�⺻�� false �� �鿩���� ���� �ȵ�����
            string ToJsonData = JsonUtility.ToJson(data, true);
            //���ã��
            string dataPath = Application.dataPath + "/" + GameDataFileName;

            // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
            File.WriteAllText(dataPath, ToJsonData);
        
            // �ùٸ��� ����ƴ��� Ȯ�ο�
          

        }
    }
}
