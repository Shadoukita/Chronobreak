namespace Spells
{
    public class CannonBarrage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { -0.25f, -0.25f, -0.25f };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 castPosition = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_CastPosition = castPosition;
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = effect1[level - 1];
            Minion other1 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", castPosition, teamOfOwner, false, true, true, true, true, true, 0, false, true);
            AddBuff(owner, other1, new Buffs.CannonBarrage(nextBuffVars_CastPosition, nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CannonBarrage : BuffScript
    {
        Vector3 castPosition;
        EffectEmitter particle;
        EffectEmitter particle2;
        Region bubbleID;
        float moveSpeedMod;
        float attackSpeedMod;
        public CannonBarrage(Vector3 castPosition = default, float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.castPosition = castPosition;
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.castPosition);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ExpirationTimer(), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            Vector3 castPosition = this.castPosition;
            SpellEffectCreate(out particle, out particle2, "pirate_cannonBarrage_aoe_indicator_green.troy", "pirate_cannonBarrage_aoe_indicator_red.troy", teamOfOwner, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, castPosition, target, default, default, false, false, false, false, false);
            bubbleID = AddPosPerceptionBubble(teamOfOwner, 650, castPosition, 8, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            RemovePerceptionBubble(bubbleID);
        }
        public override void OnUpdateActions()
        {
            Vector3 castPosition;
            Vector3 cannonPosition;
            ObjAIBase attacker = GetBuffCasterUnit();
            Vector3 centerPosition = this.castPosition;
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (RandomChance() < 0.15f)
            {
                castPosition = GetPointByUnitFacingOffset(owner, 290, 45);
                cannonPosition = GetRandomPointInAreaPosition(castPosition, 300, 50);
            }
            else if (RandomChance() < 0.1765f)
            {
                castPosition = GetPointByUnitFacingOffset(owner, 290, 135);
                cannonPosition = GetRandomPointInAreaPosition(castPosition, 300, 50);
            }
            else if (RandomChance() < 0.2076f)
            {
                castPosition = GetPointByUnitFacingOffset(owner, 290, 225);
                cannonPosition = GetRandomPointInAreaPosition(castPosition, 300, 50);
            }
            else if (RandomChance() < 0.2443f)
            {
                castPosition = GetPointByUnitFacingOffset(owner, 290, 315);
                cannonPosition = GetRandomPointInAreaPosition(castPosition, 300, 50);
            }
            else
            {
                cannonPosition = GetRandomPointInAreaPosition(centerPosition, 480, 100);
            }
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.CannonBarrageBall));
            SpellCast((ObjAIBase)owner, default, cannonPosition, cannonPosition, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            float nextBuffVars_AttackSpeedMod = attackSpeedMod;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, centerPosition, 580, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}