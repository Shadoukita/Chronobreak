namespace CharScripts
{
    public class CharScriptAhri : CharScript
    {
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) > 0)
            {
                spellName = GetSpellName(spell);
                if (spellName == nameof(Spells.AhriOrbofDeception))
                {
                    charVars.OrbofDeceptionIsActive = 1;
                    SpellBuffRemove(owner, nameof(Buffs.AhriPassiveParticle), owner, 0);
                    AddBuff(owner, owner, new Buffs.AhriIdleCheck(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (spellName == nameof(Spells.AhriFoxFire))
                {
                    charVars.FoxFireIsActive = 1;
                }
                if (spellName == nameof(Spells.AhriSeduce))
                {
                    charVars.SeduceIsActive = 1;
                    SpellBuffRemove(owner, nameof(Buffs.AhriPassiveParticle), owner, 0);
                    AddBuff(owner, owner, new Buffs.AhriIdleCheck(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (spellName == nameof(Spells.AhriTumble))
                {
                    charVars.TumbleIsActive = 1;
                }
            }
            else
            {
                spellName = GetSpellName(spell);
                if (spellName == nameof(Spells.AhriOrbofDeception))
                {
                    SpellBuffRemove(owner, nameof(Buffs.AhriIdleParticle), owner, 0);
                    AddBuff(owner, owner, new Buffs.AhriIdleCheck(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (spellName == nameof(Spells.AhriSeduce))
                {
                    SpellBuffRemove(owner, nameof(Buffs.AhriIdleParticle), owner, 0);
                    AddBuff(owner, owner, new Buffs.AhriIdleCheck(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.AhriIdleParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.OrbofDeceptionIsActive = 0;
            charVars.FoxFireIsActive = 0;
            charVars.SeduceIsActive = 0;
            charVars.TumbleIsActive = 0;
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}