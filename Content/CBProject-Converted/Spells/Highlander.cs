namespace Spells
{
    public class Highlander : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 75f, 75f, 75f, 18f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.4f, 0.6f, 0.8f };
        int[] effect1 = { 6, 9, 12 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            float nextBuffVars_MoveSpeedMod = 0.4f;
            float nextBuffVars_AttackSpeedMod = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.Highlander(nextBuffVars_AttackSpeedMod, nextBuffVars_MoveSpeedMod), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class Highlander : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Highlander_buf.troy", },
            BuffTextureName = "MasterYi_InnerFocus2.dds",
        };
        float attackSpeedMod;
        float moveSpeedMod;
        int[] effect0 = { 9, 8, 7, 6, 5 };
        public Highlander(float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.SNARE)
                {
                    Say(owner, "game_lua_Highlander");
                    returnValue = false;
                }
                if (type == BuffType.SLOW)
                {
                    Say(owner, "game_lua_Highlander");
                    returnValue = false;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SpellEffectCreate(out _, out _, "DeathsCaress_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            }
        }
        /*
        //TODO: Uncomment and fix
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            object level; // UNITIALIZED
            if(target is Champion)
            {
                float alphaStrikeCD = this.effect0[level - 1];
                float wujuStyleCD = 12.5f;
                float highlanderCD = 37.5f;
                float meditateCD = 22.5f;
                float aSCDLeft = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float medCDLeft = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float wujuCDLeft = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float highCDLeft = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float aSCDFinal = aSCDLeft - alphaStrikeCD;
                float medCDFinal = medCDLeft - meditateCD;
                float wujuCDFinal = wujuCDLeft - wujuStyleCD;
                float highCDFinal = highCDLeft - highlanderCD;
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, aSCDFinal);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, medCDFinal);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, wujuCDFinal);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, highCDFinal);
                SpellEffectCreate(out _, out _, "DeathsCaress_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            }
        }
        */
    }
}