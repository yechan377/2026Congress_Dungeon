public abstract class EnemySkill : Skill
{
    public bool CanUse()
    {
        return IsReady();
    }

    public abstract void Execute(Character caster, Character target);
}
