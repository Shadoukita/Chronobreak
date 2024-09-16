namespace Buffs
{
    public class AhriOrbDamageSilence : BuffScript
    {
        int orbofDeceptionIsActive;
        int[] effect0 = { 40, 65, 90, 115, 140 };
        public AhriOrbDamageSilence(int orbofDeceptionIsActive = default)
        {
            this.orbofDeceptionIsActive = orbofDeceptionIsActive;
        }
        public override void OnActivate()
        {
            Vector3 targetPos; // UNITIALIZED
            targetPos = default; //TODO: Verify
            //RequireVar(this.orbofDeceptionIsActive);
            TeamId teamID = GetTeamID_CS(attacker);
            if (orbofDeceptionIsActive == 1)
            {
                SpellEffectCreate(out _, out _, "Ahri_PassiveHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, false, false, false, false);
                float nextBuffVars_DrainPercent = 0.1166f;
                bool nextBuffVars_DrainedBool = false;
                AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out _, out _, "Ahri_Orb_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, false, false, false, false);
                SpellEffectCreate(out _, out _, "Ahri_passive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, false, false, false, false);
                int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(attacker, owner, effect0[level - 1], DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.325f, 0, false, false, attacker);
                SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.AhriSoulCrusher), 1);
            }
            else
            {
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.AhriSoulCrusher5(), 4, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    int count = GetBuffCountFromAll(attacker, nameof(Buffs.AhriSoulCrusher5));
                    if (count <= 3)
                    {
                        AddBuff(attacker, attacker, new Buffs.AhriSoulCrusherCounter(), 9, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                    }
                }
                SpellEffectCreate(out _, out _, "Ahri_Orb_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, false, false, false, false);
                int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(attacker, owner, effect0[level - 1], DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.325f, 0, false, false, attacker);
            }
        }
    }
}