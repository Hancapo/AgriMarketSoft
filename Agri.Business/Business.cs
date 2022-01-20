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

        public int LoginProcess(string usuario, string contrasena)
        {
            string AccExistString = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}'";
            string AccExistsIsCustomer = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND tipo_usuario_idtipo_usuario = 2";
            string AccExistsIsAdmin = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND tipo_usuario_idtipo_usuario = 1";
            string AccExistWrongPwd = $"SELECT IDUSUARIO FROM USUARIO WHERE NOMBREUSUARIO = '{usuario}' AND password = '{contrasena}'";


            //0 -> La cuenta no existe.
            //1 -> La cuenta sí existe.
            //2 -> La cuenta existe pero la contraseña es incorrecta.
            //3 -> La cuenta existe pero corresponde a una cuenta de cliente.
            //4 -> La cuenta existe y corresponde a una cuenta de administrador.
            //5 -> La cuenta es de administrador y la contraseña es correcta.

            int LoginType;

            if (ctql.RunSqlExecuteScalar(AccExistString) == null)
            {
                LoginType = 0;
            }
            else
            {
                LoginType = 1;
                if (ctql.RunSqlExecuteScalar(AccExistsIsCustomer) != null)
                {
                    LoginType = 3;
                }
                else
                {
                    if (ctql.RunSqlExecuteScalar(AccExistsIsAdmin) != null)
                    {
                        LoginType = 4;

                        if (ctql.RunSqlExecuteScalar(AccExistWrongPwd) != null)
                        {
                            LoginType = 5;
                        }
                        else
                        {
                            LoginType = 2;
                        }
                    }
                }
            }

            return LoginType;
        }
    }
}
