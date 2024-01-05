using System.Collections;
using DG.Tweening;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace RandomLearnSkillExt
{
    [HarmonyPatch(typeof(MenuSkillLearn))]
    public class LearnSkillMenuPatch
    {
        [HarmonyPatch("On")]
        [HarmonyPrefix]
        public static void PostOn(MenuSkillLearn __instance)
        {
            LearnSkillMenuPatch.CostVal = 0;
            ((CanvasGroup)AccessTools.Field(typeof(MenuSkillLearn), "refineButton").GetValue(__instance)).GetComponentInChildren<Text>().text = "重新随机技能";
        }

        [HarmonyPatch("ClickSkillCard")]
        [HarmonyPrefix]
        public static void PostClick(MenuSkillLearn __instance)
        {
            LearnSkillMenuPatch.CostVal = 0;
        }

        [HarmonyPatch("SkillReRandom")]
        [HarmonyPostfix]
        public static void PostReRandom(MenuSkillLearn __instance)
        {
            object obj = AccessTools.Method(typeof(MenuSkillLearn), "IESkillReRandom", null, null).Invoke(__instance, new object[0]);
            __instance.StartCoroutine((IEnumerator)obj);
        }

        [HarmonyPatch("SkillRandom")]
        [HarmonyPostfix]
        public static void PostRandomSkill(MenuSkillLearn __instance)
        {
            CanvasGroup refineButton = (CanvasGroup)AccessTools.Field(typeof(MenuSkillLearn), "refineButton").GetValue(__instance);
            DOTween.To(() => refineButton.alpha, delegate (float x)
            {
                refineButton.alpha = x;
            }, 1f, 0.2f).SetDelay(0.1f);
            refineButton.blocksRaycasts = true;
            refineButton.GetComponentInChildren<Text>().text = "重新随机一次技能";
            __instance.skillRandomCount = 1;
            refineButton.gameObject.SetActive(true);
        }

        public static int CostVal;

    }
}
