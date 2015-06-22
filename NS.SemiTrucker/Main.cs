// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.SemiTrucker
{
    using System;
    using System.Windows.Forms;

    using GTA;
    using GTA.Math;

    using NS.SemiTrucker.Internal;

    using Control = GTA.Control;

    public class Main : Script
    {
        private MissionRunner missionRunner;

        private CruiseControl cruiseControl;

        private Vector3 truckerMissionStartPoint = new Vector3(1715.841f, -1590.368f, 112.1237f);

        private Blip truckerMissionStartPointBlip;


        public Main()
        {
            this.Tick += this.OnTick;
            this.KeyDown += this.OnKeyDown;

            this.missionRunner = new MissionRunner(debugMode: true);
            this.cruiseControl = new CruiseControl();

            Logger.Log("STARTING NS.SEMITRUCKER MOD");

            // Show the trucker mission start checkpoint + map blip
            this.truckerMissionStartPointBlip = World.CreateBlip(this.truckerMissionStartPoint);
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (this.missionRunner == null) this.missionRunner = new MissionRunner(debugMode: true);
            
            if (this.cruiseControl == null) this.cruiseControl = new CruiseControl();
            
            if (this.missionRunner.MissionIsRunning)
            {
                this.missionRunner.Run();
            }

            var player = Game.Player;

            // TODO -- Create a mission menu to allow the player to pick a mission to play (?)
            // TODO -- Store played missions this session so a player doesn't get the same mission twice in one game session (?)

            // Start mission if the player is at the start point + not currently on a mission
            if (player.CanStartMission && !this.missionRunner.MissionIsRunning)
            {
                // Show the trucker mission start checkpoint + map blip
                //this.truckerMissionStartPointBlip = World.CreateBlip(this.truckerMissionStartPoint);
                //this.truckerMissionStartPointBlip.Sprite = BlipSprite.ArmoredTruck;


                // Start a mission if the player walks into the start point marker
                if (player.Character.Position.DistanceTo(this.truckerMissionStartPoint) <= 1)
                {
                    this.missionRunner.Start();
                }
            }

            // Set vehicle speed if cruise control is enabled
            if (Game.Player.Character.IsInVehicle())
            {
                this.cruiseControl.SetSpeed();

                // Disable cruise control if the player presses the brake or speeds up
                if (PlayerIsChangingVehicleSpeed() && this.cruiseControl.IsActive)
                {
                    this.cruiseControl.Disable();
                }
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Enable cruise control + set it to the current vehicle speed
            if (e.KeyCode == Keys.Multiply)
            {
                Logger.Log("Enable cruise control");
                this.cruiseControl.Enable();
            }

            // Enable cruise control + set it to the last stored speed
            if (e.KeyCode == Keys.Divide)
            {
                Logger.Log("Enable cruise control - last speed");
                this.cruiseControl.Enable(useLastSpeed: true);
            }

            if (e.KeyCode == Keys.Add)
            {
                this.cruiseControl.IncreaseSpeed();
            }

            if (e.KeyCode == Keys.Subtract)
            {
                this.cruiseControl.DecreaseSpeed();
            }

            if (e.KeyCode == Keys.F5)
            {
                UI.Notify("Saving location player + heading");
                Logger.Log("POS:" + Game.Player.Character.CurrentVehicle.Position + " HEADING: " + Game.Player.Character.CurrentVehicle.Heading);
            }
        }

        private static bool PlayerIsChangingVehicleSpeed()
        {
            if (Game.IsControlPressed(0, Control.VehicleBrake))
            {
                return true;
            }

            if (Game.IsControlPressed(0, Control.VehicleHandbrake))
            {
                return true;
            }

            if (Game.IsControlPressed(0, Control.VehicleAccelerate))
            {
                return true;
            }

            return false;
        }
    }
}
