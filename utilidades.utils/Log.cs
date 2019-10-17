using System; 
using System.Configuration;
using System.IO;
using System.Reflection;
namespace utilidades.utils
{
    public class Log
    {
        private readonly static string _ruta = Settings.CadenaConexion;
        public static void GrabarLog (string mensaje,string detalle )
        {
            //string ruta = ConfigurationManager.AppSettings["rutalogs"].ToString();
            BeLog log = new BeLog();
            log.FechaHora = DateTime.Now;
            log.MensajeError = mensaje;
            log.DetalleError = detalle;
            PropertyInfo[] propiedades = log.GetType().GetProperties();
            string archivoLog = string.Format("Log_{0}.txt", DateTime.Now.ToString("ddMMyyyy HHmmss"));
            string rutaArchivo = string.Concat(_ruta, archivoLog);
            using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
            {
                foreach (var item in propiedades)
                {
                    sw.WriteLine("{0}={1}", item.Name, item.GetValue(log, null));
                }
                sw.WriteLine(new string('-', 200));
            }
        }
    }
}
