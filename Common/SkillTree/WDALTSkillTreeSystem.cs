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
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Common.SkillTree
{
    public class WDALTSkillTreeSystem : ModPlayer
    {
        public static int unlockedSkillPoints = 3;
        public static bool unlockedSkillPoint1 = false;
        public const int amountNodes = 4;
        public const int amountConnectors = 3;
        public static WDALTSkillNode[] nodes = new WDALTSkillNode[amountNodes];
        public static WDALTSkillConnector[] connectors = new WDALTSkillConnector[amountConnectors];
        public static bool nodesInitialized = false;
        public const int Core = 0;
        public const int Positive1 = 1;
        public const int Negative1Thorium = 2;
        public const int ThoriumClassBooster = 3;
        public const int Connector_Core_Positive1 = 0;
        public const int Connector_Core_Negative1Thorium = 1;
        public const int Connector_Negative1Thorium_ThoriumClassBooster = 2;
        public static UserInterface UI;
        public class UI_ST : UIState
        {
            public UIImage[] nodeButtons = new UIImage[amountNodes];
            public UIImage[] connectorButtons = new UIImage[amountConnectors];
            public UIPanel panel;
            public UIText skillPointDisplay;
            public bool buttonsInit = false;

            public UI_ST()
            {
                OnInitialize();
            }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                base.DrawSelf(spriteBatch);
                panel.Draw(spriteBatch);
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
                            displayText = "Core Node\n+5% attack damage\nReduces damage taken by 5%";
                            break;
                        case Positive1:
                            displayText = "Positive Node\n+2% attack damage";
                            break;
                        case Negative1Thorium:
                            displayText = "Necessary Evil\nReduces damage done by\nall Vanilla classes by 25%";
                            break;
                        case ThoriumClassBooster:
                            displayText = "Thorium Acclimatization\nIncreases the damage bonus applied to\nThorium weapons from 20% to 30%";
                            break;
                        default:
                            displayText = "Core Node.\n+5% attack damage\nReduces damage taken by 5%";
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
                buttonsInit = false;
            }

            public override void OnActivate()
            {
                if (!buttonsInit)
                {
                    skillPointDisplay = new UIText("Skill Points Remaining: " + unlockedSkillPoints);
                    skillPointDisplay.HAlign = 0.5f;
                    skillPointDisplay.Top.Set(48, 0);
                    panel.Append(skillPointDisplay);
                    float buttonHAlign;
                    float buttonVAlign;
                    MouseEvent call;
                    for (int i = 0; i < nodeButtons.Length; i++)
                    {
                        switch (i)
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
                            case Negative1Thorium:
                                buttonHAlign = 0.5f;
                                buttonVAlign = 0.65f;
                                call = OnClick_Node_Negative1Thorium;
                                break;
                            case ThoriumClassBooster:
                                buttonHAlign = 0.5f;
                                buttonVAlign = 0.8f;
                                call = OnClick_Node_ThoriumClassBooster;
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
                    for (int i = 0; i < connectorButtons.Length; i++)
                    {
                        switch (i)
                        {
                            case Connector_Core_Positive1:
                                buttonHAlign = 0.375f;
                                buttonVAlign = 0.5f;
                                break;
                            case Connector_Core_Negative1Thorium:
                                buttonHAlign = 0.5f;
                                buttonVAlign = 0.5875f;
                                break;
                            case Connector_Negative1Thorium_ThoriumClassBooster:
                                buttonHAlign = 0.5f;
                                buttonVAlign = 0.70625f;
                                break;
                            default:
                                buttonHAlign = 0.375f;
                                buttonVAlign = 0.5f;
                                break;
                        }
                        connectorButtons[i] = new UIImage(connectors[i].disabledTexture);
                        connectorButtons[i].SetImage(connectors[i].disabledTexture);
                        connectorButtons[i].Width.Set(connectors[i].textureWidth, 0f);
                        connectorButtons[i].Height.Set(connectors[i].textureHeight, 0f);
                        connectorButtons[i].HAlign = buttonHAlign;
                        connectorButtons[i].VAlign = buttonVAlign;
                        panel.Append(connectorButtons[i]);
                    }
                    buttonsInit = true;
                }
                int currentSkillPoints = 0;
                for (int i = 0; i < nodeButtons.Length; i++)
                {
                    if (IsNodeEnabled(i))
                    {
                        currentSkillPoints += nodes[i].skillPointCost;
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
                for (int i = 0; i < connectorButtons.Length; i++)
                {
                    if (IsConnectorEnabled(i))
                    {
                        connectorButtons[i].SetImage(connectors[i].enabledTexture);
                        connectorButtons[i].Width.Set(connectors[i].textureWidth, 0f);
                        connectorButtons[i].Height.Set(connectors[i].textureHeight, 0f);
                    }
                    else
                    {
                        connectorButtons[i].SetImage(connectors[i].disabledTexture);
                        connectorButtons[i].Width.Set(connectors[i].textureWidth, 0f);
                        connectorButtons[i].Height.Set(connectors[i].textureHeight, 0f);
                    }
                }
                skillPointDisplay.SetText("Skill Points Remaining: " + (unlockedSkillPoints - currentSkillPoints));
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

            public void OnClick_Node_Negative1Thorium(UIMouseEvent evt, UIElement listeningElement)
            {
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem tree))
                {
                    tree.ToggleNode(Negative1Thorium);
                    this.Deactivate();
                    this.Activate();
                }
            }

            public void OnClick_Node_ThoriumClassBooster(UIMouseEvent evt, UIElement listeningElement)
            {
                if (Main.myPlayer >= 0 && Main.myPlayer < Main.player.Length && Main.player[Main.myPlayer].TryGetModPlayer<WDALTSkillTreeSystem>(out WDALTSkillTreeSystem tree))
                {
                    tree.ToggleNode(ThoriumClassBooster);
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
                int skillPointCost;
                switch (i)
                {
                    case Core:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                        textureWidth = 62;
                        textureHeight = 62;
                        depAmount = 0;
                        dependencies = new int[depAmount];
                        skillPointCost = 2;
                        break;
                    case Positive1:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/PositiveNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/PositiveNodeInactive");
                        textureWidth = 34;
                        textureHeight = 34;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Core;
                        skillPointCost = 1;
                        break;
                    case Negative1Thorium:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/NegativeNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/NegativeNodeInactive");
                        textureWidth = 34;
                        textureHeight = 34;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Core;
                        skillPointCost = 1;
                        break;
                    case ThoriumClassBooster:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/ThoriumNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/ThoriumNodeInactive");
                        textureWidth = 62;
                        textureHeight = 62;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Negative1Thorium;
                        skillPointCost = 1;
                        break;
                    default:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNode");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Nodes/CoreNodeInactive");
                        textureWidth = 62;
                        textureHeight = 62;
                        depAmount = 0;
                        dependencies = new int[depAmount];
                        skillPointCost = 2;
                        break;
                }
                nodes[i] = new WDALTSkillNode(false, i, dependencies, enabledTexture, disabledTexture, textureWidth, textureHeight, skillPointCost);
            }
            for (int i = 0; i < connectors.Length; i++)
            {
                int[] dependencies;
                int depAmount;
                Asset<Texture2D> enabledTexture;
                Asset<Texture2D> disabledTexture;
                int textureWidth;
                int textureHeight;
                switch (i)
                {
                    case Connector_Core_Positive1:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Connectors/HorizontalConnection");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Connectors/HorizontalConnectionInactive");
                        textureWidth = 26;
                        textureHeight = 14;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Core;
                        break;
                    case Connector_Core_Negative1Thorium:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Connectors/VerticalConnection");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Connectors/VerticalConnectionInactive");
                        textureWidth = 14;
                        textureHeight = 26;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Core;
                        break;
                    case Connector_Negative1Thorium_ThoriumClassBooster:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Connectors/VerticalConnection");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Connectors/VerticalConnectionInactive");
                        textureWidth = 14;
                        textureHeight = 26;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Negative1Thorium;
                        break;
                    default:
                        enabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Connectors/HorizontalConnection");
                        disabledTexture = mod.Assets.Request<Texture2D>("Content/SkillTree/Connectors/HorizontalConnectionInactive");
                        textureWidth = 26;
                        textureHeight = 14;
                        depAmount = 1;
                        dependencies = new int[depAmount];
                        dependencies[0] = Core;
                        break;
                }
                connectors[i] = new WDALTSkillConnector(i, dependencies, enabledTexture, disabledTexture, textureWidth, textureHeight);
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
            if (Main.dedServ || Main.netMode == NetmodeID.Server || type >= nodes.Length || type < 0)
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

        public static bool IsConnectorEnabled(int type)
        {
            if (Main.dedServ || Main.netMode == NetmodeID.Server || type >= connectors.Length || type < 0)
            {
                return false;
            }
            if (!nodesInitialized)
            {
                InitNodes();
            }
            for (int i = 0; i < connectors[type].dependencies.Length; i++)
            {
                if (!nodes[connectors[type].dependencies[i]].enabled)
                {
                    return false;
                }
            }
            return true;
        }

        public void ToggleNode(int type)
        {
            if (!Main.dedServ && Player.whoAmI == Main.myPlayer && Main.netMode != NetmodeID.Server && type < nodes.Length && type >= 0)
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
            int currentSkillPoints = 0;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null && nodes[i].enabled && nodes[i].dependencies != null)
                {
                    currentSkillPoints += nodes[i].skillPointCost;
                    for (int j = 0; j < nodes[i].dependencies.Length; j++)
                    {
                        if
                        (
                            nodes[i].dependencies[j] >= 0 &&
                            nodes[i].dependencies[j] < nodes.Length &&
                            nodes[nodes[i].dependencies[j]] != null &&
                            !nodes[nodes[i].dependencies[j]].enabled
                        )
                        {
                            return false;
                        }
                    }
                }
            }
            if (currentSkillPoints > unlockedSkillPoints)
            {
                return false;
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
            unlockedSkillPoints = 3;
            unlockedSkillPoint1 = false;
            if (tag.ContainsKey("UnlockedSkillPoint1"))
            {
                unlockedSkillPoint1 = tag.GetBool("UnlockedSkillPoint1");
                if (unlockedSkillPoint1)
                {
                    unlockedSkillPoints++;
                }
            }
            if (tag.ContainsKey("Core"))
            {
                (nodes[Core]).enabled = tag.GetBool("Core");
            }
            if (tag.ContainsKey("Positive1"))
            {
                (nodes[Positive1]).enabled = tag.GetBool("Positive1");
            }
            if (tag.ContainsKey("Negative1Thorium"))
            {
                (nodes[Negative1Thorium]).enabled = tag.GetBool("Negative1Thorium");
            }
            if (tag.ContainsKey("ThoriumClassBooster"))
            {
                (nodes[Negative1Thorium]).enabled = tag.GetBool("ThoriumClassBooster");
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
            OpenSkillTreeGUI(forceClose: true);
            tag["UnlockedSkillPoint1"] = unlockedSkillPoint1;
            if (!IsSkillTreeValid())
            {
                InitNodes(reset: true);
            }
            tag["Core"] = (nodes[Core]).enabled;
            tag["Positive1"] = (nodes[Positive1]).enabled;
            tag["Negative1Thorium"] = (nodes[Negative1Thorium]).enabled;
            tag["ThoriumClassBooster"] = (nodes[ThoriumClassBooster]).enabled;
        }

        public override void UpdateEquips()
        {
            if (IsNodeEnabled(Core))
            {
                Player.GetDamage(DamageClass.Generic) += 0.05f;
                Player.endurance += 0.05f;
            }
            if (IsNodeEnabled(Positive1))
            {
                Player.GetDamage(DamageClass.Generic) += 0.02f;
            }
            if (IsNodeEnabled(Negative1Thorium))
            {
                Player.GetDamage(DamageClass.Melee) -= 0.25f;
                Player.GetDamage(DamageClass.Ranged) -= 0.25f;
                Player.GetDamage(DamageClass.Magic) -= 0.25f;
                Player.GetDamage(DamageClass.Summon) -= 0.25f;
            }
            if (IsNodeEnabled(ThoriumClassBooster))
            {
                Player.GetModPlayer<WDALTPlayer>().skillTreeThoriumBuffNode = true;
            }
            base.UpdateEquips();
        }
    }
}
