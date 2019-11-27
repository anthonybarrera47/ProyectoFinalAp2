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
    public class ProductoresTest
    {
        [TestMethod]
        public void Guardar()
        {
            Productores Productor = new Productores
            {
                Nombre = "Anthony Barrera",
                Telefono = "829-935-9510",
                Cedula = "056-0069782-4",
                FechaNacimiento = DateTime.Now,
                UsuarioId = 1,
                EmpresaId = 1,
                Fecha = DateTime.Now
            };
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            Assert.IsTrue(repositorio.Guardar(Productor));
        }
        [TestMethod]
        public void Modificar()
        {

            Productores Productor = new Productores
            {
                ProductorId = 1,
                Nombre = "Anthony Manuel Barrera",
                Telefono = "829-935-9510",
                Cedula = "056-0069782-4",
                FechaNacimiento = DateTime.Now,
                UsuarioId = 1,
                EmpresaId = 1,
                Fecha = DateTime.Now
            };
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            Assert.IsTrue(repositorio.Modificar(Productor));
        }
        [TestMethod]
        public void Buscar()
        {
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            Assert.IsTrue(repositorio.Buscar(1) != null);
        }
        [TestMethod]
        public void Eliminar()
        {
            RepositorioBase<Productores> repositorio = new RepositorioBase<Productores>();
            Assert.IsTrue(repositorio.Eliminar(1));
        }
    }
}
