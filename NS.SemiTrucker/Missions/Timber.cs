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
            this.TruckModelHash = VehicleHash.Hauler;
            this.TruckSpawnPosition = new Vector3(0, 0, 0);
            this.TruckHeading = 1f;
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
                                                      Hash = VehicleHash.DockTrailer,
                                                      Position = new Vector3(0, 0, 0),
                                                      Heading = 1f,
                                                      DirtLevel = 7f
                                                  },
                                              StartLocation = new Vector3(0, 0, 0),
                                              Destination = new Vector3(1, 1, 1),
                                              TrailerUnhitchPosition = new Vector3(2, 2, 2)
                                          },
                                      new MissionObjective
                                          {
                                              ObjectiveOrder = 2,
                                              Description = "Take the logs to the lumber yard",
                                              TrailerInfo = new TrailerInfo
                                                  {
                                                      Hash = VehicleHash.TrailerLogs,
                                                      Position = new Vector3(0, 0, 0),
                                                      Heading = 1f,
                                                      DirtLevel = 15f
                                                  },
                                              StartLocation = new Vector3(0, 0, 0),
                                              Destination = new Vector3(1, 1, 1),
                                              TrailerUnhitchPosition = new Vector3(2, 2, 2)
                                          }
                                  };
        }
    }
}
