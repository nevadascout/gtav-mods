// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.SemiTrucker
{
    using System;
    using System.Linq;

    using GTA;
    using GTA.Math;

    using NS.SemiTrucker.MissionEngine;

    public class Main : Script
    {
        private readonly Ped playerPed = Game.Player.Character;

        private Mission activeMission = Mission.None;

        private bool playerIsOnMission;


        public Main()
        {
            this.Tick += this.onTick;
        }

        private void onTick(object sender, EventArgs e)
        {
            if (this.playerIsOnMission)
            {
                this.RunMission();
            }
            else
            {
                // Check if player has moved into our checkpoint marker
                if (this.playerPed.Position == Vector3.RelativeBack && !this.playerIsOnMission)
                {
                    UI.Notify("Walk out of the checkpoint to cancel starting a trucking mission");

                    Script.Wait(1500);

                    // TODO -- Check position again
                    // If the player is still in the checkpoint, start a trucking mission
                    this.StartRandomMission();
                }

                if (this.playerPed.Position == Vector3.RelativeBack && this.playerIsOnMission)
                {
                    this.EndCurrentMission();
                }
            }
        }


        private void RunMission()
        {

        }

        private void StartRandomMission()
        {
            this.playerIsOnMission = true;
            this.activeMission = this.GetRandomMission();

            // if(Game.Player.CanStartMission)
            
            // Fade screen to black, then
            // Spawn truck + trailer (?)
            // Place location marker w/ route
        }

        private void EndCurrentMission()
        {
            this.playerIsOnMission = false;
            this.activeMission = Mission.None;

            // Do Cleanup (?)
        }

        private Mission GetRandomMission()
        {
            var mission = Enum.GetValues(typeof(Mission)).Cast<Mission>().OrderBy(e => Guid.NewGuid()).First();

            if (mission == Mission.None)
            {
                this.GetRandomMission();
            }
            else
            {
                return mission;
            }
        }
    }
}
