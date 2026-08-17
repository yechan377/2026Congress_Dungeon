using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStat", menuName = "Data/EnemyStat")]
public class EnemyStat : ScriptableObject
{
    public string enemyName;
    public int baseHp;
    public int hpPerLevel;
    public int baseDamage;
    public int damagePerLevel;
    public int defence;
    public int speed;
    public int expReward;
    public bool isBoss;
}
