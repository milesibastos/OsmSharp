﻿// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2012 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Math.VRP.Core.Routes;
using OsmSharp.Math.VRP.Core;

namespace OsmSharp.Routing.VRP.WithDepot.MaxTime.InterImprovements
{
    /// <summary>
    /// Abstract an inter-improvement algorithm that tries to exhange edges/customers between two routes to obtain a better result.
    /// </summary>
    public interface IInterImprovement
    {
        /// <summary>
        /// Returns the name of this inter-improvement algorithm.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Returns true if this inter-improvement is symmetric.
        /// </summary>
        bool IsSymmetric
        {
            get;
        }

        /// <summary>
        /// Returns true if there was an improvement.
        /// </summary>
        /// <param name="problem"></param>
        /// <param name="solution"></param>
        /// <param name="route2_idx"></param>
        /// <param name="max"></param>
        /// <param name="route1_idx"></param>
        /// <returns></returns>
        bool Improve(MaxTimeProblem problem, MaxTimeSolution solution,
            int route1_idx, int route2_idx, double max);
    }
}