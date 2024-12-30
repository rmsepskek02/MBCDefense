using Defend.TestScript;
using UnityEngine;

public class HealthBasedCastle : MonoBehaviour
{
    //[SerializeField]
    //[Range(0f, 100f)]
    public float castleHealth;

    private Health health;  // ���ƴ� ��ũ��Ʈ 'Health' ����
    private float maxCastleHealth;  // �ִ�ü��

    public GameObject castleState01;
    public GameObject castleState02;
    public GameObject castleState03;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        maxCastleHealth = health.maxHealth; // �ִ�ü�� �ҷ�����

        health.OnDamaged += ChangeHealth;
        health.OnHeal += ChangeHealth;

        //castleState01 = transform.GetChild(1).gameObject;
        //castleState02 = transform.GetChild(2).gameObject;
        //castleState03 = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        maxCastleHealth = health.maxHealth; // ������Ʈ���� �ִ�ü�� �׻� üũ (ü�� ���׷��̵� ���)

        if (health.GetRatio() >= 0.7f)  // ����HP�� �ִ�ü���� 70�ۼ�Ʈ�� ���ų� ������
        {
            castleState01.SetActive(true);
            castleState02.SetActive(false);
            castleState03.SetActive(false);
        }
        else if (health.GetRatio() >= 0.3f) // ����HP�� �ִ�ü���� 30�ۼ�Ʈ�� ���ų� ������
        {
            castleState01.SetActive(false);
            castleState02.SetActive(true);
            castleState03.SetActive(false);
        }
        else
        {
            castleState01.SetActive(false);
            castleState02.SetActive(false);
            castleState03.SetActive(true);
        }
        
    }

    private void ChangeHealth(float amount)
    {
        castleHealth += amount;
    }
}
