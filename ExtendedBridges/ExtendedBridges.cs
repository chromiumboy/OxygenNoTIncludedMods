﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using TUNING;
using UnityEngine;
using HarmonyLib;

namespace ExtendedBridges
{

    // Expands ModUtil 
    class ModUtilExtended : KMonoBehaviour
    { 
        // Add building to planning screen at specified position
        public static void AddBuildingToPlanScreen(HashedString category, string building_id, int id = 0)
        {
            // Use officially supported call to add to the build menu, which sadly doesn't support ordering after another building id.
            ModUtil.AddBuildingToPlanScreen(category, building_id);
        }

        // Get list position of a building
        public static int GetBuildingIndex(HashedString category, string building_id)
        {
            int num = TUNING.BUILDINGS.PLANORDER.FindIndex((PlanScreen.PlanInfo x) => x.category == category);
            if (num < 0)
            { return -1; }

            IList<string> list = TUNING.BUILDINGS.PLANORDER[num].data as IList<string>;
            for (int i = 0; i < list.Count; i++)
            {
                if (string.Equals(list[i],building_id))
                { return i; }
            }

            return -1;
        }
    }

    // Add strings for the bridges and add them to the build menu
    [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
    internal class ExtendedBridges_GeneratedBuildings_LoadGeneratedBuildings
    {
        private static void Prefix()
        {
            // Declare version number
            Debug.Log("LOADED - Longer Bridges by chromiumboy - v 2020.03.08");

            // New strings
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLIQUIDCONDUITBRIDGE.NAME", UI.FormatAsLink("Long Liquid Bridge", "EXTENDEDLIQUIDCONDUITBRIDGE"));
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLIQUIDCONDUITBRIDGE.DESC", "Separate pipe systems prevent mingled contents from causing building damage.");
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLIQUIDCONDUITBRIDGE.EFFECT", string.Concat(new string[] {"Runs one " + UI.FormatAsLink("Liquid Pipe", "LIQUIDPIPING") + " section over another two without joining them.\n\nCan be run through wall and floor tile."}));

            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDGASCONDUITBRIDGE.NAME", UI.FormatAsLink("Long Gas Bridge", "EXTENDEDGASCONDUITBRIDGE"));
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDGASCONDUITBRIDGE.DESC", "Separate pipe systems prevent mingled contents from causing building damage.");
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDGASCONDUITBRIDGE.EFFECT", string.Concat(new string[] { "Runs one " + UI.FormatAsLink("Gas Pipe", "GASPIPING") + " section over another two without joining them.\n\nCan be run through wall and floor tile." }));

            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDSOLIDCONDUITBRIDGE.NAME", UI.FormatAsLink("Long Conveyor Bridge", "EXTENDEDSOLIDCONDUITBRIDGE"));
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDSOLIDCONDUITBRIDGE.DESC", "Separating rail systems helps prevent materials from reaching the wrong destinations.");
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDSOLIDCONDUITBRIDGE.EFFECT", string.Concat(new string[] { "Runs one " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT") + " section over another two without joining them.\n\nCan be run through wall and floor tile." }));

            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDWIREBRIDGE.NAME", UI.FormatAsLink("Long Wire Bridge", "EXTENDEDWIREBRIDGE"));
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDWIREBRIDGE.DESC", "Splitting generators onto separate systems can prevent power overloads and wasted electricity.");
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDWIREBRIDGE.EFFECT", "Runs one wire section over another two without joining them.\n\nCan be run through wall and floor tile.");

            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDWIREREFINEDBRIDGE.NAME", UI.FormatAsLink("Long Conductive Wire Bridge", "EXTENDEDWIREREFINEDBRIDGE"));
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDWIREREFINEDBRIDGE.DESC", "Splitting generators onto separate systems can prevent power overloads and wasted electricity.");
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDWIREREFINEDBRIDGE.EFFECT", string.Concat(new string[] { "Carries more ", UI.FormatAsLink("Wattage", "POWER"), " than a regular ", UI.FormatAsLink("Wire Bridge", "WIREBRIDGE"), " without overloading.\n\nRuns one wire section over another two without joining them.\n\nCan be run through wall and floor tile." }));

            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLOGICWIREBRIDGE.NAME", UI.FormatAsLink("Long Automation Wire Bridge", "EXTENDEDLOGICWIREBRIDGE"));
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLOGICWIREBRIDGE.DESC", "Wire bridges allow multiple automation grids to exist in a small area without connecting.");
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLOGICWIREBRIDGE.EFFECT", string.Concat(new string[] { "Runs one " + UI.FormatAsLink("Automation Wire", "LOGICWIRE") + " section over another two without joining them.\n\nCan be run through wall and floor tile." }));

            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLOGICRIBBONBRIDGE.NAME", UI.FormatAsLink("Long Automation Ribbon Bridge", "EXTENDEDLOGICRIBBONBRIDGE"));
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLOGICRIBBONBRIDGE.DESC", "Wire bridges allow multiple automation grids to exist in a small area without connecting.");
            Strings.Add("STRINGS.BUILDINGS.PREFABS.EXTENDEDLOGICRIBBONBRIDGE.EFFECT", string.Concat(new string[] { "Runs one " + UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON") + " section over another two without joining them.\n\nCan be run through wall and floor tile." }));

            // Add to build menus
            ModUtilExtended.AddBuildingToPlanScreen("Plumbing", ExtendedLiquidConduitBridgeConfig.ID, ModUtilExtended.GetBuildingIndex("Plumbing",LiquidConduitBridgeConfig.ID) + 1);
            ModUtilExtended.AddBuildingToPlanScreen("HVAC", ExtendedGasConduitBridgeConfig.ID, ModUtilExtended.GetBuildingIndex("HVAC", GasConduitBridgeConfig.ID) + 1);
            ModUtilExtended.AddBuildingToPlanScreen("Conveyance", ExtendedSolidConduitBridgeConfig.ID, ModUtilExtended.GetBuildingIndex("Conveyance", SolidConduitBridgeConfig.ID) + 1);
            ModUtilExtended.AddBuildingToPlanScreen("Power", ExtendedWireBridgeConfig.ID, ModUtilExtended.GetBuildingIndex("Power", WireBridgeConfig.ID) + 1);
            ModUtilExtended.AddBuildingToPlanScreen("Power", ExtendedWireRefinedBridgeConfig.ID, ModUtilExtended.GetBuildingIndex("Power", WireRefinedBridgeConfig.ID) + 1);
            ModUtilExtended.AddBuildingToPlanScreen("Automation", ExtendedLogicWireBridgeConfig.ID, ModUtilExtended.GetBuildingIndex("Automation", LogicWireBridgeConfig.ID) + 1);
            ModUtilExtended.AddBuildingToPlanScreen("Automation", ExtendedLogicRibbonBridgeConfig.ID, ModUtilExtended.GetBuildingIndex("Automation", LogicRibbonBridgeConfig.ID) + 1);
        }
    }

    // Add the new bridges to the tech tree
    [HarmonyPatch(typeof(Db), "Initialize")]
    internal class InsulatedPressureDoor_Db_Initialize
    {
        public static void Postfix()
        {
            Tech tech;

            tech = Db.Get().Techs.TryGet("ImprovedLiquidPiping");
            tech.unlockedItemIDs.Add(ExtendedLiquidConduitBridgeConfig.ID);

            tech = Db.Get().Techs.TryGet("ImprovedGasPiping");
            tech.unlockedItemIDs.Add(ExtendedGasConduitBridgeConfig.ID);

            tech = Db.Get().Techs.TryGet("SolidTransport");
            tech.unlockedItemIDs.Add(ExtendedSolidConduitBridgeConfig.ID);
        
            tech = Db.Get().Techs.TryGet("AdvancedPowerRegulation");
            tech.unlockedItemIDs.Add(ExtendedWireBridgeConfig.ID);

            tech = Db.Get().Techs.TryGet("PrettyGoodConductors");
            tech.unlockedItemIDs.Add(ExtendedWireRefinedBridgeConfig.ID);

            tech = Db.Get().Techs.TryGet("LogicCircuits");
            tech.unlockedItemIDs.Add(ExtendedLogicWireBridgeConfig.ID);

            tech = Db.Get().Techs.TryGet("ParallelAutomation");
            tech.unlockedItemIDs.Add(ExtendedLogicRibbonBridgeConfig.ID);
        }
    }

    // Update buildings on spawn
    [HarmonyPatch(typeof(Building), "OnSpawn")]
    internal class ExtendedBridges_Building_OnSpawn
    {
        private static void Postfix(Building __instance)
        {
            List<string> bridges = new List<string>() {
                "ExtendedLiquidConduitBridge",
                "ExtendedGasConduitBridge",
                "ExtendedSolidConduitBridge",
                "ExtendedWireBridge",
                "ExtendedWireRefinedBridge",
                "ExtendedLogicWireBridge",
                "ExtendedLogicRibbonBridge",
            };

            // Check to see if the building is on the above list
            string result = bridges.FirstOrDefault(s => __instance.name.Contains(s));

            if (!string.IsNullOrEmpty(result))
            {
                // Stretch the anim
                Debug.Log("Extending " + __instance.name);
                KBatchedAnimController animController = __instance.gameObject.GetComponent<KBatchedAnimController>();
                animController.animWidth = 1.4f;

                // Change the prefab tag on the extended automation bridge
                if (string.Equals(__instance.name, "ExtendedLogicWireBridgeComplete"))
                {
                    KPrefabID kPrefabID = __instance.gameObject.AddOrGet<KPrefabID>();
                    kPrefabID.PrefabTag = TagManager.Create("LogicWireBridge");
                }

                // Change the prefab tag on the extended automation ribbon bridge
                if (string.Equals(__instance.name, "ExtendedLogicRibbonBridgeComplete"))
                {
                    KPrefabID kPrefabID = __instance.gameObject.AddOrGet<KPrefabID>();
                    kPrefabID.PrefabTag = TagManager.Create("LogicRibbonBridge");
                }
            }
        }
    }
}
