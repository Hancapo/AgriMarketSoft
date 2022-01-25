using Agri.Connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agri.Core
{
    public class Usuario
    {
        ConnectSQL sa = new();
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string RutUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Contrasena { get; set; }
        public int IdTipo => Convert.ToInt32(sa.RunSqlExecuteScalar($"SELECT IdTipo FROM usuario where IdUsuario = {IdUsuario}"));
    }
}
