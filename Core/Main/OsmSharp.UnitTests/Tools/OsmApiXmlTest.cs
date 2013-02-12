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
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsmSharp.Tools.Xml.Gpx;
using OsmSharp.Tools.Xml.Sources;
using System.Reflection;
using System.Xml.Serialization;

namespace OsmSharp.UnitTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class OsmApiXmlTest
    {
        /// <summary>
        /// Test reads an embedded file.
        /// </summary>
        [TestMethod]
        public void OsmApiDeserializeTest()
        {
            Stream capabilities =
                Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.UnitTests.capabilities.xml");

            XmlSerializer capabilities_serializer = new XmlSerializer(typeof(
                OsmSharp.Osm.Core.Xml.v0_6.osm));

            OsmSharp.Osm.Core.Xml.v0_6.osm osm =
                (capabilities_serializer.Deserialize(capabilities) as OsmSharp.Osm.Core.Xml.v0_6.osm);
            OsmSharp.Osm.Core.Xml.v0_6.api api = osm.api;

            Assert.IsNotNull(api.area);
            Assert.IsTrue(api.area.maximumSpecified);
            Assert.AreEqual(api.area.maximum, 0.25);

            Assert.IsNotNull(api.changesets);
            Assert.IsTrue(api.changesets.maximum_elementsSpecified);
            Assert.AreEqual(api.changesets.maximum_elements, 50000);

            Assert.IsNotNull(api.timeout);
            Assert.IsTrue(api.timeout.secondsSpecified);
            Assert.AreEqual(api.timeout.seconds, 300);

            Assert.IsNotNull(api.tracepoints);
            Assert.IsTrue(api.tracepoints.per_pageSpecified);
            Assert.AreEqual(api.tracepoints.per_page, 5000);

            Assert.IsNotNull(api.version);
            Assert.IsTrue(api.version.maximumSpecified);
            Assert.AreEqual(api.version.maximum, 0.6);
            Assert.IsTrue(api.version.minimumSpecified);
            Assert.AreEqual(api.version.minimum, 0.6);

            Assert.IsNotNull(api.waynodes);
            Assert.IsTrue(api.waynodes.maximumSpecified);
            Assert.AreEqual(api.waynodes.maximum, 2000);
        }

        /// <summary>
        /// Test writes an osm api file.
        /// </summary>
        [TestMethod]
        public void OsmApiSerializeTest()
        {
            OsmSharp.Osm.Core.Xml.v0_6.osm osm = new OsmSharp.Osm.Core.Xml.v0_6.osm();
            osm.api = new Osm.Core.Xml.v0_6.api();

            osm.api.area = new Osm.Core.Xml.v0_6.area();
            osm.api.area.maximumSpecified = true;
            osm.api.area.maximum = 0.25;

            osm.api.changesets = new Osm.Core.Xml.v0_6.changesets();
            osm.api.changesets.maximum_elementsSpecified = true;
            osm.api.changesets.maximum_elements = 50000;

            osm.api.timeout = new Osm.Core.Xml.v0_6.timeout();
            osm.api.timeout.secondsSpecified = true;
            osm.api.timeout.seconds = 300;

            osm.api.tracepoints = new Osm.Core.Xml.v0_6.tracepoints();
            osm.api.tracepoints.per_pageSpecified = true;
            osm.api.tracepoints.per_page = 5000;

            osm.api.version = new Osm.Core.Xml.v0_6.version();
            osm.api.version.maximumSpecified = true;
            osm.api.version.maximum = 0.6;
            osm.api.version.minimumSpecified = true;
            osm.api.version.minimum = 0.6;

            osm.api.waynodes = new Osm.Core.Xml.v0_6.waynodes();
            osm.api.waynodes.maximumSpecified = true;
            osm.api.waynodes.maximum = 2000;

            XmlSerializer capabilities_serializer = new XmlSerializer(typeof(
                OsmSharp.Osm.Core.Xml.v0_6.osm));

            Stream stream = new MemoryStream();
            capabilities_serializer.Serialize(stream, osm);

            stream.Seek(0, SeekOrigin.Begin);

            TextReader reader = new StreamReader(stream);
            string osm_string = reader.ReadToEnd();

            Assert.AreEqual(osm_string.Length, 100);
        }
    }
}
