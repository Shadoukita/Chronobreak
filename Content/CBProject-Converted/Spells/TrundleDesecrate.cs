namespace Spells
{
    public class TrundleDesecrate : SpellScript
    {
        public override void SelfExecute()
        {
            Vector3 ownerPos = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);
            Minion other1 = SpawnMinion("birds", "TestCube", "idle.lua", ownerPos, teamID, true, true, true, true, true, true, 0, false, true);
            AddBuff(attacker, other1, new Buffs.TrundleDesecrate(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell2", 1, owner, false, true, true);
        }
    }
}
namespace Buffs
{
    public class TrundleDesecrate : BuffScript
    {
        EffectEmitter particle2;
        EffectEmitter particle;
        float[] effect0 = { 0.2f, 0.3f, 0.4f, 0.5f, 0.6f };
        float[] effect1 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        float[] effect2 = { 0.8f, 0.75f, 0.7f, 0.65f, 0.6f };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle2, out particle, "trundledesecrate_green.troy", "trundledesecrate_red.troy", teamID, 10, 0, TeamId.TEAM_ORDER, default, default, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnUpdateStats()
        {
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.AlwaysSelf, nameof(Buffs.TrundleDiseaseOverseer), true))
            {
                int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_SelfASMod = effect0[level - 1];
                float nextBuffVars_SelfMSMod = effect1[level - 1];
                float nextBuffVars_CCReduc = effect2[level - 1];
                AddBuff((ObjAIBase)unit, unit, new Buffs.TrundleDesecrateBuffs(nextBuffVars_SelfASMod, nextBuffVars_SelfMSMod, nextBuffVars_CCReduc), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
        }
    }
}