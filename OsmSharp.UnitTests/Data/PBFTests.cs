﻿//// OsmSharp - OpenStreetMap (OSM) SDK
//// Copyright (C) 2012 Abelshausen Ben
//// 
//// This file is part of OsmSharp.
//// 
//// OsmSharp is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 2 of the License, or
//// (at your option) any later version.
//// 
//// OsmSharp is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

//using NUnit.Framework;
//using System.Reflection;
//using OsmSharp.Osm;
//using OsmSharp.Osm.Data;
//using OsmSharp.Osm.Data.Core.Memory;
//using OsmSharp.Osm.Data.PBF.Processor;

//namespace OsmSharp.UnitTests.Data
//{
//    /// <summary>
//    /// Containts tests for the PBF format.
//    /// </summary>
//    [TestFixture]
//    public class PBFTests
//    {
//        /// <summary>
//        /// Tests reading PBF data from a Stream.
//        /// </summary>
//        [Test]
//        public void TestPBFReaderDefault()
//        {
//            this.TestReadPBF("OsmSharp.UnitTests.api.osm.pbf");
//        }

//        /// <summary>
//        /// Tests reading PBF data from a Stream.
//        /// </summary>
//        [Test]
//        public void TestPBFReaderNoMeta()
//        {
//            this.TestReadPBF("OsmSharp.UnitTests.api_omitmetadata_true.osm.pbf");
//        }

//        /// <summary>
//        /// Tests reading PBF data from a Stream.
//        /// </summary>
//        [Test]
//        public void TestPBFReaderNoCompress()
//        {
//            this.TestReadPBF("OsmSharp.UnitTests.api_compress_none.osm.pbf");
//        }

//        /// <summary>
//        /// Tests reading PBF data from a Stream.
//        /// </summary>
//        [Test]
//        public void TestPBFReaderUseDense()
//        {
//            this.TestReadPBF("OsmSharp.UnitTests.api_usedense_false.osm.pbf");
//        }

//        /// <summary>
//        /// Tests reading a PBF and see if the data is there.
//        /// </summary>
//        /// <param name="resource"></param>
//        private void TestReadPBF(string resource)
//        {
//            // create the pbf source from a pbf in the resources of this assembly.
//            var source = new PBFOsmStreamSource(
//                Assembly.GetExecutingAssembly().GetManifestResourceStream(resource));

//            // pull the data from the source into the memory data source.
//            var data = new MemoryDataSource();
//            data.PullFromSource(source);

//            // test the data.
//            this.TestData(data);
//        }

//        /// <summary>
//        /// Tests if the data present is the data from api.osm.
//        /// </summary>
//        private void TestData(IDataSourceReadOnly data)
//        {
//            // test what data is present in the datasource.
//            Node node291738780 = data.GetNode(291738780);
//            Assert.IsNotNull(node291738780);
//            Assert.AreEqual(291738780, node291738780.Id);

//            Node node1727654333 = data.GetNode(1727654333);
//            Assert.IsNotNull(node1727654333);
//            Assert.AreEqual(1727654333, node1727654333.Id);

//            Way way87281441 = data.GetWay(87281441);
//            Assert.IsNotNull(way87281441);
//            Assert.AreEqual(87281441, way87281441.Id);

//            Way way76705106 = data.GetWay(76705106);
//            Assert.IsNotNull(way76705106);
//            Assert.AreEqual(76705106, way76705106.Id);
//        }
//    }
//}