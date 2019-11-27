using BLL;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAp2Test
{
    [TestClass]
    public class PesadasTest
    {
        [TestMethod]
        public void Guardar()
        {
            Pesadas pesadas = new Pesadas()
            {
                ProductorId = 2,
                FactoriaId = 2,
                PrecioFanega = 2400,
                Fecha = DateTime.Now,
                TotalPagar = 1000,
                TotalSacos = 1,
                Humedad = 142,
                TotalKiloGramos = 100,
                UsuarioId = 1,
                EmpresaId = 1
            };
            pesadas.AgregarDetalle(new PesadasDetalle(0, pesadas.PesadaId, 2, "Prueba", 100, 2));
            RepositorioPesadas repositorio = new RepositorioPesadas();
            Assert.IsTrue(repositorio.Guardar(pesadas));
        }
        [TestMethod]
        public void Modificar()
        {
            RepositorioPesadas repositorio = new RepositorioPesadas();
            Pesadas pesadas = repositorio.Buscar(1);
            pesadas.AgregarDetalle(new PesadasDetalle(0, pesadas.PesadaId, 2, "Prueba", 100, 2));
            
            Assert.IsTrue(repositorio.Modificar(pesadas));
        }
        [TestMethod]
        public void Buscar()
        {
            RepositorioPesadas repositorio = new RepositorioPesadas();  
            Assert.IsNotNull(repositorio.Buscar(1));
        }
        [TestMethod]
        public void Eliminar()
        {
            RepositorioPesadas repositorio = new RepositorioPesadas();
            Assert.IsTrue(repositorio.Eliminar(2));
        }
    }
}
