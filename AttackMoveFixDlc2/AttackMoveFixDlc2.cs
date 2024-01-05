using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace 攻击手感调整
{
    [BepInPlugin("byAILOMANGACC_AttackMoveFix_DLC2", "攻击手感调整_DLC2", "1.0.0")]
    public class AttackMoveFix : BaseUnityPlugin
    {
        public void Start()
        {
            Harmony.CreateAndPatchAll(typeof(AttackMoveFix), null);
        }

        public static float move1()
        {
            bool flag = Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f || Hinput.anyGamepad.leftStick;
            float result;
            if (flag)
            {
                result = 0.15f;
            }
            else
            {
                result = 0f;
            }
            return result;
        }

        public static float move2()
        {
            bool flag = Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f || Hinput.anyGamepad.leftStick;
            float result;
            if (flag)
            {
                result = 1.5f;
            }
            else
            {
                result = 0f;
            }
            return result;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(PlayerAnimControl), "Update")]
        public static IEnumerable<CodeInstruction> Updatefix(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher codeMatcher = new CodeMatcher(instructions, null);
            CodeMatcher codeMatcher2 = codeMatcher;
            bool flag = true;
            CodeMatch[] array = new CodeMatch[4];
            array[0] = new CodeMatch(new OpCode?(OpCodes.Ldfld), null, null);
            array[1] = new CodeMatch(new OpCode?(OpCodes.Callvirt), null, null);
            array[2] = new CodeMatch(new OpCode?(OpCodes.Call), null, null);
            array[3] = new CodeMatch((CodeInstruction i) => i.opcode == OpCodes.Ldc_R4 && CodeInstructionExtensions.OperandIs(i, 0.15f), null);
            codeMatcher2.MatchForward(flag, array);
            codeMatcher.SetInstructionAndAdvance(new CodeInstruction(OpCodes.Callvirt, typeof(AttackMoveFix).GetMethod("move1")));
            CodeMatcher codeMatcher3 = codeMatcher;
            bool flag2 = true;
            CodeMatch[] array2 = new CodeMatch[4];
            array2[0] = new CodeMatch(new OpCode?(OpCodes.Ldfld), null, null);
            array2[1] = new CodeMatch(new OpCode?(OpCodes.Callvirt), null, null);
            array2[2] = new CodeMatch(new OpCode?(OpCodes.Call), null, null);
            array2[3] = new CodeMatch((CodeInstruction i) => i.opcode == OpCodes.Ldc_R4 && CodeInstructionExtensions.OperandIs(i, 0.15f), null);
            codeMatcher3.MatchForward(flag2, array2);
            codeMatcher.SetInstructionAndAdvance(new CodeInstruction(OpCodes.Callvirt, typeof(AttackMoveFix).GetMethod("move1")));
            CodeMatcher codeMatcher4 = codeMatcher;
            bool flag3 = true;
            CodeMatch[] array3 = new CodeMatch[4];
            array3[0] = new CodeMatch(new OpCode?(OpCodes.Ldfld), null, null);
            array3[1] = new CodeMatch(new OpCode?(OpCodes.Callvirt), null, null);
            array3[2] = new CodeMatch(new OpCode?(OpCodes.Call), null, null);
            array3[3] = new CodeMatch((CodeInstruction i) => i.opcode == OpCodes.Ldc_R4 && CodeInstructionExtensions.OperandIs(i, 0.15f), null);
            codeMatcher4.MatchForward(flag3, array3);
            codeMatcher.SetInstructionAndAdvance(new CodeInstruction(OpCodes.Callvirt, typeof(AttackMoveFix).GetMethod("move1")));
            CodeMatcher codeMatcher5 = codeMatcher;
            bool flag4 = true;
            CodeMatch[] array4 = new CodeMatch[4];
            array4[0] = new CodeMatch(new OpCode?(OpCodes.Ldfld), null, null);
            array4[1] = new CodeMatch(new OpCode?(OpCodes.Callvirt), null, null);
            array4[2] = new CodeMatch(new OpCode?(OpCodes.Call), null, null);
            array4[3] = new CodeMatch((CodeInstruction i) => i.opcode == OpCodes.Ldc_R4 && CodeInstructionExtensions.OperandIs(i, 1.5f), null);
            codeMatcher5.MatchForward(flag4, array4);
            codeMatcher.SetInstructionAndAdvance(new CodeInstruction(OpCodes.Callvirt, typeof(AttackMoveFix).GetMethod("move2")));
            return codeMatcher.Instructions();
        }
    }
}
