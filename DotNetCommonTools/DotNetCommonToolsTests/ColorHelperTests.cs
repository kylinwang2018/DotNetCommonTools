using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetCommonTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCommonTools.Tests
{
    [TestClass()]
    public class ColorHelperTests
    {
        [TestMethod()]
        public void HEX2RGBTest1()
        {
            string hexString = "#000000";

            ColorHelper.ColorRGB colorRGB = ColorHelper.HEX2RGB(hexString);

            ColorHelper.ColorRGB resultRGB;
            resultRGB.r = 0;
            resultRGB.g = 0;
            resultRGB.b = 0;

            Assert.AreEqual(colorRGB, resultRGB);
        }

        [TestMethod()]
        public void HEX2RGBTest2()
        {
            string hexString = "#812dd3";

            ColorHelper.ColorRGB colorRGB = ColorHelper.HEX2RGB(hexString);

            ColorHelper.ColorRGB resultRGB;
            resultRGB.r = 129;
            resultRGB.g = 45;
            resultRGB.b = 211;

            Assert.AreEqual(colorRGB, resultRGB);
        }

        [TestMethod()]
        public void HEX2RGBTestShort()
        {
            string hexString = "#f00";

            ColorHelper.ColorRGB colorRGB = ColorHelper.HEX2RGB(hexString);

            ColorHelper.ColorRGB resultRGB;
            resultRGB.r = 255;
            resultRGB.g = 0;
            resultRGB.b = 0;

            Assert.AreEqual(colorRGB, resultRGB);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Invalid HEX value")]
        public void HEX2RGBTestInvalid()
        {
            string hexString = "kkk";
            ColorHelper.HEX2RGB(hexString);
        }
    }
}