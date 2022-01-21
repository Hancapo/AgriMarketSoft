using Agri.Connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agri.Business
{
    public class Business
    {
        private ConnectSQL ctql = new();

        public (int,int) LoginProcess(string correo, string contrasena)
        {
            string AccExistsString = $"SELECT IDUSUARIO FROM USUARIO WHERE CORREO = '{correo}'";
            string AccExistsIsCustomer = $"SELECT IDUSUARIO FROM USUARIO WHERE CORREO = '{correo}' AND idtipo = 1";
            string AccExistsIsProveedor = $"SELECT IDUSUARIO FROM USUARIO WHERE CORREO = '{correo}' AND idtipo = 2";
            string AccExistWrongPwd = $"SELECT IDUSUARIO FROM USUARIO WHERE CORREO = '{correo}' AND contrasena = '{contrasena}'";


            //0 -> La cuenta no existe.
            //1 -> La cuenta existe.
            //2 -> La contraseña es incorrecta.
            //3 -> Ingreso de sesión exitoso.
            

            //1 -> Cliente
            //2 -> Proveedor


            int LoginType = -1;
            int UserType = -1;


            if (ctql.RunSqlExecuteScalar(AccExistsString) != null)
            {
                LoginType = 1;

                if (ctql.RunSqlExecuteScalar(AccExistsIsCustomer) != null)
                {
                    UserType = 1;

                    if (ctql.RunSqlExecuteScalar(AccExistWrongPwd) != null)
                    {
                        LoginType = 3;
                    }
                    else
                    {
                        LoginType = 2;

                    }
                }

                else if (ctql.RunSqlExecuteScalar(AccExistsIsProveedor) != null)
                {
                    UserType = 2;

                    if (ctql.RunSqlExecuteScalar(AccExistWrongPwd) != null)
                    {
                        LoginType = 3;

                    }
                    else
                    {
                        LoginType = 2;
                    }

                }
            }
            else
            {
                LoginType = 0;
            }

            return (LoginType, UserType);
        }
    }
}
