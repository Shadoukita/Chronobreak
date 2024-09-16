namespace Spells
{
    public class HowlingGaleSpell2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 110, 145, 190, 235, 280 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_Speed = 150;
            float nextBuffVars_Gravity = 45;
            int nextBuffVars_IdealDistance = 100; // UNUSED
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bool isStealthed = GetStealthed(target);
            if (owner.Team != target.Team)
            {
                Vector3 bouncePos;
                Vector3 nextBuffVars_Position;
                if (!isStealthed)
                {
                    BreakSpellShields(target);
                    ApplyDamage(owner, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                    bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
                    nextBuffVars_Position = bouncePos;
                    AddBuff(owner, target, new Buffs.Move(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Position), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
                else
                {
                    if (target is Champion)
                    {
                        BreakSpellShields(target);
                        ApplyDamage(owner, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                        bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
                        nextBuffVars_Position = bouncePos;
                        AddBuff(owner, target, new Buffs.Move(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Position), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        bool canSee = CanSeeTarget(owner, target);
                        if (canSee)
                        {
                            BreakSpellShields(target);
                            ApplyDamage(owner, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                            bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
                            nextBuffVars_Position = bouncePos;
                            AddBuff(owner, target, new Buffs.Move(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Position), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class HowlingGaleSpell2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DarkBinding_tar.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
        };
    }
}