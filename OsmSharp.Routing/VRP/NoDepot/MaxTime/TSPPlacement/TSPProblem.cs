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
using OsmSharp.Math.VRP.Core;

namespace OsmSharp.Routing.VRP.NoDepot.MaxTime.TSPPlacement
{
    /// <summary>
    /// Represents the equivalent TSP problem related to this VRP.
    /// </summary>
    public class TSPProblem : OsmSharp.Math.TSP.Problems.IProblem
    {
        /// <summary>
        /// Creates a new TSP problem.
        /// </summary>
        /// <param name="vrp"></param>
        public TSPProblem(MaxTimeProblem vrp)
        {
            this.Size = vrp.Size;
            this.Symmetric = false;
            this.Euclidean = false;
            this.First = 0;
            this.Last = 0;
            this.WeightMatrix = vrp.WeightMatrix;
        }

        /// <summary>
        /// Returns the size.
        /// </summary>
        public int Size
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the first if any.
        /// </summary>
        public int? First
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the last if any.
        /// </summary>
        public int? Last
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Returns the symmetric flag.
        /// </summary>
        public bool Symmetric
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns true if the problem is euclidean.
        /// </summary>
        public bool Euclidean
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the weight matrix.
        /// </summary>
        public double[][] WeightMatrix
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the weight between two customers.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public double Weight(int from, int to)
        {
            return this.WeightMatrix[from][to];
        }

        #region Nearest Neighbour

        /// <summary>
        /// Keeps the nearest neighbour list.
        /// </summary>
        private NearestNeighbours10[] _neighbours;

        /// <summary>
        /// Generate the nearest neighbour list.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public NearestNeighbours10 Get10NearestNeighbours(int v)
        {
            if (_neighbours == null)
            {
                _neighbours = new NearestNeighbours10[this.Size];
            }
            NearestNeighbours10 result = _neighbours[v];
            if (result == null)
            {
                SortedDictionary<double, List<int>> neighbours = new SortedDictionary<double, List<int>>();
                for (int customer = 0; customer < this.Size; customer++)
                {
                    if (customer != v)
                    {
                        double weight = this.WeightMatrix[v][customer];
                        List<int> customers = null;
                        if (!neighbours.TryGetValue(weight, out customers))
                        {
                            customers = new List<int>();
                            neighbours.Add(weight, customers);
                        }
                        customers.Add(customer);
                    }
                }

                result = new NearestNeighbours10();
                foreach (KeyValuePair<double, List<int>> pair in neighbours)
                {
                    foreach (int customer in pair.Value)
                    {
                        if (result.Count < 10)
                        {
                            if (result.Max < pair.Key)
                            {
                                result.Max = pair.Key;
                            }
                            result.Add(customer);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                _neighbours[v] = result;
            }
            return result;
        }

        #endregion
    }
}
