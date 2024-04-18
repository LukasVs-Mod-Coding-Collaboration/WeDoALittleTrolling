/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2024 LukasV-Coding

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items.Accessories;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Common.Utilities;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using System.Collections.Generic;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ReLogic.Content;

namespace WeDoALittleTrolling.Common.SkillTree
{
    internal class WDALTSkillTreeSystem : ModPlayer
    {
        public const int amountNodes = 2;
        public WDALTSkillNode[] nodes = new WDALTSkillNode[amountNodes];
        public const int Core = 0;
        public const int Basic = 1;
        public static UserInterface UI;
        public class UI_ST : UIState
        {
            public Mod mod;
            public UIImageButton[] nodeButtons = new UIImageButton[amountNodes];
            public UIPanel panel;

            public UI_ST()
            {
                OnInitialize();
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                base.DrawSelf(spriteBatch);
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem system))
                {
                    if (ContainsPoint(Main.MouseScreen))
                    {
                        Main.LocalPlayer.mouseInterface = true;
                    }
                    if (panel.ContainsPoint(Main.MouseScreen))
                    {
                        Main.LocalPlayer.mouseInterface = true;
                    }
                    string displayText;
                    for (int i = 0; i < nodeButtons.Length; i++)
                    {
                        switch (i)
                        {
                            case Core:
                                displayText = "Core Node.\nDoes nothing yet.";
                                break;
                            case Basic:
                                displayText = "Basic Node.\nDoes nothing yet.";
                                if (!system.IsNodeEnabled(Core))
                                {
                                    displayText += "\nLocked: Requires Core Node.";
                                }
                                break;
                            default:
                                displayText = "Core Node.\nDoes nothing yet.";
                                break;
                        }
                        if (nodeButtons[i].IsMouseHovering)
                        {
                            Main.hoverItemName = displayText;
                        }
                    }
                }
            }

            public override void OnInitialize()
            {
                mod = WeDoALittleTrolling.instance;
                panel = new UIPanel();
                panel.Width.Set(512, 0);
                panel.Height.Set(512, 0);
                panel.HAlign = 0.5f;
                panel.VAlign = 0.5f;
                Append(panel);
                UIText header = new UIText("We Do A Little Trolling Skill Tree");
                header.HAlign = 0.5f;
                header.Top.Set(16, 0);
                panel.Append(header);
                Asset<Texture2D> buttonTexture;
                float buttonHAlign;
                float buttonVAlign;
                MouseEvent call;
                for (int i = 0; i < nodeButtons.Length; i++)
                {
                    switch(i)
                    {
                        case Core:
                            buttonHAlign = 0.5f;
                            buttonVAlign = 0.5f;
                            buttonTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                            call = OnClick_Node_Core;
                            break;
                        case Basic:
                            buttonHAlign = 0.25f;
                            buttonVAlign = 0.5f;
                            buttonTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                            call = OnClick_Node_Basic;
                            break;
                        default:
                            buttonHAlign = 0.5f;
                            buttonVAlign = 0.5f;
                            buttonTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                            call = OnClick_Node_Core;
                            break;
                    }
                    nodeButtons[i] = new UIImageButton(buttonTexture);
                    nodeButtons[i].SetImage(buttonTexture);
                    nodeButtons[i].SetHoverImage(buttonTexture);
                    nodeButtons[i].HAlign = buttonHAlign;
                    nodeButtons[i].VAlign = buttonVAlign;
                    nodeButtons[i].Width.Set(62, 0);
                    nodeButtons[i].Height.Set(62, 0);
                    nodeButtons[i].OnLeftClick += call;
                    panel.Append(nodeButtons[i]);
                }
            }

