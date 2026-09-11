using UnityEngine;

public abstract class Skill
{
    public string SkillName { get; protected set; }
    public int Cooldown { get; protected set; }
    public Sprite Icon { get; protected set; }

    protected int currentCooldown;

    public bool IsReady()
    {
        return currentCooldown <= 0;
    }

    protected void StartCooldown()
    {
        currentCooldown = Cooldown;
    }

    public void ReduceCooldown()
    {
        if (currentCooldown > 0) currentCooldown--;
    }

    public void ResetCooldown()
    {
        currentCooldown = 0;
    }
}