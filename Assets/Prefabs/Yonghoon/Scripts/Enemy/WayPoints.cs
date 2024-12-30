using UnityEngine;

namespace Defend.Enemy
{
    //Waypoint���� �����ϴ� Ŭ����
    public class WayPoints : MonoBehaviour
    {
        //�ʵ�
        public static Transform[] points;
        // Inspector�� ������ �ν��Ͻ� ����
        [SerializeField] private Transform[] serializedTransforms;
        private void Awake()
        {
            points = new Transform[this.transform.childCount];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = this.transform.GetChild(i);
            }
            serializedTransforms = points;
        }

    }
}