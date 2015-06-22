// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseMission.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.SemiTrucker.Missions
{
    using System.Collections.Generic;

    using GTA;
    using GTA.Math;
    using GTA.Native;

    public class BaseMission
    {
        public Vehicle Truck { get; set; }

        public VehicleHash TruckModelHash { get; set; }

        public Vector3 TruckSpawnPosition { get; set; }

        public float TruckHeading { get; set; }

        public float TruckDirtLevel { get; set; }

        public List<MissionObjective> Objectives { get; set; }

        public int Payout { get; set; }


        public void Start()
        {
            // TODO -- Add logger here

            Game.FadeScreenOut(1000);
            Game.Player.CanControlCharacter = false;

            // Spawn a trailer at each objective
            foreach (var objective in this.Objectives)
            {
                // TODO -- Check there isn't already a vehicle at the specified coords
                var trailerModel = new Model(objective.TrailerInfo.Hash);
                objective.Trailer = World.CreateVehicle(trailerModel, objective.TrailerInfo.Position, objective.TrailerInfo.Heading);
                objective.Trailer.DirtLevel = objective.TrailerInfo.DirtLevel;
            }

            // Spawn the truck
            // TODO -- Check there isn't already a vehicle at the specified coords
            var truckModel = new Model(this.TruckModelHash);
            this.Truck = World.CreateVehicle(truckModel, this.TruckSpawnPosition, this.TruckHeading);
            this.Truck.DirtLevel = this.TruckDirtLevel;

            // Show a blip on the map
            var truckBlip = this.Truck.AddBlip();
            truckBlip.ShowRoute = true;

            // Show an objective hint
            UI.ShowSubtitle("Get in the truck", 6000);

            Game.FadeScreenIn(1000);
            Game.Player.CanControlCharacter = true;
        }
    }
}