            public override void OnActivate()
            {
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem system))
                {
                    for (int i = 0; i < nodeButtons.Length; i++)
                    {
                        Asset<Texture2D> buttonTextureDisabled;
                        Asset<Texture2D> buttonTextureEnabled;
                        switch (i)
                        {
                            case Core:
                                buttonTextureDisabled = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                                buttonTextureEnabled = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNode");
                                break;
                            case Basic:
                                buttonTextureDisabled = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                                buttonTextureEnabled = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNode");
                                break;
                            default:
                                buttonTextureDisabled = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                                buttonTextureEnabled = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNode");
                                break;
                        }
                        if (system.IsNodeEnabled(i))
                        {
                            nodeButtons[i].SetImage(buttonTextureEnabled);
                            nodeButtons[i].SetHoverImage(buttonTextureEnabled);
                            nodeButtons[i].Width.Set(62, 0);
                            nodeButtons[i].Height.Set(62, 0);
                        }
                        else
                        {
                            nodeButtons[i].SetImage(buttonTextureDisabled);
                            nodeButtons[i].SetHoverImage(buttonTextureDisabled);
                            nodeButtons[i].Width.Set(62, 0);
                            nodeButtons[i].Height.Set(62, 0);
                        }
                    }
                }
                base.OnActivate();
            }

            public void OnClick_Node_Core(UIMouseEvent evt, UIElement listeningElement)
            {
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem system))
                {
                    system.ToggleNode(Core);
                    this.Deactivate();
                    this.Activate();
                }
            }

            public void OnClick_Node_Basic(UIMouseEvent evt, UIElement listeningElement)
            {
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem system))
                {
                    system.ToggleNode(Basic);
                    this.Deactivate();
                    this.Activate();
                }
            }
        }

        public static UI_ST SkillUI;
        public static GameTime prevUIUpdateGameTime;

        public static void UIInit()
        {
            UI = new UserInterface();
            SkillUI = new UI_ST();
            SkillUI.Activate();
        }

        public static void UIDestroy()
        {
            SkillUI = null;
        }

        public static void UpdateUI(GameTime gameTime)
        {
            prevUIUpdateGameTime = gameTime;
            if (UI?.CurrentState != null)
            {
                UI.Update(gameTime);
            }
        }

        public static void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert
                (
                    mouseTextIndex,
                    new LegacyGameInterfaceLayer
                    (
                        "WDALT: Skill Tree",
                        delegate
                        {
                            if (prevUIUpdateGameTime != null && UI?.CurrentState != null)
                            {
                                UI.Draw(Main.spriteBatch, prevUIUpdateGameTime);
                            }
                            return true;
                        },
                        InterfaceScaleType.UI
                    )
                );
            }
        }

        public override void Initialize()
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                int[] dependencies;
                int depAmount = 0;
                switch (i)
                {
                    case Core:
                        depAmount = 0;
                        dependencies = new int[depAmount];
                        break;
                    case Basic:
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Core;
                        break;
                    default:
                        dependencies = new int[depAmount];
                        break;
                }
                nodes[i] = new WDALTSkillNode(false, i, dependencies);
            }
        }

        public bool IsNodeEnabled(int type)
        {
            if(nodes[type].enabled)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ToggleNode(int type)
        {
            if(nodes[type].enabled == false)
            {
                nodes[type].enabled = true;
                if (!IsSkillTreeValid())
                {
                    nodes[type].enabled = false;
                }
            }
            else
            {
                nodes[type].enabled = false;
                if (!IsSkillTreeValid())
                {
                    nodes[type].enabled = true;
                }
            }
        }

        public bool IsSkillTreeValid()
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null && nodes[i].enabled && nodes[i].dependencies != null)
                {
                    for (int j = 0; j < nodes[i].dependencies.Length; j++)
                    {
                        if(nodes[j] != null && !nodes[j].enabled)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public void OpenSkillTreeGUI()
        {
            if (Player.whoAmI != Main.myPlayer || Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (UI?.CurrentState != SkillUI)
            {
                UI?.SetState(SkillUI);
            }
            else
            {
                UI?.SetState(null);
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (WDALTKeybindSystem.SkillTreeKeybind.JustPressed && Player.whoAmI == Main.myPlayer && !Main.dedServ && Main.netMode != NetmodeID.Server)
            {
                OpenSkillTreeGUI();
            }
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("Core"))
            {
                (nodes[Core]).enabled = tag.GetBool("Core");
            }
            if (tag.ContainsKey("Basic"))
            {
                (nodes[Basic]).enabled = tag.GetBool("Basic");
            }
            if (!IsSkillTreeValid())
            {
                Initialize();
            }
        }

        public override void SaveData(TagCompound tag)
        {
            if (!IsSkillTreeValid())
            {
                return;
            }
            if ((nodes[Core]).enabled)
            {
                tag["Core"] = (nodes[Core]).enabled;
            }
            if ((nodes[Basic]).enabled)
            {
                tag["Basic"] = (nodes[Basic]).enabled;
            }
        }
    }
}
