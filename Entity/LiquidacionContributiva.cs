using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LiquidacionContributiva : LiquidacionCuotaModeradora
    {

        public LiquidacionContributiva(int numerodeLiquidacion, string identificacion, decimal salariodePaciente, decimal valordeServicio) :
            base(numerodeLiquidacion, identificacion, "contributiva", salariodePaciente, valordeServicio)
        {
        }
        public override void EstablecerTarifa()
        {
            if (SalariodePaciente < SalarioMinimo * 2)
            {
                Tarifa = (decimal)0.15;
            }
            else if (SalariodePaciente > SalarioMinimo * 5)
            {
                Tarifa = (decimal)0.25;
            }
            else
            {
                Tarifa = (decimal)0.20;
            }
            { }
        }
        public override void EstablecerTopemaximo()
        {
            if (SalariodePaciente < SalarioMinimo * 2)
            {
                TopeMaximo = 250000;
            }
            else if (SalariodePaciente > SalarioMinimo * 5)
            {

                TopeMaximo = 1500000;
            }
            else
            {

                TopeMaximo = 900000;
            }
        }
    }
}
