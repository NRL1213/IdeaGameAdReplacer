using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine.Rendering.VirtualTexturing;

namespace Ad_d_Text;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        var Harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        Harmony.PatchAll();
    }
}

[HarmonyPatch(typeof(PopupAds), "Start")]
public static class TextPatch
{
    static bool Prefix(ref string[] ___randomStrings)
    {
        string[] Config;
        string[] Text;
        string ConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + "Config.txt";
        string CustomTextPath = System.AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + "CustomText.txt";
        char Seperator = '#';

        Config = System.IO.File.ReadAllLines(ConfigPath);

        bool Append = false;
        foreach (var Line in Config)
        {
            if (Line.Contains("Append:"))
            {
                if (Line.Contains("True"))
                {
                    Append = true;
                }
                else
                {
                    Append = false;
                }
            }
            else if (Line.Contains("LineEnding:"))
            {
                var Split = Line.Split(' ');
                Seperator = Line[Line.Length - 1];
            }
        }

        var temp = System.IO.File.ReadAllText(CustomTextPath);
        Text = temp.Split(Seperator);

        if (Append)
        {
            var Size = ___randomStrings.Length + Text.Length;
            var NewText = new string[Size];
            ___randomStrings.CopyTo(NewText, 0);
            Text.CopyTo(NewText, ___randomStrings.Length);
            ___randomStrings = NewText;
        }
        else
        {
            var Size = Text.Length;
            var NewText = new string[Size];
            Text.CopyTo(NewText, 0);
            ___randomStrings = NewText;
        }

        return true;
    }
}

[HarmonyPatch(typeof(MainMenu), "Start")]
public static class BuildConfig
{
    static bool Prefix()
    {
        string ConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + "Config.txt";
        string CustomTextPath = System.AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + "CustomText.txt";

        if (!System.IO.File.Exists(ConfigPath))
        {
            var Create = System.IO.File.Create(ConfigPath);
            Create.Close();
            System.IO.File.WriteAllText(ConfigPath, "#If true adds your text to originals\nAppend: True\n#Line Seperator\nLineEnding: #");
            System.IO.File.ReadAllLines(ConfigPath);
        }

        if (!System.IO.File.Exists(CustomTextPath))
        {
            var Create = System.IO.File.Create(CustomTextPath);
            Create.Close();
            System.IO.File.ReadAllLines(CustomTextPath);
        }

        return true;
    }
}
