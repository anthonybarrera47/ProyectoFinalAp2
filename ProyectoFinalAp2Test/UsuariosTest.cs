using System;
using BLL;
using Entidades;
using Enums;
using Herramientas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProyectoFinalAp2Test
{
    [TestClass]
    public class UsuariosTest
    {
        [TestMethod]
        public void Guardar()
        {
            Usuarios usuarios = new Usuarios
            {
                Nombre = "Anthony Barrera",
                Password = "1234",
                UserName = "root",
                TipoUsuario = TipoUsuario.UsuarioNormal,
                Fecha = DateTime.Now
            };
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            Assert.IsTrue(repositorio.Guardar(usuarios));
        }
        [TestMethod]
        public void Modificar()
        {
            Usuarios usuarios = new Usuarios
            {
                UsuarioId = 1,
                Nombre = "Anthony Manuel Barrera",
                Password = Utils.SHA1("1234"),
                UserName = "root",
                TipoUsuario = TipoUsuario.UsuarioNormal,
                Fecha = DateTime.Now
            };
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            Assert.IsTrue(repositorio.Modificar(usuarios));
        }
        [TestMethod]
        public void Buscar()
        {
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            Assert.IsTrue(repositorio.Buscar(1)!=null);
        }
        [TestMethod]
        public void Eliminar()
        {
            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            Assert.IsTrue(repositorio.Eliminar(1));
        }

    }
}
