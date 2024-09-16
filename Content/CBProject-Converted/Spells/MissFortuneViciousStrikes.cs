namespace Spells
{
    public class MissFortuneViciousStrikes : SpellScript
    {
        float[] effect0 = { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_ASMod = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.MissFortuneViciousStrikes(nextBuffVars_ASMod), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MissFortuneViciousStrikes : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "MissFortuneViciousStrikes",
            BuffTextureName = "MissFortune_ImpureShots.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float aSMod;
        EffectEmitter ar; // UNUSED
        public MissFortuneViciousStrikes(float aSMod = default)
        {
            this.aSMod = aSMod;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            //RequireVar(this.aSMod);
            SpellEffectCreate(out ar, out _, "missFortune_viciousShots_attack_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_HAND_LOC", default, owner, default, default, true, default, default, false);
            SpellEffectCreate(out ar, out _, "missFortune_viciousShots_attack_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_HAND_LOC", default, owner, default, default, true, default, default, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, aSMod);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && attacker.Team != target.Team)
            {
                int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
                AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}