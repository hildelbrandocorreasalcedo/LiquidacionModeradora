using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.IO;

namespace DAL
{
    public class LIquidacionCuotaModeradoraRepository

    {

        private string ruta = @"Liquidacioncuotamoderadoras.txt";
        public List<LiquidacionCuotaModeradora> liquidacionesCuotas;

        public LIquidacionCuotaModeradoraRepository()
        {
            liquidacionesCuotas = new List<LiquidacionCuotaModeradora>();
        }
        public void Guardar(LiquidacionCuotaModeradora liquidacioncuotamoderadora)

        {
            FileStream fileStream = new FileStream(ruta, FileMode.Append);
            StreamWriter stream = new StreamWriter(fileStream);
            stream.WriteLine(liquidacioncuotamoderadora.ToString());
            stream.Close();
            fileStream.Close();

        }




        public List<LiquidacionCuotaModeradora> Consultar()
        {
            liquidacionesCuotas.Clear();
            FileStream filestream = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(filestream);
            string linea = string.Empty;

            while ((linea = reader.ReadLine()) != null)
            {

                LiquidacionCuotaModeradora liquidacioncuotamoderadora = MapearLiquidacionCuotaModeradora(linea);
                liquidacionesCuotas.Add(liquidacioncuotamoderadora);
            }
            filestream.Close();
            reader.Close();
            return liquidacionesCuotas;



        }

        public LiquidacionCuotaModeradora MapearLiquidacionCuotaModeradora(string linea)
        {
            string[] datos = linea.Split(';');
            int NumerodeLiquidacion = int.Parse(datos[0]);
            string Identificacion = datos[1];
            string TipodeAfiliacion = datos[2];
            decimal SalariodePaciente = decimal.Parse(datos[3]);
            decimal ValordeServicio = decimal.Parse(datos[4]);
            decimal Cuotareal = decimal.Parse(datos[7]);
            decimal Tarifa = decimal.Parse(datos[5]);
            decimal TopeMaximo = decimal.Parse(datos[6]);
            if (datos[2] == "contributiva")
            {
                LiquidacionCuotaModeradora liquidacioncuotamoderadoracontributiva = new LiquidacionContributiva(NumerodeLiquidacion, Identificacion, SalariodePaciente, ValordeServicio);
                liquidacioncuotamoderadoracontributiva.Tarifa = Tarifa;
                liquidacioncuotamoderadoracontributiva.TopeMaximo = TopeMaximo;
                liquidacioncuotamoderadoracontributiva.CuotaModeradora = Cuotareal;

                return liquidacioncuotamoderadoracontributiva;
            }

            else
            {
                LiquidacionCuotaModeradora liquidacioncuotamoderadorasubsidiada = new LiquidacionSubsidiada(NumerodeLiquidacion, Identificacion, ValordeServicio);
                liquidacioncuotamoderadorasubsidiada.Tarifa = Tarifa;
                liquidacioncuotamoderadorasubsidiada.TopeMaximo = TopeMaximo;
                liquidacioncuotamoderadorasubsidiada.CuotaModeradora = Cuotareal;
                return liquidacioncuotamoderadorasubsidiada;

            }

        }
        public void Eliminar(int Numerodeliquidacion)
        {
            liquidacionesCuotas.Clear();
            liquidacionesCuotas = Consultar();
            FileStream fileStream = new FileStream(ruta, FileMode.Create);
            fileStream.Close();
            foreach (var item in liquidacionesCuotas)
            {
                if (item.NumerodeLiquidacion != Numerodeliquidacion)
                {
                    Guardar(item);
                }
            }

        }

        public LiquidacionCuotaModeradora Buscar(int numerodeliquidacion)
        {
            liquidacionesCuotas.Clear();
            liquidacionesCuotas = Consultar();

            foreach (var item in liquidacionesCuotas)
            {
                if (item.NumerodeLiquidacion.Equals(numerodeliquidacion))
                {
                    return item;
                }
            }
            return null;
        }

        public void Modificar(LiquidacionCuotaModeradora liquidacioncuotamoderadora)
        {
            liquidacionesCuotas.Clear();
            liquidacionesCuotas = Consultar();
            FileStream fileStream = new FileStream(ruta, FileMode.Create);
            fileStream.Close();
            foreach (var item in liquidacionesCuotas)
            {
                if (item.NumerodeLiquidacion != liquidacioncuotamoderadora.NumerodeLiquidacion)
                {
                    Guardar(item);
                }
                else
                {
                    Guardar(liquidacioncuotamoderadora);
                }
            }

        }





    }
}