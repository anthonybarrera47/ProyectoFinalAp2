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
    public class TipoArrozTest
    {
        [TestMethod]
        public void Guardar()
        {
            TipoArroz TipoArroz = new TipoArroz
            {
                Descripcion = "Yocaju",
                Kilos = 0,
                UsuarioId = 1,
                EmpresaId = 1,
                Fecha = DateTime.Now
            };
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            Assert.IsTrue(repositorio.Guardar(TipoArroz));
        }
        [TestMethod]
        public void Modificar()
        {
            TipoArroz TipoArroz = new TipoArroz
            {
                TipoArrozId = 1,
                Descripcion = "Yocaju",
                Kilos = 0,
                UsuarioId = 1,
                EmpresaId = 1,
                Fecha = DateTime.Now
            };
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            Assert.IsTrue(repositorio.Modificar(TipoArroz));
        }
        [TestMethod]
        public void Buscar()
        {
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            Assert.IsTrue(repositorio.Buscar(1) != null);
        }
        [TestMethod]
        public void Eliminar()
        {
            RepositorioBase<TipoArroz> repositorio = new RepositorioBase<TipoArroz>();
            Assert.IsTrue(repositorio.Eliminar(1));
        }
    }
}
