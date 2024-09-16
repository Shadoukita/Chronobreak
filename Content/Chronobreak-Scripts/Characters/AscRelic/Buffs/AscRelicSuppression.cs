namespace Buffs
{
    internal class AscRelicSuppression : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.AURA,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };

        float timer = 100.0f;

        public override void OnDeactivate(bool expired)
        {
            RestoreMana(target, target.Stats.ManaPoints.Total);
        }

        public override void OnUpdate()
        {
            timer -= Time.DeltaTime;
            if (timer <= 0)
            {
                //Exact values for both Current Mana reduction and timer are unknow, these are approximations.
                RemoveMana(target, 700.0f);
                timer = 530;
            }
        }
    }
}