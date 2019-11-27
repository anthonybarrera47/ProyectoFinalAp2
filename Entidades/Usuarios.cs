using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Usuarios
    {

        [Key]
        public int UsuarioId { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }  
        public TipoUsuario TipoUsuario { get; set; }
        public bool EsPropietarioEmpresa { get; set; }
        public int Empresa { get; set; }
        public DateTime Fecha { get; set; }

        public Usuarios()
        {
            UsuarioId = 0;
            UserName = string.Empty;
            Nombre = string.Empty;
            Password = string.Empty; 
            TipoUsuario = TipoUsuario.UsuarioNormal;
            EsPropietarioEmpresa = false;
            Empresa = 0;
            Fecha = DateTime.Now;
        }

        public Usuarios(int usuarioId, string userName, string nombre, string password, TipoUsuario tipoUsuario, bool esPropietarioEmpresa, int empresa, DateTime fecha)
        {
            UsuarioId = usuarioId;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            TipoUsuario = tipoUsuario;
            EsPropietarioEmpresa = esPropietarioEmpresa;
            Empresa = empresa;
            Fecha = fecha;
        }
    }
}
