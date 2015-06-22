// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="nevada_scout">
//   Copyright (c) nevada_scout 2015. All Rights Reserved.
//   This code is part of nevada_scout's mod suite for GTA V.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NS.Common.Internal
{
    using System;
    using System.IO;

    public static class Logger
    {
        public static void Log(object message)
        {
            File.AppendAllText("ns_common.log", DateTime.Now + " : " + message + Environment.NewLine);
        }
    }
}
