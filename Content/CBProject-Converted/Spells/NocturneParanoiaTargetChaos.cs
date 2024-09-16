namespace Spells
{
    public class NocturneParanoiaTargetChaos : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class NocturneParanoiaTargetChaos : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "NocturneParanoiaTarget",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        bool delay;
        EffectEmitter loop;
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            delay = false;
            if (teamOfOwner == TeamId.TEAM_CHAOS)
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.NocturneParanoiaTargetOrder));
                if (count > 0)
                {
                    delay = true;
                }
                else
                {
                    SpellEffectCreate(out loop, out _, "NocturneParanoiaFriend.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, "root", default, owner, default, default, false);
                }
            }
            else
            {
                SpellBuffClear(owner, nameof(Buffs.NocturneParanoiaTargetOrder));
                SpellEffectCreate(out loop, out _, "NocturneParanoiaFoe.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, "root", default, owner, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (!delay)
            {
                SpellEffectRemove(loop);
            }
        }
        public override void OnUpdateActions()
        {
            if (delay)
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.NocturneParanoiaTargetOrder));
                if (count == 0)
                {
                    SpellEffectCreate(out loop, out _, "NocturneParanoiaFriend.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, "root", default, owner, default, default, false);
                    delay = false;
                }
            }
        }
    }
}