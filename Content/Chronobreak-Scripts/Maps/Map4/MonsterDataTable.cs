namespace MapScripts.Map4;

//Map4 doesnt have a MonsterDataTable file
//TODO: Verify the values and research how stats progressed at the time
public static class MonsterDataTable
{
    static readonly List<float> AttackDamage = new()
    {
        1.0f,
        1.0f,
        1.0f,
        1.02f,
        1.1f,
        1.2f,
        1.3f,
        1.4f,
        1.5f,
        1.6f,
        1.7f,
        1.8f,
        2.0f,
        2.2f,
        2.4f,
        2.6f,
        2.8f,
        3.0f,
        3.2f,
        3.4f
    };

    static readonly List<float> Experience = new()
    {
        1.0f,
        1.0f,
        1.0f,
        1.01f,
        1.02f,
        1.03f,
        1.04f,
        1.05f,
        1.06f,
        1.07f,
        1.08f,
        1.09f,
        1.1f,
        1.12f,
        1.14f,
        1.16f,
        1.18f,
        1.2f,
        1.22f,
        1.24f
    };

    static readonly List<float> Gold = new()
    {
        1.0f,
        1.0f,
        1.0f,
        1.01f,
        1.02f,
        1.03f,
        1.04f,
        1.05f,
        1.06f,
        1.07f,
        1.08f,
        1.09f,
        1.1f,
        1.12f,
        1.14f,
        1.16f,
        1.18f,
        1.2f,
        1.22f,
        1.24f
    };

    static readonly List<float> Health = new()
    {
        1.0f,
        1.0f,
        1.0f,
        1.02f,
        1.1f,
        1.2f,
        1.3f,
        1.4f,
        1.5f,
        1.6f,
        1.7f,
        1.8f,
        1.9f,
        2.0f,
        2.1f,
        2.2f,
        2.3f,
        2.4f,
        2.5f,
        2.6f
    };

    public static void UpdateStats(NeutralMinion monster)
    {
        var level = monster.MinionLevel;
        if (level > 19)
        {
            level = 19;
        }

        //The Attack damage doesn't get updated on the Monster's HUD, i already double checked and the value is right though.
        monster.Stats.AttackDamage.MulBaseValuePerm(AttackDamage[level]);
        monster.Stats.ExpGivenOnDeath.MulBaseValuePerm(Experience[level]);
        monster.Stats.GoldGivenOnDeath.MulBaseValuePerm(Gold[level]);
        monster.Stats.HealthPoints.MulBaseValuePerm(Health[level]);
        SetFullHealth(monster);
    }
}
