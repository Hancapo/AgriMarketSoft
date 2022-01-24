using Agri.Connect;
using Agri.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Region = Agri.Core.Region;

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

        public List<Categoria> GetCategoriaList()
        {

            List<Categoria> listaCategoria = new();

            string sqlcommand = "select * from categoria";


            foreach (DataRow dr in ctql.SqltoDataTable(sqlcommand).Rows)
            {
                Categoria categoria = new()
                {
                    IdCategoria = Convert.ToInt32(dr["idcategoria"]),
                    NombreCategoria = dr["nombre"].ToString()
                };
                listaCategoria.Add(categoria);
            }

            return listaCategoria;
        }

        public List<Proveedor> GetProveedoresList()
        {
            List<Proveedor> proveedores = new();

            string sqlcommand = "select * from proveedor";

            foreach (DataRow dr in ctql.SqltoDataTable(sqlcommand).Rows)
            {
                Proveedor proveedor = new()
                {
                    RutProveedor = dr["rutproveedor"].ToString(),
                    Nombre = dr["nombreproveedor"].ToString(),
                    Contacto = dr["contacto"].ToString(),
                    Latitud = Convert.ToSingle(dr["latitud"]),
                    Longitud = Convert.ToSingle(dr["longitud"])
                };

                proveedores.Add(proveedor);
            }

            return proveedores;

        }

        public List<Region> GetRegionesList()
        {
            List<Region> listaRegion = new();

            string sqlcommand = "select * from region";


            foreach (DataRow dr in ctql.SqltoDataTable(sqlcommand).Rows)
            {
                Region region = new()
                {
                    IdRegion = Convert.ToInt32(dr["idregion"]),
                    NombreRegion = dr["nombreregion"].ToString()
                };
                listaRegion.Add(region);
            }

            return listaRegion;

        }

        
    }
}

