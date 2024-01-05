using System;
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

        /**
         * 是否开启蓝魂消耗
         */
        private static bool costOn = false;

        /**
         * 需要花费的蓝魂
         */
        private static int costVal = 0;

        /**
         * 增加键盘快捷键的支持（原本只支持手柄）
         */
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void PostUpdate(MenuSkillLearn __instance)
        {
            // 开启/关闭蓝魂消耗 -- 快捷键P
            if (Input.GetKeyDown(KeyCode.P))
            {
                costOn = !costOn;
            }

            // 以下均是原版的触发代码，仅加了键盘的按键支持
            if (__instance.skillRandomCount > 0 && (Input.GetKeyDown(KeyCode.E) || Singleton<GamepadBtn>.Instance.GetUpLikeYKeyDown()))
            {
                __instance.SkillReRandom();
                return;
            }
            if (Input.GetKeyDown(KeyCode.F) || Singleton<GamepadBtn>.Instance.GetLeftLikeXKeyDown())
            {
                __instance.AbandonSkill();
                return;
            }
            if (__instance.isNightmareBook && (Input.GetKeyDown(KeyCode.Q) || Singleton<GamepadBtn>.Instance.GetGamepadUIReturn()))
            {
                __instance.SubNightmareLevel();
                return;
            }
        }

        /**
         * 初始化
         */
        [HarmonyPatch("On")]
        [HarmonyPrefix]
        public static void PostOn(MenuSkillLearn __instance)
        {
            costVal = 0;
            ((CanvasGroup)AccessTools.Field(typeof(MenuSkillLearn), "refineButton").GetValue(__instance)).GetComponentInChildren<Text>().text = "随机技能-无消耗";
        }

        /**
         * 选择技能后消耗归0
         */
        [HarmonyPatch("ClickSkillCard")]
        [HarmonyPrefix]
        public static void PostClick(MenuSkillLearn __instance)
        {
            costVal = 0;
        }

        /**
         * 执行重新随机方法
         */
        [HarmonyPatch("SkillReRandom")]
        [HarmonyPostfix]
        public static void PostReRandom(MenuSkillLearn __instance)
        {
            // 未开启消耗 或 开启了且有足够的蓝魂
            if (!costOn || PlayerAnimControl.instance.Souls >= costVal)
            {
                if (costOn)
                {
                    PlayerAnimControl.instance.Souls -= costVal;
                }
                object obj = AccessTools.Method(typeof(MenuSkillLearn), "IESkillReRandom", null, null).Invoke(__instance, new object[0]);
                __instance.StartCoroutine((IEnumerator)obj);
            }

        }

        /**
         * 随机按钮的展示
         */
        [HarmonyPatch("SkillRandom")]
        [HarmonyPostfix]
        public static void PostRandomSkill(MenuSkillLearn __instance)
        {
            costVal += costOn ? 100 : 0;
            // 未开启消耗 或 开启了且有足够的蓝魂
            if (!costOn || PlayerAnimControl.instance.Souls >= costVal)
            {
                CanvasGroup refineButton = (CanvasGroup)AccessTools.Field(typeof(MenuSkillLearn), "refineButton").GetValue(__instance);
                DOTween.To(() => refineButton.alpha, delegate (float x)
                {
                    refineButton.alpha = x;
                }, 1f, 0.2f).SetDelay(0.1f);
                refineButton.blocksRaycasts = true;
                refineButton.GetComponentInChildren<Text>().text = string.Format("花费{0}蓝魂，重新随机一次技能", costVal);
                __instance.skillRandomCount = 1;
                refineButton.gameObject.SetActive(true);
            }
        }

    }
}
