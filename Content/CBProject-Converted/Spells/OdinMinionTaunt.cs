namespace Spells
{
    public class OdinMinionTaunt : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class OdinMinionTaunt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "odin_minion_tower_buf.troy", "", "", },
            BuffName = "OdinMinionTaunt",
            BuffTextureName = "Soraka_Bless.dds",
        };
        float magicResistBuff;
        float armorBuff;
        float damageTakenFromGuardian;
        float moveSpeedBuff;
        public OdinMinionTaunt(float magicResistBuff = default, float armorBuff = default)
        {
            this.magicResistBuff = magicResistBuff;
            this.armorBuff = armorBuff;
        }
        public override void OnActivate()
        {
            damageTakenFromGuardian = 0.8f;
            moveSpeedBuff = 0.25f;
            ApplyTaunt(attacker, owner, 1);
            //RequireVar(this.armorBuff);
            //RequireVar(this.magicResistBuff);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedBuff);
            IncFlatSpellBlockMod(owner, magicResistBuff);
            IncFlatArmorMod(owner, armorBuff);
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            string attackerSkinName = GetUnitSkinName(attacker); // UNUSED
            float damageMultiplier = 1;
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.OdinGuardianBuff)) > 0)
            {
                TeamId attackerTeam = GetTeamID_CS(attacker);
                if (attackerTeam != TeamId.TEAM_NEUTRAL)
                {
                    damageMultiplier = damageTakenFromGuardian;
                }
            }
            damageAmount *= damageMultiplier;
        }
    }
}