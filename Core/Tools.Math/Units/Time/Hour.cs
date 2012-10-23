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

namespace Tools.Math.Units.Time
{
    public class Hour : Unit
    {
        public Hour()
            : base(0.0d)
        {

        }

        private Hour(double value)
            : base(value)
        {

        }

        #region Time-Conversions

        public static implicit operator Hour(double value)
        {
            Hour hr = new Hour(value);
            return hr;
        }

        public static implicit operator Hour(TimeSpan timespan)
        {
            Hour hr = new Hour();
            hr = timespan.TotalMilliseconds * 1000.0d * 3600.0d;
            return hr;
        }

        public static implicit operator Hour(Second sec)
        {
            Hour hr = new Hour();
            hr = sec.Value / 3600.0d;
            return hr;
        }

        #endregion

        public override string ToString()
        {
            return this.Value.ToString() + "H";
        }
    }
}