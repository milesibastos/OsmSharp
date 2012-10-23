﻿// OsmSharp - OpenStreetMap tools & library.
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
using Tools.Math.Units.Time;
using Osm.Routing.Core.Route;
using Tools.Math.TSP.Problems;
using Tools.Math.VRP.Core;
using Tools.Math.VRP.Core.Routes;
using Tools.Math.VRP.Core.BestPlacement;
using Tools.Math.VRP.Core.Routes.ASymmetric;
using Osm.Core;
using Osm.Routing.Core.Resolving;

namespace Osm.Routing.Core.VRP.NoDepot.MaxTime.BestPlacement
{
    public class RouterBestPlacement<ResolvedType> : RouterMaxTime<ResolvedType>
        where ResolvedType : IResolvedPoint
    {
        /// <summary>
        /// Creates a new best placement min max no depot vrp router.
        /// </summary>
        /// <param name="router"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public RouterBestPlacement(IRouter<ResolvedType> router, Second max, Second delivery_time)
            :base(router, max, delivery_time)
        {

        }

        /// <summary>
        /// Returns the name of this solver.
        /// </summary>
        public override string Name
        {
            get 
            { 
                return "CI"; 
            }
        }

        /// <summary>
        /// Executes a solver procedure.
        /// </summary>
        /// <param name="problem"></param>
        /// <returns></returns>
        internal override MaxTimeSolution Solve(MaxTimeProblem problem)
        {
            // create the calculator.
            MaxTimeCalculator calculator = new MaxTimeCalculator(problem);

            // create the solution.
            MaxTimeSolution solution = new MaxTimeSolution(problem.Size, true);

            // keep placing customer until none are left.
            List<int> customers = new List<int>(problem.Customers);
            while (customers.Count > 0)
            {
                // select a customer using some heuristic.
                int customer_idx = Tools.Math.Random.StaticRandomGenerator.Get().Generate(customers.Count);
                int customer = customers[customer_idx];
                customers.Remove(customer);

                // start a route r.
                float current_route_weight = 0;
                IRoute current_route = solution.Add(customer);
                //Console.WriteLine("Starting new route with {0}", customer);
                while (customers.Count > 0)
                {
                    // calculate the best placement.
                    CheapestInsertionResult result =
                        CheapestInsertionHelper.CalculateBestPlacement(problem, current_route, customers);

                    // calculate the new weight.
                    float potential_weight = calculator.CalculateOneRouteIncrease(current_route_weight, result.Increase);
                    // cram as many customers into one route as possible.
                    if (potential_weight < problem.Max.Value)
                    {
                        // insert the customer, it is 
                        customers.Remove(result.Customer);
                        current_route.Insert(result.CustomerBefore, result.Customer, result.CustomerAfter);
                        current_route_weight = potential_weight;

                        //// improve if needed.
                        ////if (improvement_probalitity > Tools.Math.Random.StaticRandomGenerator.Get().Generate(1))
                        //if (((problem.Size - customers.Count) % _k) == 0)
                        //{ // an improvement is descided.
                        //    current_route_weight = this.ImproveIntraRoute(problem,
                        //        current_route, current_route_weight);
                        //}
                    }
                    else
                    {// ok we are done!
                        //// apply the intra-route heuristics.
                        //for (int route_idx = 0; route_idx < solution.Count - 1; route_idx++)
                        //{ // apply the intra-route heurstic between the new and all existing routes.
                        //    this.ImproveInterRoute(problem, solution.Route(route_idx), current_route);
                        //}

                        //// apply the inter-route heuristics.
                        //for (int route_idx = 0; route_idx < solution.Count; route_idx++)
                        //{ // apply heurstic for each route.
                        //    IRoute route = solution.Route(route_idx);
                        //    this.ImproveIntraRoute(problem, current_route, current_route_weight);
                        //}

                        // break the route.
                        break;
                    }
                }
            }

            return solution;
        }
    }
}