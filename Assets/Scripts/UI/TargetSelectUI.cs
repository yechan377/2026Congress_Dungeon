using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] targetButtons;

    private PlayerSkill pendingSkill;

    public void Show(List<EnemyData> enemies, PlayerSkill skill)
    {
        pendingSkill = skill;
        panel.SetActive(true);

        for (int i = 0; i < targetButtons.Length; i++)
        {
            bool hasEnemy = i < enemies.Count;
            targetButtons[i].gameObject.SetActive(hasEnemy);
            if (!hasEnemy) continue;

            EnemyData target = enemies[i];
            targetButtons[i].onClick.RemoveAllListeners();
            targetButtons[i].onClick.AddListener(() => OnClickTarget(target));
        }
    }

    private void OnClickTarget(EnemyData target)
    {
        panel.SetActive(false);
        BattleManager.Instance.UseSkillOnTarget(pendingSkill, target);
    }
}