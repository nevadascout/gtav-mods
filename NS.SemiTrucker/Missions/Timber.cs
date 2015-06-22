// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Timber.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.SemiTrucker.Missions
{
    using System.Collections.Generic;

    using GTA.Math;
    using GTA.Native;

    public class Timber : BaseMission
    {
        public Timber()
        {
            this.TruckModelHash = VehicleHash.Hauler; // Phantom
            this.TruckSpawnPosition = new Vector3(1730.482f, -1555.9f, 112.2797f);
            this.TruckHeading = 80.61585f;
            this.TruckDirtLevel = 15f;

            this.Payout = 4500;

            this.Objectives = new List<MissionObjective>
                                  {
                                      new MissionObjective
                                          {
                                              ObjectiveOrder = 1,
                                              Description = "Take the container to the sawmill",
                                              TrailerInfo = new TrailerInfo
                                                  {
                                                      Hash = VehicleHash.Trailers2,
                                                      Position = new Vector3(1737.056f, -1535.493f, 112.298f),
                                                      Heading = 247.2201f,
                                                      DirtLevel = 7f
                                                  },
                                              StartLocation = new Vector3(1730.482f, -1555.9f, 112.2797f),
                                              Destination = new Vector3(-589.4041f, 5302.249f, 70.44966f),
                                              TrailerUnhitchPosition = new Vector3(-579.5291f, 5373.465f, 70.45017f)
                                          },
                                      new MissionObjective
                                          {
                                              ObjectiveOrder = 2,
                                              Description = "Take the logs to the lumber yard",
                                              TrailerInfo = new TrailerInfo
                                                  {
                                                      Hash = VehicleHash.TrailerLogs,
                                                      Position = new Vector3(-576.3062f, 5373.465f, 70.51006f),
                                                      Heading = 274.3176f,
                                                      DirtLevel = 15f
                                                  },
                                              StartLocation = new Vector3(-576.3062f, 5373.465f, 70.51006f),
                                              Destination = new Vector3(1204.871f, -1313.668f, 35.46343f),
                                              TrailerUnhitchPosition = new Vector3(1204.871f, -1313.668f, 35.46343f)
                                          }
                                  };
        }
    }
}
