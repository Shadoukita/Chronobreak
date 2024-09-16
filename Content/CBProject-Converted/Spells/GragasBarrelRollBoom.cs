namespace Spells
{
    public class GragasBarrelRollBoom : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class GragasBarrelRollBoom : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            SpellToggleSlot = 1,
        };
        int skinID;
        //object lifetime; // UNUSED
        EffectEmitter troyVar;
        //float lifeTime; // UNUSED
        float[] effect0 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        int[] effect1 = { 85, 135, 185, 235, 285 };
        int[] effect2 = { 3, 3, 3, 3, 3 };
        int[] effect3 = { 11, 10, 9, 8, 7 };
        public GragasBarrelRollBoom(int skinID = default)
        {
            this.skinID = skinID;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifetime);
            //RequireVar(this.skinID);
            //this.lifetime;
            TeamId teamofOwner = GetTeamID_CS(owner);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GragasBarrelRollToggle));
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            if (skinID == 3)
            {
                SpellEffectCreate(out troyVar, out _, "gragas_giftbarrelfoam.troy", default, teamofOwner, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
            }
            else if (skinID == 4)
            {
                SpellEffectCreate(out troyVar, out _, "gragas_barrelfoam_classy.troy", default, teamofOwner, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
            }
            else
            {
                SpellEffectCreate(out troyVar, out _, "gragas_barrelfoam.troy", default, teamofOwner, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamofOwner = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_ASDebuff = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, attacker.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 1, false, false, (ObjAIBase)owner);
                AddBuff(attacker, unit, new Buffs.GragasExplosiveCaskDebuff(nextBuffVars_ASDebuff), 1, 1, effect2[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            int gragasSkinID = GetSkinID(owner);
            if (gragasSkinID == 4)
            {
                SpellEffectCreate(out _, out _, "gragas_barrelboom_classy.troy", default, teamofOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, attacker.Position3D, owner, default, default, true, default, default, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "gragas_barrelboom.troy", default, teamofOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, attacker.Position3D, owner, default, default, true, default, default, false);
            }
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GragasBarrelRoll));
            float cooldownVar = effect3[level - 1];
            float cDMod = GetPercentCooldownMod(owner);
            float cDModTrue = cDMod + 1;
            float barrelCD = cooldownVar * cDModTrue;
            float cDMinusBarrel = barrelCD - lifeTime;
            SetSlotSpellCooldownTimeVer2(cDMinusBarrel, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(troyVar);
            SpellBuffRemove(owner, nameof(Buffs.GragasBarrelRoll), (ObjAIBase)owner);
            ApplyDamage(attacker, attacker, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
        }
        /*
        public override void OnUpdateStats()
        {
            this.lifeTime = lifeTime;
        }
        */
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.GragasBarrelRollToggle))
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}