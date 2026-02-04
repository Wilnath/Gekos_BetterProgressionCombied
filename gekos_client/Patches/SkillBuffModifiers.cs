using EFT;
using gekos_api.Helpers;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static EFT.SkillManager;

namespace gekos_api.Patches
{
    public static class SkillBuffMultiConfig 
    {
        public static SkillsConfig config;

        static SkillBuffMultiConfig()
        {
            config = ConfigHandler.GetSkillsConfig();
            if (config == null)
            {
                Plugin.LogSource.LogError("Could not load skills config.");
            }
            if (config.BuffMultis == null)
            {
                Plugin.LogSource.LogError("Could not load buff multis in skills config.");
            }
        }
    }


    //Base class to minimize duplication. Cannot do the patch directly because of Harmony limitations
    public abstract class SkillBuffMultiBase<T> : ModulePatch where T : class
    {
        // Each derived class calls this to get the correct target method
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(T), "method_0");
        }

        // Shared logic for adjusting the skill buff
        protected static void DoPostfix(ref T __instance)
        {
            try
            {
                var fieldInfo = AccessTools.Field(typeof(T), "SkillBuffClass");
                if (fieldInfo == null)
                {
                    Plugin.LogSource.LogWarning($"Could not find field 'SkillBuffClass' in type {typeof(T).Name}.");
                    return;
                }
                // Retrieve the field value
                SkillBuffClass buffClass = fieldInfo.GetValue(__instance) as SkillBuffClass;

                EBuffId? skillBuff = buffClass?.Id;
                if (skillBuff == null)
                {
                    Plugin.LogSource.LogWarning("Null skill buff (or no ID)!");
                    return;
                }

                if (SkillBuffMultiConfig.config.BuffMultis.TryGetValue(skillBuff.ToString(), out float multi))
                {
                    buffClass.Value *= multi;
                }
            } catch (Exception e)
            {
                Plugin.LogSource.LogError("Something went wrong when trying to apply skill buff multipliers! Double check that the config is setup correctly!");
                Plugin.LogSource.LogError(e);
            }
        }
    }

    // Actual classes
    public class SkillBuffMulti1 : SkillBuffMultiBase<SkillManager.SkillBuffClass.Class1425>
    {
        [PatchPostfix]
        public static void Postfix(ref SkillManager.SkillBuffClass.Class1425 __instance)
        {
            DoPostfix(ref __instance);
        }
    }

    public class SkillBuffMulti2 : SkillBuffMultiBase<SkillManager.SkillBuffClass.Class1426>
    {
        [PatchPostfix]
        public static void Postfix(ref SkillManager.SkillBuffClass.Class1426 __instance)
        {
            DoPostfix(ref __instance);
        }
    }

    public class SkillBuffMulti3 : SkillBuffMultiBase<SkillManager.SkillBuffClass.Class1427>
    {
        [PatchPostfix]
        public static void Postfix(ref SkillManager.SkillBuffClass.Class1427 __instance)
        {
            DoPostfix(ref __instance);
        }
    }

    public class SkillBuffMulti4 : SkillBuffMultiBase<SkillManager.SkillBuffClass.Class1428>
    {
        [PatchPostfix]
        public static void Postfix(ref SkillManager.SkillBuffClass.Class1428 __instance)
        {
            DoPostfix(ref __instance);
        }
    }
}
