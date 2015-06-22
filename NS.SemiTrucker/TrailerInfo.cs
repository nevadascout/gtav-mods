// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrailerInfo.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.SemiTrucker
{
    using GTA.Math;
    using GTA.Native;

    public class TrailerInfo
    {
        public TrailerInfo()
        {
            this.DirtLevel = 4f;
        }

        public VehicleHash Hash { get; set; }

        public Vector3 Position { get; set; }

        public float Heading { get; set; }

        public float DirtLevel { get; set; }
    }
}
