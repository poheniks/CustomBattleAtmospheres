using HarmonyLib;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;
using System.Reflection;
using TaleWorlds.Library;

namespace CustomBattleAtmospheres
{
    public class CustomBattleAtmospheresSubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            
            Harmony harmony = new Harmony("CustomBattleAtmospheres");
            MethodInfo o = typeof(BannerlordMissions).GetMethod("CreateAtmosphereInfoForMission", BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo p = typeof(PatchCustomBattleAtmosphere).GetMethod("Postfix", BindingFlags.NonPublic | BindingFlags.Static);
            harmony.Patch(o, postfix: new HarmonyMethod(p));
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("Custom Battle Atmospheres Loaded"));
        }
    }

    [HarmonyPatch()]
    class PatchCustomBattleAtmosphere
    {
        static void Postfix(ref AtmosphereInfo __result)
        {
            AtmosphereInfo atmosphere = new AtmosphereInfo();
            atmosphere.AtmosphereName = "";
            __result = atmosphere;
        }
    }
}
