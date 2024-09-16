namespace Buffs
{
    public class SejuaniRunSpeed : BuffScript
    {
        int runAnim;
        public override void OnActivate()
        {
            runAnim = 1;
        }
        public override void OnUpdateActions()
        {
            float mS = GetMovementSpeed(owner);
            if (mS >= 405)
            {
                if (runAnim != 3)
                {
                    runAnim = 3;
                    OverrideAnimation("Run", "Run3", owner);
                }
            }
            else if (mS >= 355)
            {
                if (runAnim != 2)
                {
                    runAnim = 2;
                    OverrideAnimation("Run", "Run2", owner);
                }
            }
            else
            {
                if (runAnim != 1)
                {
                    runAnim = 1;
                    ClearOverrideAnimation("Run", owner);
                }
            }
        }
    }
}