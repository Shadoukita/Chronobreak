namespace Spells
{
    public class PhosphorusBomb : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            AddBuff(attacker, attacker, new Buffs.PhosphorusBomb(nextBuffVars_TargetPos), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class PhosphorusBomb : BuffScript
    {
        Vector3 targetPos;
        EffectEmitter particle;
        Region bubbleID;
        int[] effect0 = { 80, 130, 180, 230, 280 };
        public PhosphorusBomb(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.targetPos);
            TeamId casterID = GetTeamID_CS(attacker);
            Vector3 targetPos = this.targetPos;
            SpellEffectCreate(out particle, out _, "corki_phosphorous_bomb_tar.troy", default, casterID, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            bubbleID = AddPosPerceptionBubble(casterID, 375, targetPos, 6, default, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, this.targetPos, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.PhosphorusBombBlind(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, this.targetPos, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            SpellEffectRemove(particle);
        }
    }
}