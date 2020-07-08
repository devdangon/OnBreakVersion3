using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnBreak.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio.Tests
{
    [TestClass()]
    public class ContratoTests
    {
        [TestMethod()]
        public void ReadRut_IsEmpty()
        {
            //var rut = "12345678";//Rut bueno
            var rut = "123";

            //var esperado = new List<Contrato>();//
            var esperado = 0;
            

            // Act
            var act = new Contrato().ReadRut(rut);

            // Assert
            Assert.AreEqual(esperado, act.Count);
            
        }

        public void ReadRut_IsFind()
        {
            var rut = "12345678";//Rut bueno
            //var rut = "123";

            //var esperado = new List<Contrato>();//
            var esperado = new Contrato();


            // Act
            var act = new Contrato().ReadRut(rut);

            // Assert
            Assert.AreSame(esperado, act);

        }
    }
}