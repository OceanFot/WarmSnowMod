using BepInEx;
using HarmonyLib;

namespace RandomLearnSkillExt
{
    [BepInPlugin("SkySwordKill.StrongerSwordMaster.RandomLearnSkillExt", "无限随机技能", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("SkySwordKill.StrongerSwordMaster.RandomLearnSkillExt").PatchAll();
        }
    }
}
