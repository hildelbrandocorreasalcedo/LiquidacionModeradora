using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LiquidacionSubsidiada : LiquidacionCuotaModeradora
    {
        public LiquidacionSubsidiada(int numerodeLiquidacion, string identificacion, decimal valordeServicio) :
            base(numerodeLiquidacion, identificacion, "subsidiada", 0, valordeServicio)
        {
        }
        public override void EstablecerTarifa()
        {
            Tarifa = (decimal)0.05;

        }
        public override void EstablecerTopemaximo()
        {
            TopeMaximo = 200000;
        }
    }
}
