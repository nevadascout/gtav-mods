// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MissionObjective.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.SemiTrucker.Missions
{
    using GTA;
    using GTA.Math;

    public class MissionObjective
    {
        public MissionObjective()
        {
            this.Completed = false;
            this.InProgress = false;
        }

        public TrailerInfo TrailerInfo { get; set; }

        public Vehicle Trailer { get; set; }

        public Vector3 StartLocation { get; set; }

        public Vector3 Destination { get; set; }

        public Vector3 TrailerUnhitchPosition { get; set; }

        public Blip DestinationBlip { get; set; }

        public string Description { get; set; }

        public int ObjectiveOrder { get; set; }

        public bool InProgress { get; set; }

        public bool Completed { get; set; }
    }
}
