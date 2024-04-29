using HarmonyLib;

namespace Stux.patch;

[HarmonyPatch(typeof(RadMechAI))]
public class RadMechBrainwashPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void Postfix(ref RadMechAI __instance)
    {
        __instance.enemyType.audioClips = RadMechSounds.brainwashSounds;
    }
    
}
