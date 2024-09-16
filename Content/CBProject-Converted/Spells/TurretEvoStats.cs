namespace Buffs
{
    public class TurretEvoStats : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "H28GEvolutionTurret",
            BuffTextureName = "Jester_DeathWard.dds",
        };
        float bonusDamage;
        float bonusHealth;
        float bonusArmor;
        float lastTimeExecuted;
        public TurretEvoStats(float bonusHealth = default)
        {
            this.bonusHealth = bonusHealth;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            return owner.Team == attacker.Team || type != BuffType.STUN;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusDamage);
            //RequireVar(this.bonusHealth);
            //RequireVar(this.bonusArmor);
            SetCanMove(owner, false);
            SpellEffectCreate(out _, out _, "jackintheboxpoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, bonusHealth);
            IncFlatPhysicalDamageMod(owner, bonusDamage);
            IncFlatArmorMod(owner, bonusArmor);
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1, default, true))
                {
                    ApplyTaunt(unit, owner, 0.5f);
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                bonusDamage += 0.125f;
                bonusArmor += 0.125f;
            }
        }
    }
}