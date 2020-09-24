using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    public class LiquidacionCuotaModeradoraService
    {
        private LIquidacionCuotaModeradoraRepository liquidacionescuotasRepositorio;

        public LiquidacionCuotaModeradoraService()
        {
            liquidacionescuotasRepositorio = new LIquidacionCuotaModeradoraRepository();
        }
        public string Guardar(LiquidacionCuotaModeradora liquidacioncuotamoderadora)
        {
            try
            {
                if (liquidacionescuotasRepositorio.Buscar(liquidacioncuotamoderadora.NumerodeLiquidacion) == null)
                {
                    liquidacionescuotasRepositorio.Guardar(liquidacioncuotamoderadora);
                    return $"Los datos de la cuenta numero {liquidacioncuotamoderadora.NumerodeLiquidacion} han sido guardados correctamente";
                }
                return $"No es posible registrar la cuenta con numero {liquidacioncuotamoderadora.NumerodeLiquidacion}, porque ya se encuentra registrada";
            }
            catch (Exception E)
            {
                return "Error de lectura " + E.Message;
            }
        }
        public string Eliminar(int numerodeliquidacion)
        {
            try
            {
                LiquidacionCuotaModeradora liquidacioncuotamoderadora = liquidacionescuotasRepositorio.Buscar(numerodeliquidacion);
                if (liquidacioncuotamoderadora != null)
                {
                    liquidacionescuotasRepositorio.Eliminar(numerodeliquidacion);
                    Console.WriteLine($"Los datos de la liquidacion {numerodeliquidacion} han sido eliminados correctamente");
                    return null;
                }
                Console.WriteLine($"No es posible eliminar la liquidacion {numerodeliquidacion}, porque no se encuentra registrada");
                return null;
            }
            catch (Exception E)
            {
                Console.WriteLine("Error de lectura " + E.Message);
                return null;
            }
        }
        public void Modificar(LiquidacionCuotaModeradora liquidacioncuotamoderadora)
        {
            try
            {
                liquidacionescuotasRepositorio.Modificar(liquidacioncuotamoderadora);
            }
            catch (Exception E)
            {
                Console.WriteLine("Error de lectura" + E.Message);
            }

        }
        public void Consultar()
        {
            try
            {
                List<LiquidacionCuotaModeradora> liquidacionescuotasmoderadoras = liquidacionescuotasRepositorio.Consultar();
                if (liquidacionescuotasmoderadoras != null)
                {
                    ImprimirLiquidaciones(liquidacionescuotasmoderadoras);
                }
                else
                {
                    Console.WriteLine("No existen liquidaciones registradas en la lista");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine("Error de lectura " + E.Message);
            }
        }
        public void ImprimirLiquidaciones(List<LiquidacionCuotaModeradora> liquidacionesCuotaModeradora)
        {
            Console.WriteLine("{0,15}{1,17}{2,16}{3,12}{4,16}{5,17}", "Nro. Liquidacion", "Tipo_afiliacion", "identificacion", "salario", "Valor_Servicio", "Cuota_calculada");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            foreach (var item in liquidacionesCuotaModeradora)
            {
                Console.WriteLine("{0,15}{1,17}{2,16}{3,12}{4,16}{5,17}", item.NumerodeLiquidacion, item.TipodeAfiliacion, item.Identificacion, item.SalariodePaciente, item.ValordeServicio, item.CuotaModeradora);
            }


        }
        public LiquidacionCuotaModeradora Buscar(int numerodeliquidacion)
        {
            try
            {
                LiquidacionCuotaModeradora liquidacioncuotamoderadora = liquidacionescuotasRepositorio.Buscar(numerodeliquidacion);
                if (liquidacioncuotamoderadora == null)
                {
                    Console.WriteLine($"La liquidacion  {numerodeliquidacion} no se encuentra registrada");
                }
                return liquidacioncuotamoderadora;
            }
            catch (Exception E)
            {
                Console.WriteLine("Error de lectura " + E.Message);
                return null;
            }





        }


    }
}

