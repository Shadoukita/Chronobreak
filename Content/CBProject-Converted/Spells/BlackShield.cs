namespace Spells
{
    public class BlackShield : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 16f, 16f, 16f, 16f, 16f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 95, 160, 225, 290, 355 };
        int[] effect1 = { 5, 5, 5, 5, 5 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float abilityPower = GetFlatMagicDamageMod(owner);
            float baseHealth = effect0[level - 1];
            float abilityPowerMod = abilityPower * 0.7f;
            float shieldHealth = abilityPowerMod + baseHealth;
            float nextBuffVars_ShieldHealth = shieldHealth;
            AddBuff(attacker, target, new Buffs.BlackShield(nextBuffVars_ShieldHealth), 1, 1, effect1[level - 1], BuffAddType.RENEW_EXISTING, BuffType.SPELL_IMMUNITY, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class BlackShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Spellimmunity.troy", },
            BuffName = "Black Shield",
            BuffTextureName = "FallenAngel_BlackShield.dds",
            OnPreDamagePriority = 2,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldHealth;
        float oldArmorAmount;
        public BlackShield(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.FEAR)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.CHARM)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SILENCE)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SLEEP)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SLOW)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SNARE)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.STUN)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.TAUNT)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.BLIND)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if (type == BuffType.SUPPRESSION)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
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
            //RequireVar(this.shieldHealth);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, shieldHealth, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            if (shieldHealth > 0)
            {
                RemoveShield(owner, shieldHealth, true, false);
            }
        }
        public override void OnUpdateActions()
        {
            if (shieldHealth <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = shieldHealth;
            if (damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                if (shieldHealth >= damageAmount)
                {
                    shieldHealth -= damageAmount;
                    damageAmount = 0;
                    oldArmorAmount -= shieldHealth;
                    ReduceShield(owner, oldArmorAmount, true, false);
                }
                else
                {
                    damageAmount -= shieldHealth;
                    shieldHealth = 0;
                    ReduceShield(owner, oldArmorAmount, true, false);
                }
            }
        }
    }
}