namespace Spells
{
    public class SummonerFlash : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            if (!canMove)
            {
                returnValue = false;
            }
            if (!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void UpdateTooltip(int spellSlot)
        {
            float baseCooldown;
            if (avatarVars.UtilityMastery == 1)
            {
                baseCooldown = 250;
            }
            else
            {
                baseCooldown = 265;
            }
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
        public override float AdjustCooldown()
        {
            float baseCooldown;
            if (avatarVars.UtilityMastery == 1)
            {
                baseCooldown = 250;
            }
            else
            {
                baseCooldown = 265;
            }
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            return baseCooldown;
        }
        public override void SelfExecute()
        {
            Vector3 castPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, castPos);
            FaceDirection(owner, castPos);
            if (distance > 400)
            {
                castPos = GetPointByUnitFacingOffset(owner, 400, 0);
            }
            StopChanneling((ObjAIBase)target, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
            SpellEffectCreate(out _, out _, "summoner_flashback.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "summoner_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FlashBeenHit)) > 0)
            {
                Vector3 nextBuffVars_CastPos = castPos;
                AddBuff(owner, owner, new Buffs.SummonerFlash(nextBuffVars_CastPos), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                TeleportToPosition(owner, castPos);
            }
        }
    }
}
namespace Buffs
{
    public class SummonerFlash : BuffScript
    {
        Vector3 castPos;
        public SummonerFlash(Vector3 castPos = default)
        {
            this.castPos = castPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.castPos);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 castPos = this.castPos;
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            TeleportToPosition(owner, castPos);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}