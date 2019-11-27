using Entidades;
using Extensores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioUsuarios : RepositorioBase<Usuarios>
    {
        public static bool ValidarUsuario(Usuarios user)
        {
            bool paso = true;

            RepositorioBase<Usuarios> repositorio = new RepositorioBase<Usuarios>();
            List<Usuarios> usuario = new List<Entidades.Usuarios>();
            Expression<Func<Usuarios, bool>> filtro = x => true;
            var username = user.UserName.EliminarEspaciosEnBlanco();
            filtro = x => x.UserName.Equals(username);
            usuario = repositorio.GetList(filtro);


            if (user.UsuarioId == 0)
            {
                if (usuario.Exists(x => username.Trim().Equals(username.Trim())))
                    paso = false;
            }
            else
            {
                if (user.UsuarioId != 0)
                {
                    Usuarios UsuarioComprobar = new RepositorioBase<Entidades.Usuarios>().Buscar(user.UsuarioId);
                    if (!UsuarioComprobar.UserName.Trim().Equals(user.UserName.Trim()))
                    {
                        if (usuario.Exists(x => username.Trim().Equals(username.Trim())))
                            paso = false;
                    }
                }

            }
            repositorio.Dispose();
            return paso;
        }
        public static string SHA1(object password)
        {
            using (System.Security.Cryptography.SHA1Managed SHa1 = new System.Security.Cryptography.SHA1Managed())
            {
                var hash = SHa1.ComputeHash(Encoding.UTF8.GetBytes(password.ToString()));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte item in hash)
                {
                    sb.Append(item.ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public override List<Usuarios> GetList(Expression<Func<Usuarios, bool>> expression)
        {
            return base.GetList(expression);
        }
        public static bool Autenticar(string UserName, string Password)
        {
            bool paso = false;
            RepositorioUsuarios repositorio = new RepositorioUsuarios();
            List<Usuarios> lista = repositorio.GetList(x => true);
            foreach (var item in lista)
            {
                if (paso)
                    break;
                paso = item.Password.Equals(SHA1(Password)) && item.UserName.Equals(UserName);
            }
            return paso;
        }
        public static Usuarios GetUser(string Username)
        {
            Usuarios usuarios = new Usuarios();
            Username = Username.EliminarEspaciosEnBlanco();
            RepositorioUsuarios repositorio = new RepositorioUsuarios();
            List<Usuarios> lista = repositorio.GetList(x=>(x.UserName.Equals(Username)));
            return lista.FirstOrDefault();
        }
        public static Empresas GetEmpresas(int id)
        {
            Empresas empresas = new Empresas();
            Usuarios usuarios = new Usuarios();
            RepositorioUsuarios repositorioUsuarios = new RepositorioUsuarios();
            RepositorioBase<Empresas> repositorio = new RepositorioBase<Empresas>();

            usuarios = repositorioUsuarios.Buscar(id);
            List<Empresas> lista = repositorio.GetList(x => x.EmpresaID == usuarios.Empresa);
            return lista.FirstOrDefault();
        }
    }
}
