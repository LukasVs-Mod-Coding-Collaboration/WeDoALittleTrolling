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
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.ModSystems;
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
        public const int amountNodes = 3;
        public static WDALTSkillNode[] nodes = new WDALTSkillNode[amountNodes];
        public static bool nodesInitialized = false;
        public const int Core = 0;
        public const int Positive1 = 1;
        public const int Negative1 = 2;
        public static UserInterface UI;
        public class UI_ST : UIState
        {
            public UIImage[] nodeButtons = new UIImage[amountNodes];
            public UIPanel panel;

            public UI_ST()
            {
                OnInitialize();
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                base.DrawSelf(spriteBatch);
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
                        case Positive1:
                            displayText = "Positive Node.\nDoes nothing yet.";
                            break;
                        case Negative1:
                            displayText = "Negative Node.\nDoes nothing yet.";
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

            public override void OnInitialize()
            {
                if (!nodesInitialized)
                {
                    InitNodes();
                }
                panel = new UIPanel();
                panel.Width.Set(700, 0);
                panel.Height.Set(700, 0);
                panel.HAlign = 0.5f;
                panel.VAlign = 0.5f;
                Append(panel);
                UIText header = new UIText("We Do A Little Trolling Skill Tree");
                header.HAlign = 0.5f;
                header.Top.Set(16, 0);
                panel.Append(header);
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
                            call = OnClick_Node_Core;
                            break;
                        case Positive1:
                            buttonHAlign = 0.25f;
                            buttonVAlign = 0.5f;
                            call = OnClick_Node_Positive1;
                            break;
                        case Negative1:
                            buttonHAlign = 0.75f;
                            buttonVAlign = 0.5f;
                            call = OnClick_Node_Negative1;
                            break;
                        default:
                            buttonHAlign = 0.5f;
                            buttonVAlign = 0.5f;
                            call = OnClick_Node_Core;
                            break;
                    }
                    nodeButtons[i] = new UIImage(nodes[i].disabledTexture);
                    nodeButtons[i].SetImage(nodes[i].disabledTexture);
                    nodeButtons[i].Width.Set(nodes[i].textureWidth, 0f);
                    nodeButtons[i].Height.Set(nodes[i].textureHeight, 0f);
                    nodeButtons[i].HAlign = buttonHAlign;
                    nodeButtons[i].VAlign = buttonVAlign;
                    nodeButtons[i].OnLeftClick += call;
                    panel.Append(nodeButtons[i]);
                }
            }

            public override void OnActivate()
            {
                for (int i = 0; i < nodeButtons.Length; i++)
                {
                    if (IsNodeEnabled(i))
                    {
                        nodeButtons[i].SetImage(nodes[i].enabledTexture);
                        nodeButtons[i].Width.Set(nodes[i].textureWidth, 0f);
                        nodeButtons[i].Height.Set(nodes[i].textureHeight, 0f);
                    }
                    else
                    {
                        nodeButtons[i].SetImage(nodes[i].disabledTexture);
                        nodeButtons[i].Width.Set(nodes[i].textureWidth, 0f);
                        nodeButtons[i].Height.Set(nodes[i].textureHeight, 0f);
                    }
                }
                base.OnActivate();
            }

            public void OnClick_Node_Core(UIMouseEvent evt, UIElement listeningElement)
            {
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem tree))
                {
                    tree.ToggleNode(Core);
                    this.Deactivate();
                    this.Activate();
                }
            }

            public void OnClick_Node_Positive1(UIMouseEvent evt, UIElement listeningElement)
            {
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem tree))
                {
                    tree.ToggleNode(Positive1);
                    this.Deactivate();
                    this.Activate();
                }
            }

            public void OnClick_Node_Negative1(UIMouseEvent evt, UIElement listeningElement)
            {
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem tree))
                {
                    tree.ToggleNode(Negative1);
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

        public static void InitNodes(bool reset = false)
        {
            if (nodesInitialized)
            {
                if (reset)
                {
                    nodes = null;
                    nodes = new WDALTSkillNode[amountNodes];
                }
                else
                {
                    return;
                }
            }
            Mod mod = WeDoALittleTrolling.instance;
            for (int i = 0; i < nodes.Length; i++)
            {
                int[] dependencies;
                int depAmount;
                Asset<Texture2D> enabledTexture;
                Asset<Texture2D> disabledTexture;
                int textureWidth;
                int textureHeight;
                switch (i)
                {
                    case Core:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                        textureWidth = 62;
                        textureHeight = 62;
                        depAmount = 0;
                        dependencies = new int[depAmount];
                        break;
                    case Positive1:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/PositiveNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/PositiveNodeInactive");
                        textureWidth = 34;
                        textureHeight = 34;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Core;
                        break;
                    case Negative1:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/NegativeNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/NegativeNodeInactive");
                        textureWidth = 34;
                        textureHeight = 34;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Core;
                        break;
                    default:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                        textureWidth = 62;
                        textureHeight = 62;
                        depAmount = 0;
                        dependencies = new int[depAmount];
                        break;
                }
                nodes[i] = new WDALTSkillNode(false, i, dependencies, enabledTexture, disabledTexture, textureWidth, textureHeight);
            }
            nodesInitialized = true;
        }

        public override void Initialize()
        {
            if (Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (!nodesInitialized)
            {
                InitNodes();
            }
        }

        public static bool IsNodeEnabled(int type)
        {
            if (Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return false;
            }
            if (!nodesInitialized)
            {
                InitNodes();
            }
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
            if (!Main.dedServ && Player.whoAmI == Main.myPlayer && Main.netMode != NetmodeID.Server)
            {
                if (nodes[type].enabled == false)
                {
                    nodes[type].enabled = true;
                    if (!IsSkillTreeValid())
                    {
                        nodes[type].enabled = false;
                        SoundEngine.PlaySound(SoundID.Item53, Player.Center);
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item90, Player.Center);
                    }
                }
                else
                {
                    nodes[type].enabled = false;
                    if (!IsSkillTreeValid())
                    {
                        nodes[type].enabled = true;
                        SoundEngine.PlaySound(SoundID.Item53, Player.Center);
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item90, Player.Center);
                    }
                }
            }
        }

        public static bool IsSkillTreeValid()
        {
            if (Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return false;
            }
            if (!nodesInitialized)
            {
                InitNodes();
            }
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

        public void OpenSkillTreeGUI(bool forceClose = false)
        {
            if (Player.whoAmI != Main.myPlayer || Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (UI?.CurrentState == SkillUI || forceClose)
            {
                UI?.SetState(null);
            }
            else
            {
                UI?.SetState(SkillUI);
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (WDALTKeybindSystem.SkillTreeKeybind.JustPressed && Player.whoAmI == Main.myPlayer && !Main.dedServ && Main.netMode != NetmodeID.Server)
            {
                OpenSkillTreeGUI();
            }
            if (triggersSet.Inventory && Player.whoAmI == Main.myPlayer && !Main.dedServ && Main.netMode != NetmodeID.Server)
            {
                OpenSkillTreeGUI(forceClose: true);
            }
        }

        public override void LoadData(TagCompound tag)
        {
            if (Player.whoAmI != Main.myPlayer || Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (tag.ContainsKey("Core"))
            {
                (nodes[Core]).enabled = tag.GetBool("Core");
            }
            if (tag.ContainsKey("Positive1"))
            {
                (nodes[Positive1]).enabled = tag.GetBool("Positive1");
            }
            if (tag.ContainsKey("Negative1"))
            {
                (nodes[Negative1]).enabled = tag.GetBool("Negative1");
            }
            if (!IsSkillTreeValid())
            {
                InitNodes(reset: true);
            }
        }

        public override void SaveData(TagCompound tag)
        {
            if (Player.whoAmI != Main.myPlayer || Main.dedServ || Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (!IsSkillTreeValid())
            {
                return;
            }
            if ((nodes[Core]).enabled)
            {
                tag["Core"] = (nodes[Core]).enabled;
            }
            if ((nodes[Positive1]).enabled)
            {
                tag["Positive1"] = (nodes[Positive1]).enabled;
            }
            if ((nodes[Negative1]).enabled)
            {
                tag["Negative1"] = (nodes[Negative1]).enabled;
            }
        }
    }
}
