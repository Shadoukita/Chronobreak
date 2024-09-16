namespace Buffs
{
    public class ExplosiveCartridges : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ExplosiveCartridges",
            BuffTextureName = "Heimerdinger_Level3Turret.dds",
        };
        public override void OnSpellHit(AttackableUnit target)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                TeamId teamID = GetTeamID_CS(owner);
                ObjAIBase attacker = GetChampionBySkinName("Heimerdinger", teamID);
                AddBuff(attacker, target, new Buffs.UrAniumRoundsHit(), 50, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                SpellEffectCreate(out _, out _, "TiamatMelee_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                float dmg = GetTotalAttackDamage(owner);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 210, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (target != unit)
                    {
                        float thirdDA = 0.4f * dmg;
                        ApplyDamage(attacker, unit, thirdDA, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}