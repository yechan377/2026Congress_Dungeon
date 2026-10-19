using System.Collections.Generic;
using UnityEngine;

// 기획서 몬스터 표를 코드로 생성 (에셋 필요 없음)
public static class MonsterTable
{
    private static EnemyStat Make(string name, int baseHp, int hpPer, int baseDmg, int dmgPer, int def, int spd, int exp, bool boss = false, bool elite = false)
    {
        var s = ScriptableObject.CreateInstance<EnemyStat>();
        s.enemyName = name; s.baseHp = baseHp; s.hpPerLevel = hpPer;
        s.baseDamage = baseDmg; s.damagePerLevel = dmgPer;
        s.defence = def; s.speed = spd; s.expReward = exp;
        s.isBoss = boss; s.isElite = elite;
        return s;
    }

    // 일반 몬스터 [스테이지-1][종류]
    private static readonly EnemyStat[][] normals =
    {
        new[] { Make("1스테이지 몬스터1", 50, 10, 8, 2, 0, 8, 20),  Make("1스테이지 몬스터2", 40, 10, 10, 2, 0, 11, 30) },
        new[] { Make("2스테이지 몬스터1", 60, 10, 11, 2, 0, 11, 50), Make("2스테이지 몬스터2", 50, 10, 14, 3, 0, 13, 60) },
        new[] { Make("3스테이지 몬스터1", 70, 10, 13, 2, 25, 13, 80), Make("3스테이지 몬스터2", 60, 10, 17, 2, 10, 17, 90) },
    };
    private static readonly EnemyStat elite = Make("3스테이지 정예", 100, 8, 17, 2, 40, 15, 120, false, true);
    private static readonly EnemyStat[] bosses =
    {
        Make("1스테이지 보스", 210, 0, 20, 0, 30, 12, 150, true),
        Make("2스테이지 보스", 430, 0, 30, 0, 45, 14, 300, true),
        Make("3스테이지 보스", 680, 0, 40, 0, 60, 18, 600, true),
    };
    private static readonly int[] bossLevel = { 4, 7, 11 };
    private static readonly int[] minLevel = { 1, 4, 7 };

    public static List<EnemyData> Spawn(int stage, NodeType type, int maxCount)
    {
        int si = Mathf.Clamp(stage, 1, 3) - 1;
        List<EnemyData> list = new();

        switch (type)
        {
            case NodeType.Boss:
                list.Add(EnemyFactory.Create(bosses[si], bossLevel[si], stage));
                break;
            case NodeType.Elite:
                list.Add(EnemyFactory.Create(elite, 9, stage));
                break;
            default:
                int count = Random.Range(1, maxCount + 1);
                for (int i = 0; i < count; i++)
                {
                    var stat = normals[si][Random.Range(0, 2)];
                    int lv = Random.Range(minLevel[si], minLevel[si] + 3);
                    list.Add(EnemyFactory.Create(stat, lv, stage));
                }
                break;
        }
        return list;
    }

    public static string NameOf(EnemyData e) => "";
}
