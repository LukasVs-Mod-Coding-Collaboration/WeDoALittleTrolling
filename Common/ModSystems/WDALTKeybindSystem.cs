using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using WeDoALittleTrolling.Common.Configs;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Common.ModSystems
{
    public class WDALTKeybindSystem : ModSystem
    {
        public static ModKeybind LifeforceEngineKeybind { get; private set; }
        public static ModKeybind DashKeybind { get; private set; }
        // public static ModKeybind SkillTreeKeybind { get; private set; }

        public static void RegisterHooks()
        {
            On_Player.DoCommonDashHandle += On_Player_DoCommonDashHandle;
        }

        public static void UnregisterHooks()
        {
            On_Player.DoCommonDashHandle -= On_Player_DoCommonDashHandle;
        }

        public static void On_Player_DoCommonDashHandle(On_Player.orig_DoCommonDashHandle orig, Player self, out int dir, out bool dashing, Player.DashStartAction dashStartAction)
        {
            if (ModContent.GetInstance<WDALTClientConfig>().DisableDoubleTapDashing && self.active && !self.dead && self.whoAmI == Main.myPlayer)
            {
                dir = 0;
                dashing = false;
            }
            else
            {
                orig.Invoke(self, out dir, out dashing, dashStartAction);
            }
            if ((Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient) && self.TryGetModPlayer<WDALTPlayerUtil>(out WDALTPlayerUtil util))
            {
                if
                (
                    self.active &&
                    !self.dead &&
                    (
                        DashKeybind != null &&
                        DashKeybind.JustPressed &&
                        self.whoAmI == Main.myPlayer
                    ) ||
                    (
                        util.isDashing
                    )
                )
                {
                    dashing = true;
                    dir = self.direction;
                    if (self.controlRight)
                    {
                        dir = 1;
                    }
                    else if (self.controlLeft)
                    {
                        dir = -1;
                    }
                    else if (self.velocity.X > 0.01f)
                    {
                        dir = 1;
                    }
                    else if (self.velocity.X < -0.01f)
                    {
                        dir = -1;
                    }
                    self.timeSinceLastDashStarted = 0;
                    dashStartAction?.Invoke(dir);
                    util.isDashing = false;
                    if (Main.netMode == NetmodeID.MultiplayerClient && self.whoAmI == Main.myPlayer && DashKeybind != null && DashKeybind.JustPressed)
                    {
                        ModPacket syncDashPacket = WeDoALittleTrolling.instance.GetPacket();
                        syncDashPacket.Write(WDALTPacketTypeID.syncDash);
                        syncDashPacket.Write((int)Main.myPlayer);
                        syncDashPacket.Send();
                    }
                }
            }
            else if ((Main.netMode == NetmodeID.Server || Main.dedServ) && self.TryGetModPlayer<WDALTPlayerUtil>(out WDALTPlayerUtil util2))
            {
                if (util2.isDashing && self.active && !self.dead)
                {
                    dashing = true;
                    dir = self.direction;
                    if (self.controlRight)
                    {
                        dir = 1;
                    }
                    else if (self.controlLeft)
                    {
                        dir = -1;
                    }
                    else if (self.velocity.X > 0.01f)
                    {
                        dir = 1;
                    }
                    else if (self.velocity.X < -0.01f)
                    {
                        dir = -1;
                    }
                    self.timeSinceLastDashStarted = 0;
                    dashStartAction?.Invoke(dir);
                    util2.isDashing = false;
                }
            }
        }

        public override void Load()
        {
            if (Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return;
            }
            // Registers a new keybind
            // Localize keybind by adding a Mods.{ModName}.Keybind.{KeybindName} entry to localization file. The actual text displayed to English users is in en-US.hjson
            LifeforceEngineKeybind = KeybindLoader.RegisterKeybind(Mod, "LifeforceEngine", "L");
            // SkillTreeKeybind = KeybindLoader.RegisterKeybind(Mod, "Open Skill Tree", "K");
            DashKeybind = KeybindLoader.RegisterKeybind(Mod, "Dash", "C");
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            if (Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return;
            }
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            LifeforceEngineKeybind = null;
            DashKeybind = null;
            // SkillTreeKeybind = null;
        }
    }
}
