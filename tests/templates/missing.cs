﻿// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated from BlockTemplate.tt/>

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.GameActions;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.Items;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Core.Controller;
    using Eco.Gameplay.Components.Storage;
    using Eco.Core.Utils;
    using Eco.Gameplay.Items.Recipes;

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(CustomTextComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "PinkPoweredCart Item")]
    public partial class PinkPoweredCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static PinkPoweredCartObject()
        {
            WorldObject.AddOccupancy<PinkPoweredCartObject>(new List<BlockOccupancy>(0));
        }
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override bool PlacesBlocks => false;
        public override LocString DisplayName { get { return Localizer.DoStr("Pink Powered Cart"); } }
        public Type RepresentedItemType { get { return typeof(PinkPoweredCartItem); } }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel",
        };
        private PinkPoweredCartObject() { }
        protected override void Initialize()
        {
            base.Initialize();
            this.GetComponent<CustomTextComponent>().Initialize(200);
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(55);
            this.GetComponent<AirPollutionComponent>().Initialize(0.1f);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);
            this.GetComponent<PublicStorageComponent>().Initialize(18, 3500000);
            this.GetComponent<MinimapComponent>().InitAsMovable();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Vehicles"));
            this.GetComponent<VehicleComponent>().Initialize(12, 1.5f, 1);
            this.GetComponent<VehicleComponent>().FailDriveMsg = Localizer.Do($"You are too hungry to drive {this.DisplayName}!");
        }
    }

    [Serialized]
    [LocDisplayName("Pink Powered Cart")]
    [Weight(15000)]
    [Tag("ColoredPoweredCart")]
    [AirPollution(0.1f)]
    [LocDescription("Large cart for hauling sizable loads.")]
    [IconGroup("World Object Minimap")]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true)]
    public partial class PinkPoweredCartItem : WorldObjectItem<PinkPoweredCartObject>, IPersistentData
    {
        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)] public object PersistentData { get; set; }
    }
}
