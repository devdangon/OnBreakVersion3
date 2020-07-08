using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnBreak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Tests
{
    [TestClass()]
    public class ClienteTests
    {
        [TestMethod()]
        public void ValidarRutTest()
        {
            //Arrange
            var rut = "123";
            var resultadoEsperado = true;

            // Act
            var act = new Cliente().ValidarRut(rut);

            // Assert
            Assert.AreEqual(resultadoEsperado, act);
        }
    }
}