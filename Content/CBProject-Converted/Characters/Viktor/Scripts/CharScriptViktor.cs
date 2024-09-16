namespace CharScripts
{
    public class CharScriptViktor : CharScript
    {
        EffectEmitter staffIdle;
        EffectEmitter staffIdle2;
        EffectEmitter staffIdleYELLOW; // UNUSED
        EffectEmitter staffIdleBLUE; // UNUSED
        EffectEmitter staffIdleRED; // UNUSED
        public override void OnUpdateActions()
        {
            if (!charVars.HasRemoved)
            {
                TeamId ownerTeam = GetTeamID_CS(owner);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ViktorAugmentQ)) > 0)
                {
                    SpellEffectRemove(staffIdle);
                    SpellEffectRemove(staffIdle2);
                    charVars.HasRemoved = true;
                    SpellEffectCreate(out staffIdleYELLOW, out _, "Viktorb_yellow.troy", "Viktorb_yellow.troy", ownerTeam, 0, 0, TeamId.TEAM_UNKNOWN, ownerTeam, default, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ViktorAugmentW)) > 0)
                    {
                        SpellEffectRemove(staffIdle);
                        SpellEffectRemove(staffIdle2);
                        charVars.HasRemoved = true;
                        SpellEffectCreate(out staffIdleBLUE, out _, "Viktorb_blue.troy", "Viktorb_blue.troy", ownerTeam, 0, 0, TeamId.TEAM_UNKNOWN, ownerTeam, default, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
                    }
                    else
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ViktorAugmentE)) > 0)
                        {
                            SpellEffectRemove(staffIdle);
                            SpellEffectRemove(staffIdle2);
                            charVars.HasRemoved = true;
                            SpellEffectCreate(out staffIdleRED, out _, "Viktorb_red.troy", "Viktorb_red.troy", ownerTeam, 0, 0, TeamId.TEAM_UNKNOWN, ownerTeam, default, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ViktorPassiveAPPerLev(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            CreateItem(owner, 3200);
            TeamId ownerTeam = GetTeamID_CS(owner);
            SpellEffectCreate(out staffIdle, out staffIdle2, "Viktor_idle.troy", "Viktor_idle.troy", ownerTeam, 0, 0, TeamId.TEAM_NEUTRAL, ownerTeam, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
        }
        public override void OnResurrect()
        {
            AddBuff(owner, owner, new Buffs.ViktorPassiveAPPerLev(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}