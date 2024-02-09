using Terraria.ModLoader;

namespace WeDoALittleTrolling.Common.ModSystems
{
    public class WDALTKeybindSystem : ModSystem
    {
        public static ModKeybind LifeforceEngineKeybind { get; private set; }

        public override void Load()
        {
            // Registers a new keybind
            // Localize keybind by adding a Mods.{ModName}.Keybind.{KeybindName} entry to localization file. The actual text displayed to English users is in en-US.hjson
            LifeforceEngineKeybind = KeybindLoader.RegisterKeybind(Mod, "LifeforceEngine", "L");
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            LifeforceEngineKeybind = null;
        }
    }
}
