using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Stux.patch;
using UnityEngine;

namespace Stux;

[BepInPlugin(modGUID, modName, modVersion)]
public class RadMechSounds : BaseUnityPlugin
{
    private const string modGUID = "stux.RadmechSounds";
    private const string modName = "RadmechSounds";
    private const string modVersion = "0.1.0";
    public static RadMechSounds Instance { get; set; }

    public static ManualLogSource Log => Instance.Logger;
    
    public static AudioClip[] brainwashSounds;

    private readonly Harmony _harmony = new(modGUID);


    public RadMechSounds()
    {
        Instance = this;
    }

    private void Awake()
    {
        this.Logger.LogInfo("stux.RadmechSounds is loading");
        
        brainwashSounds = new AudioClip[5];
        // set all brainwashSounds to blank asset at assets/blank.wav
        for (int i = 0; i < brainwashSounds.Length; i++)
        {
            brainwashSounds[i] = AssetBundle.LoadFromFile(Info.Location.TrimEnd("RadmechSounds.dll".ToCharArray()) + "blank").LoadAsset<AudioClip>("assets/blank.wav");
        }
        if (brainwashSounds[0] == null)
        { //  todo, check if all are null
            this.Logger.LogError("Failed to load audio assets!");
        }
        else
        {
            this.Logger.LogInfo("stux.RadmechSounds is loaded");
        }
        
        Log.LogInfo($"Applying patches...");
        ApplyPluginPatch();
        Log.LogInfo($"Patches applied");
    }

    /// <summary>
    /// Applies the patch to the game.
    /// </summary>
    private void ApplyPluginPatch()
    {
        _harmony.PatchAll(typeof(RadMechBrainwashPatch));
    }
}
