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
    public class FactoriaTest
    {
        [TestMethod]
        public void Guardar()
        {
            Factoria factoria = new Factoria
            {
                Nombre = "Anthony Barrera",
                Direccion = "Las Guaranas",
                Telefono = "829-935-9510",
                UsuarioId =1,
                EmpresaId= 1,
                Fecha = DateTime.Now
            };
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            Assert.IsTrue(repositorio.Guardar(factoria));
        }
        [TestMethod]
        public void Modificar()
        {
            Factoria factoria = new Factoria
            {
                FactoriaId = 1,
                Nombre = "Anthony Manuel Barrera",
                Direccion = "Las Guaranas",
                Telefono = "829-935-9510",
                UsuarioId = 1,
                EmpresaId = 1,
                Fecha = DateTime.Now
            };
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            Assert.IsTrue(repositorio.Modificar(factoria));
        }
        [TestMethod]
        public void Buscar()
        {
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            Assert.IsTrue(repositorio.Buscar(1) != null);
        }
        [TestMethod]
        public void Eliminar()
        {
            RepositorioBase<Factoria> repositorio = new RepositorioBase<Factoria>();
            Assert.IsTrue(repositorio.Eliminar(1));
        }
    }
}
