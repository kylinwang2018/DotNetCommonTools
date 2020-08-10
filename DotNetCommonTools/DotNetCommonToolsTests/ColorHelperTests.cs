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
            string hexString = "#812DD3";

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

        [TestMethod()]
        public void RGB2HEXTestShort()
        {
            ColorHelper.ColorRGB testRGB;
            testRGB.r = 255;
            testRGB.g = 0;
            testRGB.b = 0;

            string result = ColorHelper.RGB2HEX(testRGB);

            Assert.AreEqual(result, "#F00");
        }

        [TestMethod()]
        public void RGB2HEXTestNormal()
        {
            ColorHelper.ColorRGB testRGB;
            testRGB.r = 129;
            testRGB.g = 45;
            testRGB.b = 211;

            string result = ColorHelper.RGB2HEX(testRGB);

            Assert.AreEqual(result, "#812DD3");
        }

        [TestMethod()]
        public void HSL2RGBTest()
        {
            ColorHelper.ColorRGB colorRGB = ColorHelper.HSL2RGB(0, 1.0, 0.5);

            ColorHelper.ColorRGB resultRGB;
            resultRGB.r = 255;
            resultRGB.g = 0;
            resultRGB.b = 0;

            Assert.AreEqual(colorRGB, resultRGB);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "H,S,L should in range of 0-1")]
        public void HSL2RGBInvalid()
        {
            ColorHelper.HSL2RGB(0, 2.0, 0.5);
        }

        [TestMethod()]
        public void RGB2HSLTest()
        {
            ColorHelper.ColorRGB testRGB;
            testRGB.r = 129;
            testRGB.g = 45;
            testRGB.b = 211;

            double h, s, l;
            ColorHelper.RGB2HSL(testRGB, out h, out s, out l);

            Assert.AreEqual(h, 0.7510,4);
            Assert.AreEqual(s, 0.654,3);
            Assert.AreEqual(l, 0.502,3);
        }

        [TestMethod()]
        public void RGB2HSL2RGBTest()
        {
            ColorHelper.ColorRGB testRGB;
            testRGB.r = 129;
            testRGB.g = 45;
            testRGB.b = 211;

            double h, s, l;
            ColorHelper.RGB2HSL(testRGB, out h, out s, out l);

            ColorHelper.ColorRGB colorRGB = ColorHelper.HSL2RGB(h, s, l);

            Assert.AreEqual(colorRGB, testRGB);
        }
    }
}