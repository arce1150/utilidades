using System; 
using System.ComponentModel;
using System.Configuration;
using System.IO; 

namespace utilidades.utils
{
    public class Settings
    {
        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting)) return default(T);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }

        public static string CadenaConexion
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            }
        }
        public static string RutaErrores
        {
            get
            {
                return ConfigurationManager.AppSettings["rutalogs"].ToString();
            }
        }
        public static string GetDirectory(string key)
        {
            var ruta = Settings.Get<string>("RutaCarpetaLogGeneral");
            Directory.CreateDirectory(ruta);
            return ruta;
        }
    }
}
