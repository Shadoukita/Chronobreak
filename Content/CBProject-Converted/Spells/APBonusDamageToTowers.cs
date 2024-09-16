namespace Buffs
{
    public class APBonusDamageToTowers : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "APBonusDamageToTowers",
            BuffTextureName = "Minotaur_ColossalStrength.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnDisconnect()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.DisconnectTimer(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            if (
                avatarVars.MasteryJuggernaut
                && owner.Team != attacker.Team
                && type
                is BuffType.SNARE
                or BuffType.SLOW
                or BuffType.FEAR
                or BuffType.CHARM
                or BuffType.SLEEP
                or BuffType.STUN
                or BuffType.TAUNT
            ){
                float cCreduction = 0.9f;
                duration *= cCreduction;
            }
            return true;
        }
        public override void OnActivate()
        {
            string foritfyCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (foritfyCheck == nameof(Spells.SummonerFortify))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 3, true, false, false);
            }
            string foritfyCheck2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (foritfyCheck2 == nameof(Spells.SummonerFortify))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 3, true, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            float healthPERC = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (avatarVars.MasteryInitiate && healthPERC > 0.7f)
            {
                IncPercentMovementSpeedMod(owner, avatarVars.MasteryInitiateAmt);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                if (avatarVars.MasterySeigeCommander)
                {
                    foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectTurrets, default, true))
                    {
                        AddBuff(attacker, unit, new Buffs.MasterySiegeCommanderDebuff(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            if (avatarVars.MasteryScholar && target is Champion)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
                {
                    IncExp(owner, 20);
                }
                else
                {
                    IncExp(owner, 40);
                }
            }
            if (avatarVars.MasteryBounty && target is Champion)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
                {
                    float masteryBountyAmt = avatarVars.MasteryBountyAmt / 2;
                    IncGold(owner, masteryBountyAmt);
                }
                else
                {
                    IncGold(owner, avatarVars.MasteryBountyAmt);
                }
            }
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (avatarVars.MasteryScholar && target is Champion)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
                {
                    IncExp(owner, 20);
                }
                else
                {
                    IncExp(owner, 40);
                }
            }
            if (avatarVars.MasteryBounty && target is Champion)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
                {
                    float masteryBountyAmt = avatarVars.MasteryBountyAmt / 2;
                    IncGold(owner, masteryBountyAmt);
                }
                else
                {
                    IncGold(owner, avatarVars.MasteryBountyAmt);
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            float abilityPower;
            float abilityDamageToAdd;
            float bonusAttackPower;
            if (target is BaseTurret)
            {
                abilityPower = GetFlatMagicDamageMod(owner);
                abilityDamageToAdd = abilityPower / 2.5f;
                bonusAttackPower = GetFlatPhysicalDamageMod(owner);
                if (bonusAttackPower <= abilityDamageToAdd)
                {
                    damageAmount -= bonusAttackPower;
                    damageAmount += abilityDamageToAdd;
                }
                if (avatarVars.MasteryDemolitionist)
                {
                    damageAmount += avatarVars.MasteryDemolitionistAmt;
                }
            }
            else
            {
                if (target is not ObjAIBase)
                {
                    abilityPower = GetFlatMagicDamageMod(owner);
                    abilityDamageToAdd = abilityPower / 2.5f;
                    bonusAttackPower = GetFlatPhysicalDamageMod(owner);
                    if (bonusAttackPower <= abilityDamageToAdd)
                    {
                        damageAmount -= bonusAttackPower;
                        damageAmount += abilityDamageToAdd;
                    }
                    if (avatarVars.MasteryDemolitionist)
                    {
                        damageAmount += avatarVars.MasteryDemolitionistAmt;
                    }
                }
            }
            if (avatarVars.MasteryButcher && target is ObjAIBase && target is not BaseTurret && target is not Champion)
            {
                damageAmount += avatarVars.MasteryButcherAmt;
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (avatarVars.MasteryBladedArmor && attacker is ObjAIBase && attacker is not BaseTurret && attacker is not Champion)
            {
                ApplyDamage((ObjAIBase)owner, attacker, avatarVars.MasteryBladedArmorAmt, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_REACTIVE, 1, 0, 0, false, false, (ObjAIBase)owner);
            }
        }
    }
}