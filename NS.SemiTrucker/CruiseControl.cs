// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CruiseControl.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.SemiTrucker
{
    using GTA;

    public class CruiseControl
    {
        private float speed;

        public bool IsActive { get; set; }

        public void SetSpeed()
        {
            // TODO -- Allow speed to slow down if vehicle wheels are not on the ground
            if (this.IsActive) // && Game.Player.Character.CurrentVehicle.IsOnAllWheels
            {
                Game.Player.Character.CurrentVehicle.Speed = this.speed;
            }
        }

        public void Enable(bool useLastSpeed = false)
        {
            this.IsActive = true;

            if (!useLastSpeed)
            {
                this.speed = Game.Player.Character.CurrentVehicle.Speed;
            }

            UI.Notify(string.Format("Cruise Control Enabled.\nSpeed: {0}", this.speed));
        }

        public void Disable()
        {
            this.IsActive = false;

            UI.Notify("Cruise Control Disabled");
        }

        public void IncreaseSpeed()
        {
            this.speed += 2;

            UI.Notify("Increasing speed by 2");
        }

        public void DecreaseSpeed()
        {
            this.speed -= 2;

            UI.Notify("Decreasing speed by 2");
        }
    }
}
