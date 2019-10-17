using System;
using System.Collections.Generic;
using utilidades.Entidades;
namespace utilidades.Servicios.Interfaces
{
    public abstract partial class ProcedimientoSvc: ServiceProvider
    {
        public abstract List<ProcedimientoBe> ListaProcedimiento();
    }
}
