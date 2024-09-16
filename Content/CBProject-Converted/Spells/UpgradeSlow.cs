namespace Buffs
{
    public class UpgradeSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UpgradeSlow",
            BuffTextureName = "Heimerdinger_UPGRADE.dds",
        };
        EffectEmitter frostTurrets;
        bool willPop;
        int redShift;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out frostTurrets, out _, "heimerdinger_slowAura_self.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            willPop = false;
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.H28GEvolutionTurret)) > 0)
            {
                willPop = true;
                redShift = PushCharacterData("HeimerTBlue", owner, true);
            }
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell1)) == 0 && GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell2)) == 0)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    SpellBuffClear(owner, nameof(Buffs.H28GEvolutionTurretSpell3));
                    CancelAutoAttack(owner, true);
                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(frostTurrets);
            if (willPop)
            {
                PopCharacterData(owner, redShift);
            }
            else
            {
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ExplosiveCartridges)) > 0)
                {
                    int rShift = PushCharacterData("HeimerTRed", owner, true); // UNUSED
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.UrAniumRounds)) > 0)
                    {
                        int gShift = PushCharacterData("HeimerTGreen", owner, true); // UNUSED
                    }
                    else
                    {
                        int yShift = PushCharacterData("HeimerTYellow", owner, true); // UNUSED
                    }
                }
            }
        }
    }
}