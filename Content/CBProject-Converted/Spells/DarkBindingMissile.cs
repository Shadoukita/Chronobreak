namespace Spells
{
    public class DarkBindingMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 135, 190, 245, 300 };
        float[] effect1 = { 2, 2.25f, 2.5f, 2.75f, 3 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            bool isStealthed = GetStealthed(target);
            float damageAmount = effect0[level - 1];
            if (!isStealthed)
            {
                DestroyMissile(missileNetworkID);
                ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.9f, 1, false, false);
                AddBuff(attacker, target, new Buffs.DarkBindingMissile(), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
            }
            else
            {
                if (target is Champion)
                {
                    DestroyMissile(missileNetworkID);
                    ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.9f, 1, false, false);
                    AddBuff(attacker, target, new Buffs.DarkBindingMissile(), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        DestroyMissile(missileNetworkID);
                        ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.9f, 1, false, false);
                        AddBuff(attacker, target, new Buffs.DarkBindingMissile(), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class DarkBindingMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DarkBinding_tar.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}