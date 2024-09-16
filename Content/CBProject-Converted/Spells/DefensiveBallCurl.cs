namespace Spells
{
    public class DefensiveBallCurl : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 50, 75, 100, 125, 150 };
        int[] effect1 = { 15, 25, 35, 45, 55 };
        public override void SelfExecute()
        {
            float nextBuffVars_ArmorAmount = effect0[level - 1];
            float nextBuffVars_DamageReturn = effect1[level - 1];
            AddBuff(attacker, owner, new Buffs.DefensiveBallCurl(nextBuffVars_ArmorAmount, nextBuffVars_DamageReturn), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class DefensiveBallCurl : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "DefensiveBallCurl",
            BuffTextureName = "Armordillo_ShellBash.dds",
        };
        float armorAmount;
        float damageReturn;
        int casterID;
        EffectEmitter particle;
        public DefensiveBallCurl(float armorAmount = default, float damageReturn = default)
        {
            this.armorAmount = armorAmount;
            this.damageReturn = damageReturn;
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PowerBall)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.PowerBall), (ObjAIBase)owner, 0);
            }
            //RequireVar(this.armorAmount);
            //RequireVar(this.damageReturn);
            casterID = PushCharacterData("RammusDBC", owner, false);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DefensiveBallCurlCancel));
            SpellEffectCreate(out particle, out _, "DefensiveBallCurl_buf", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SetSlotSpellCooldownTimeVer2(1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            PopCharacterData(owner, casterID);
            SpellEffectCreate(out _, out _, "DBC_out.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            float baseCD = 14;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * baseCD;
            float finalCooldown = newCooldown - lifeTime;
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DefensiveBallCurl));
            SetSlotSpellCooldownTimeVer2(finalCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(particle);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorAmount);
            IncFlatSpellBlockMod(owner, armorAmount);
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (attacker is not BaseTurret)
            {
                float baseArmor = GetArmor(owner);
                float armorMod = baseArmor * 0.1f;
                float damageReturn = armorMod + this.damageReturn;
                ApplyDamage((ObjAIBase)owner, attacker, damageReturn, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
                SpellEffectCreate(out _, out _, "Thornmail_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, false, false, false);
            }
        }
    }
}