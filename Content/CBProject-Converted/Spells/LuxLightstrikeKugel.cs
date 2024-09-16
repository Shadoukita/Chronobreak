namespace Spells
{
    public class LuxLightStrikeKugel : SpellScript
    {
        int[] effect0 = { 9, 9, 9, 9, 9 };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_LSCooldown = effect0[level - 1];
            Vector3 nextBuffVars_Position = missileEndPosition;
            AddBuff(attacker, attacker, new Buffs.LuxLightStrikeKugel(nextBuffVars_Position, nextBuffVars_LSCooldown), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetSlotSpellCooldownTimeVer2(0, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
        }
    }
}
namespace Buffs
{
    public class LuxLightStrikeKugel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "LuxLightstrike_mis.troy", },
        };
        Vector3 position;
        float lSCooldown;
        EffectEmitter particle;
        EffectEmitter particle1;
        EffectEmitter particle2;
        Region bubbleID;
        EffectEmitter partExplode; // UNUSED
        int[] effect0 = { 60, 105, 150, 195, 240 };
        float[] effect1 = { -0.2f, -0.24f, -0.28f, -0.32f, -0.36f };
        public LuxLightStrikeKugel(Vector3 position = default, float lSCooldown = default)
        {
            this.position = position;
            this.lSCooldown = lSCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.position);
            //RequireVar(this.level);
            //RequireVar(this.lSCooldown);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LuxLightstrikeToggle));
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 position = this.position;
            SpellEffectCreate(out particle, out _, "LuxLightstrike_mis.troy", default, teamOfOwner, 400, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, position, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle1, out particle2, "LuxLightstrike_tar_green.troy", "LuxLightstrike_tar_red.troy", teamOfOwner, 400, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "top", position, target, default, default, false, false, false, false, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
            bubbleID = AddPosPerceptionBubble(teamOfOwner, 650, position, 6, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float boomDamage = effect0[level - 1];
            Vector3 position = this.position;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, boomDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "globalhit_mana.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, false, false, false, false);
                if (unit is not BaseTurret)
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
            SpellEffectRemove(particle);
            SpellEffectRemove(particle1);
            SpellEffectRemove(particle2);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LuxLightStrikeKugel));
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * lSCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            TeamId casterID = GetTeamID_CS(owner);
            SpellEffectCreate(out partExplode, out _, "LuxBlitz_nova.troy", default, casterID, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, position, owner, default, default, true, false, false, false, false);
            RemovePerceptionBubble(bubbleID);
        }
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MoveSpeedMod = effect1[level - 1];
            Vector3 position = this.position;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}