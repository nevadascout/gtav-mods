// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MissionRunner.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.SemiTrucker
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using GTA;

    using NS.SemiTrucker.Missions;

    public class MissionRunner
    {
        private readonly Ped player;

        // ReSharper disable once InconsistentNaming
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed. Suppression is okay here.")]
        private readonly bool DEBUG;

        private Mission activeMission;

        private Vehicle truck;

        private List<MissionObjective> objectives;

        public MissionRunner(bool debugMode = false)
        {
            this.activeMission = Mission.None;
            this.MissionIsRunning = false;
            this.player = Game.Player.Character;

            DEBUG = debugMode;
        }

        public bool MissionIsRunning { get; set; }

        public BaseMission ActiveMission { get; set; }


        public void Start()
        {
            var mission = this.GetRandomMission();

            this.activeMission = mission;
            this.MissionIsRunning = true;

            switch (this.activeMission)
            {
                case Mission.Timber:
                    this.ActiveMission = new Missions.Timber();
                    break;
            }

            if (DEBUG) UI.Notify("STARTING MISSION");

            this.ActiveMission.Start();

            this.truck = this.ActiveMission.Truck;
            this.objectives = this.ActiveMission.Objectives.OrderBy(x => x.ObjectiveOrder).ToList();
        }

        public void Run()
        {
            if (this.MissionIsRunning)
            {
                this.RunMission();
            }
        }


        private Mission GetRandomMission()
        {
            // TODO -- Generate a random mission based on mission enum
            return Mission.Timber;
        }

        private void RunMission()
        {
            var completedMissions = this.objectives.Count(p => p.Completed);
            var totalMissions = this.objectives.Count();

            if (completedMissions == totalMissions)
            {
                this.End();
                return;
            }

            var currentObjective = this.objectives.First(p => p.Completed == false);

            if (currentObjective == null)
            {
                if (DEBUG) UI.Notify("ERROR - NO OBJECTIVE FOUND");

                return;
            }

            if (this.player.CurrentVehicle == this.truck)
            {
                // If the player is in the truck, don't show a blip for it
                this.truck.CurrentBlip.Remove();

                // Get the trailer hooked up
                if (!currentObjective.InProgress && this.player.Position.DistanceTo(currentObjective.StartLocation) < 20)
                {
                    currentObjective.Trailer.AddBlip();
                    currentObjective.Trailer.CurrentBlip.ShowRoute = true;

                    UI.ShowSubtitle("Hook up the trailer", 5000);
                }

                // Once trailer is hooked up, start the objective
                // TODO -- Change this to use a check for the trailer being attached instead of distance
                if (!currentObjective.InProgress && this.player.CurrentVehicle.Position.DistanceTo(currentObjective.Trailer.Position) <= 10)
                {
                    if (DEBUG) UI.Notify(string.Format("OBJECTIVE {0} :: STARTED", currentObjective.ObjectiveOrder));

                    currentObjective.InProgress = true;

                    // Remove trailer blip + route to trailer
                    currentObjective.Trailer.CurrentBlip.Remove();

                    // Set route to objective destination
                    currentObjective.DestinationBlip = World.CreateBlip(currentObjective.Destination);
                    currentObjective.DestinationBlip.ShowRoute = true;

                    UI.ShowSubtitle(currentObjective.Description, 6000);
                }

                // Check for mission completion
                if (currentObjective.InProgress && currentObjective.Trailer.Position.DistanceTo(currentObjective.Destination) < 5)
                {
                    // Ask player to unhitch the trailer in the marker
                    // TODO -- create checkpoint marker at unhitch position
                    // var marker = World.DrawMarker(MarkerType.HorizontalCircleFat, currentObjective.TrailerUnhitchPosition, );

                    UI.ShowSubtitle("Unhook the trailer in the marker", 5000);

                    // TODO -- how to check if the trailer is unhitched ?
                    if (currentObjective.Trailer.Position.DistanceTo(currentObjective.TrailerUnhitchPosition) <= 1)
                    {
                        currentObjective.InProgress = false;
                        currentObjective.Completed = true;

                        currentObjective.DestinationBlip.Remove();

                        if (DEBUG) UI.Notify(string.Format("OBJECTIVE {0} :: COMPLETED", currentObjective.ObjectiveOrder));
                    }
                }
            }
            else
            {
                this.truck.AddBlip();
                this.truck.CurrentBlip.ShowRoute = true;
            }
        }

        private void End()
        {
            if (this.ActiveMission.Payout > 0)
            {
                Game.Player.Money += this.ActiveMission.Payout;
            }

            // TODO -- Display message for successful mission completion

            // Do cleanup
            this.activeMission = Mission.None;
            this.MissionIsRunning = false;

            this.truck = null;
            this.objectives = null;


            if (DEBUG) UI.Notify("MISSION COMPLETED");
        }
    }
}
