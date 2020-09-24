using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public abstract class LiquidacionCuotaModeradora
    {
        public int NumerodeLiquidacion { get; set; }
        public string Identificacion { get; set; }
        public string TipodeAfiliacion { get; set; }
        public decimal SalariodePaciente { get; set; }
        public decimal ValordeServicio { get; set; }
        public decimal Tarifa { get; set; }
        public decimal TopeMaximo { get; set; }
        public decimal CuotaModeradora { get; set; }

        public decimal SalarioMinimo = 890000;
        public abstract void EstablecerTarifa();
        public abstract void EstablecerTopemaximo();
        public LiquidacionCuotaModeradora(int numerodeLiquidacion, string identificacion, string tipodeAfilicion, decimal salariodePaciente, decimal valordeServicio)
        {
            NumerodeLiquidacion = numerodeLiquidacion;
            Identificacion = identificacion;
            TipodeAfiliacion = tipodeAfilicion;
            SalariodePaciente = salariodePaciente;
            ValordeServicio = valordeServicio;
        }
        public LiquidacionCuotaModeradora()
        {
        }

        public void CalcularCuota()
        {
            CuotaModeradora = ValordeServicio * Tarifa;
            if (CuotaModeradora > TopeMaximo)
            {
                CuotaModeradora = TopeMaximo;
            }

        }

        public override string ToString()
        {
            return $"{NumerodeLiquidacion};{Identificacion};{TipodeAfiliacion};{SalariodePaciente};{ValordeServicio};{Tarifa};{TopeMaximo};{CuotaModeradora}";
        }

    }
}