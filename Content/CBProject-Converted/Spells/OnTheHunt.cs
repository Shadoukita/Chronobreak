namespace Spells
{
    public class SivirR: OnTheHunt {}
    public class OnTheHunt : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 90f, 90f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.25f, 0.25f, 0.25f };
        float[] effect1 = { 0.3f, 0.45f, 0.6f };
        float[] effect2 = { 0.15f, 0.225f, 0.3f };
        int[] effect3 = { 15, 15, 15 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = effect1[level - 1];
            float nextBuffVars_AllyAttackSpeedMod = effect2[level - 1]; // UNUSED
            AddBuff(attacker, attacker, new Buffs.OnTheHunt(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, effect3[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class OnTheHunt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", "", },
            AutoBuffActivateEffect = new[] { "", "OntheHuntBase_buf.troy", "", "", },
            BuffName = "On The Hunt",
            BuffTextureName = "Sivir_Deadeye.dds",
        };
        float moveSpeedMod;
        float attackSpeedMod;
        float lastTimeExecuted;
        public OnTheHunt(float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.allyAttackSpeedMod);
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.OnTheHunt));
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                AddBuff(attacker, unit, new Buffs.OnTheHuntAuraBuff(), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float duration = GetBuffRemainingDuration(owner, nameof(Buffs.OnTheHunt));
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.OnTheHuntAuraBuff)) == 0)
                    {
                        AddBuff(attacker, unit, new Buffs.OnTheHuntAuraBuff(), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}