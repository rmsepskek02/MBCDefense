using Defend.Projectile;
using Defend.TestScript;
using Defend.Utillity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 슬로우타워 기능 정의
/// 범위 내 적 유닛에게 디버프를 부여하여 이동속도 저하
/// </summary>
namespace Defend.Tower
{
    public class SlowTower : MultipleTower
    {
        private HashSet<Transform> attackedTargets = new HashSet<Transform>(); // 이미 공격한 타겟 저장

        // TODO :: MultipleTower 도 호출하나 ? 최상위만하나 ?
        protected override void Start()
        {
            base.Start();
            
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Shoot()
        {
            // 슛 딜레이와 타겟 유효성 검사
            if (towerInfo.shootDelay >= shootTime) return;

            // 유효한 타겟들 가져오기
            var allTargets = UpdateTargets()
                .Where(target => target != null && target.GetComponent<Health>().CurrentHealth > 0) // 유효한 타겟 필터링
                .ToList();

            // 유효한 타겟이 없으면 종료
            if (allTargets.Count == 0) return;

            // 공격하지 않은 타겟만 대상으로 발사체 생성
            foreach (var target in allTargets)
            {
                if (!attackedTargets.Contains(target))
                {
                    // 발사체 인스턴스 생성
                    GameObject projectilePrefab = Instantiate(
                        towerInfo.projectile.prefab,
                        firePoint.transform.position,
                        Quaternion.identity
                    );

                    // 발사체 초기화
                    projectilePrefab.GetComponent<ProjectileBase>().Init(towerInfo.projectile, target);

                    // 타겟을 공격된 목록에 추가
                    attackedTargets.Add(target);

                    // 애니메이션 재생
                    if (animator != null)
                        animator.SetTrigger(Constants.ANIM_SHOOTTRIGGER);
                }
            }

            // 슛 타임 초기화
            shootTime = 0;
        }
    }
}