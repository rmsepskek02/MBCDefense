using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Defend.Enemy;
namespace Defend.TestScript
{
    /// <summary>
    /// 체력을 관리하는 클래스
    /// </summary>
    public class Health : MonoBehaviour
    {
        #region Variables

        //체력관련
        public float maxHealth = 100f;    //최대 Hp
        public float CurrentHealth { get; set; }    //현재 Hp

        public Image CastleHealthBar;
        //아머 관련
        public float baseArmor = 5f;
        public float CurrentArmor { get; set; }

        private bool isDeath = false;                       //죽음 체크
        //체젠 관련
        public float RgAmount;          //체젠량
        public float Rginterval;        //체젠 간격
        [SerializeField] private bool isHpTime = false;                      //체젠 체크
        //UnityAction
        public UnityAction<float> OnDamaged;
        public UnityAction OnDie;
        public UnityAction<float> OnHeal;
        public UnityAction<float> Armorchange;

        public float GetRatio() => CurrentHealth / maxHealth;

        private ListSpawnManager listSpawnManager;

        #endregion
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            listSpawnManager = FindAnyObjectByType<ListSpawnManager>();
            maxHealth *= (listSpawnManager.waveCount + 1);
            CurrentHealth = maxHealth;
            CurrentArmor = baseArmor;
            HPTime(RgAmount, Rginterval);//1초마다 1의 체력을 회복
        }

        public void chakeC()
        {
            if(transform.name == "Castle")
            {
                CastleHealthBar.fillAmount = CurrentHealth/maxHealth;
            }
        }

        //맥스 체력 올리기
        public void IncreaseMaxHealth(float amount)
        {
            maxHealth += amount;
            //Debug.Log("Max Health up " + maxHealth);
        }

        //체력 리젠
        public void HPTime(float amount, float interval)
        {
            StartCoroutine(RegenerateHealth(amount, interval));
        }

        public IEnumerator RegenerateHealth(float amount, float interval)
        {
            if (isHpTime == true)
            {


                while (true)
                {
                    amount = RgAmount;

                    CurrentHealth += amount;
                    // 최대 체력을 초과하지 않도록 제한
                    CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);

                    // 지정된 간격만큼 대기
                    yield return new WaitForSeconds(interval);
                }
            }
        }
        //힐 Real Method
        /*
        public void Heal(float amount)
        {
            //죽었으면 실행하지 않음
            if (isDeath) return;

            // 힐 적용 전 체력 저장
            float beforeHealth = CurrentHealth;

            // 힐 적용
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, CurrentHealth, maxHealth);

            // 실제 힐량 계산
            float realHeal = CurrentHealth - beforeHealth;

            //**************************************************
            // 오버힐량 계산 => ex) 만약 힐량이 최대체력보다 높아지면 쉴드값을 준다거나, 추가적인 버프를 준다거나 필요시 사용! 
            //float overheal = Mathf.Max(0f, amount - realHeal);
            //**************************************************

            // 디버깅 또는 로그 출력
            //Debug.Log($"Healed: {realHeal}, Current Health: {CurrentHealth}");
            if (realHeal > 0f)
            {
                //힐이펙트 구현
                OnHeal?.Invoke(realHeal);
            }
        }
        */
        //테스트용 heal
        public void Heal(float amount)
        {
            //죽었으면 실행하지 않음
            if (isDeath) return;

            // 힐 적용 전 체력 저장
            float beforeHealth = CurrentHealth;

            // 힐 적용
            amount = amount / 100;
            CurrentHealth = Mathf.Clamp(CurrentHealth + (maxHealth * amount), CurrentHealth, maxHealth); 

            // 실제 힐량 계산
            float realHeal = CurrentHealth - beforeHealth;

            //**************************************************
            // 오버힐량 계산 => ex) 만약 힐량이 최대체력보다 높아지면 쉴드값을 준다거나, 추가적인 버프를 준다거나 필요시 사용! 
            //float overheal = Mathf.Max(0f, amount - realHeal);
            //**************************************************

            // 디버깅 또는 로그 출력
            //Debug.Log($"Healed: {realHeal}, Current Health: {CurrentHealth}");
            if (realHeal > 0f)
            {
                //힐이펙트 구현
                OnHeal?.Invoke(realHeal);
            }
        }

        //damageSource: 데미지를 주는 주체
       
        public void TakeDamage(float damage)
        {
            //죽었으면 실행하지 않음
            if (isDeath) return;

            // 방어력 적용 후 최종 데미지 계산
            float mitigatedDamage = Mathf.Max(damage - CurrentArmor, 0); // Clamp 대신 Max 사용 (더 간결)

            // 실질적으로 들어온 데미지 계산
            float realDamage = Mathf.Min(CurrentHealth, mitigatedDamage);

            if (realDamage > 0f)
            {
                // 체력 감소, 데미지이펙트 구현
                CurrentHealth -= realDamage;
                OnDamaged?.Invoke(-realDamage);
            }
            //Debug.Log($"Damage Taken: {realDamage}, Remaining Health: {CurrentHealth}");
            // 체력이 0 이하라면 죽음 처리
            if (CurrentHealth <= 0f)
            {
                HandleDeath();
            }
        }
        //아머값 변경시 호출될 메서드
        public void ChangedArmor(float amount)
        {
            CurrentArmor += amount;
            Armorchange?.Invoke(amount);
        }

        //죽음 처리 관리
        void HandleDeath()
        {
            //죽음 체크
            if (isDeath)
                return;

            if (CurrentHealth <= 0f)
            {
                isDeath = true;
                //죽음 구현
                OnDie?.Invoke();
            }
        }
    }
}