using System;
using System.Collections.Generic;
using System.Linq;
using utilidades.Entidades;
using utilidades.Servicios.Interfaces;
using utilidades.utils;

namespace utilidades.Servicios.Bussiness
{
    public class ProcedimientoProvider : ProcedimientoSvc
    {
        private readonly string _conexion = Settings.CadenaConexion;
        public override List<ProcedimientoBe> ListaProcedimiento()
        { 
            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(_conexion))
            {
                
                try
                {
                    cn.Open();
                    string query = "Select BaseDatos,Esquema,Procedimiento,Tipo,Texto,acceso,FechaCreacion,fechamodificacion ";
                    query += " from( select convert(varchar,r.SPECIFIC_CATALOG) as BaseDatos,convert(varchar, r.SPECIFIC_SCHEMA) as Esquema";
                    query += ",convert(varchar, r.SPECIFIC_NAME) as Procedimiento,convert(varchar, r.ROUTINE_TYPE) as Tipo, convert(text, r.ROUTINE_DEFINITION) as Texto ";
                    query += ",convert(varchar, r.SQL_DATA_ACCESS) as acceso, convert(varchar, r.CREATED, 103) as FechaCreacion, convert(varchar, r.LAST_ALTERED, 103) as fechamodificacion ";
                    query += " from INFORMATION_SCHEMA.ROUTINES r where r.ROUTINE_TYPE in ('PROCEDURE', 'FUNCTION') ) as tblresult;"; 
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader();
                    List<ProcedimientoBe> lista = null; 
                    if (dr != null)
                    {
                        lista = new List<ProcedimientoBe>();
                        ProcedimientoBe objProc = null;
                        while (dr.Read())
                        {
                            objProc = new ProcedimientoBe();
                            objProc.BaseDatos= dr["BaseDatos"] == DBNull.Value ? default(string) : (string)dr["BaseDatos"];
                            objProc.Esquema = dr["Esquema"] == DBNull.Value ? default(string) : (string)dr["Esquema"];
                            objProc.Procedimiento = dr["Procedimiento"] == DBNull.Value ? default(string) : (string)dr["Procedimiento"];
                            objProc.Tipo = dr["Tipo"] == DBNull.Value ? default(string) : (string)dr["Tipo"];
                            objProc.Texto = dr["Texto"] == DBNull.Value ? default(string) : (string)dr["Texto"];
                            objProc.FechaCreacion = dr["FechaCreacion"] == DBNull.Value ? default(string) : (string)dr["FechaCreacion"]; 
                            lista.Add(objProc);
                        }
                        dr.Close();
                    }
                    return lista;
                }
                catch(Exception ex)
                {
                    Log.GrabarLog(ex.Message,ex.StackTrace);
                    throw ex;
                }
            } 
        }
    }
}
