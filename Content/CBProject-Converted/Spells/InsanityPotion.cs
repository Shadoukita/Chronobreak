namespace Spells
{
    public class InsanityPotion : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 35, 50, 65 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_Stats = effect0[level - 1];
            AddBuff(attacker, target, new Buffs.InsanityPotion(nextBuffVars_Stats), 1, 1, 25, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellEffectCreate(out _, out _, "insanitypotion_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out _, out _, "insanitypotion_steam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottletip", default, target, default, default, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class InsanityPotion : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Insanity Potion",
            BuffTextureName = "ChemicalMan_ChemicalRage.dds",
        };
        float stats;
        float[] effect0 = { 0.9f, 0.85f, 0.8f };
        public InsanityPotion(float stats = default)
        {
            this.stats = stats;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cCreduction = effect0[level - 1];
            if (owner.Team != attacker.Team)
            {
                //float percentReduction; // UNITIALIZED
                if (type == BuffType.SNARE)
                {
                    duration *= cCreduction;
                }
                if (type == BuffType.SLOW)
                {
                    duration *= cCreduction;
                }
                if (type == BuffType.FEAR)
                {
                    duration *= cCreduction;
                }
                if (type == BuffType.CHARM)
                {
                    duration *= cCreduction;
                }
                if (type == BuffType.SLEEP)
                {
                    duration *= cCreduction;
                }
                if (type == BuffType.STUN)
                {
                    duration *= cCreduction;
                }
                if (type == BuffType.TAUNT)
                {
                    duration *= cCreduction;
                }
                if (type == BuffType.SILENCE)
                {
                    //duration *= percentReduction;
                    duration *= cCreduction; //TODO: Verify
                }
                if (type == BuffType.BLIND)
                {
                    //duration *= percentReduction;
                    duration *= cCreduction; //TODO: Verify
                }
                duration = Math.Max(0.3f, duration);
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.stats);
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            float statsPercent = stats / 100; // UNUSED
            float statsPer5 = stats / 5;
            IncFlatSpellBlockMod(owner, stats);
            IncFlatMovementSpeedMod(owner, stats);
            IncFlatArmorMod(owner, stats);
            IncFlatMagicDamageMod(owner, stats);
            IncFlatHPRegenMod(owner, statsPer5);
            IncFlatPARRegenMod(owner, statsPer5, PrimaryAbilityResourceType.MANA);
        }
    }
}