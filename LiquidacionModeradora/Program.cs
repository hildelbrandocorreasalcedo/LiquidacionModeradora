using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using BLL;

namespace LiquidacionModeradora
{
    public class Program
    {
        public static LiquidacionCuotaModeradoraService liquidacionescuotasService = new LiquidacionCuotaModeradoraService();

        public Program()
        {
            liquidacionescuotasService = new LiquidacionCuotaModeradoraService();


        }

        static string mensaje;


        static void Main(string[] args)
        {

            int opcion = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("                  Menu De Liquidaciones                ");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("1. Registrar Liquidacioncuotamoderadora");
                Console.WriteLine("2. Eliminar Liquidacioncuotamoderadora");
                Console.WriteLine("3. Buscar Liquidacioncuotamoderadora ");
                Console.WriteLine("4. Modificar valor del servicio de una liquidacioncuotamoderadora");
                Console.WriteLine("5. Ver listado de liquidaciones de cuotas");
                Console.WriteLine("0. Salir de la aplicacion\n");
                Console.WriteLine("Digite su opcion: ");
                opcion = ValidarNumeros("Error, debe ingresar una opcion valida ", 0, 5);
                EjecutarOpcion(opcion);
            } while (opcion != 0);
        }
        public static void EjecutarOpcion(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    Registrar();
                    break;
                case 2:
                    Eliminar();
                    break;
                case 3:
                    Buscar();
                    break;
                case 4:
                    Modificar();
                    break;
                case 5:
                    ConsultarListado();
                    break;
                case 0:
                    break;
            }
        }

        public static void Registrar()
        {
            string cadena;
            do
            {
                Console.Clear();
                LiquidacionCuotaModeradora liquidacionCuotaModeradora = RecolectarDatos();
                liquidacionCuotaModeradora.EstablecerTarifa();
                liquidacionCuotaModeradora.EstablecerTopemaximo();
                liquidacionCuotaModeradora.CalcularCuota();
                mensaje = liquidacionescuotasService.Guardar(liquidacionCuotaModeradora);
                Console.WriteLine($"{mensaje}");
                Console.WriteLine("El valor de la cuota moderadora es: {0}", liquidacionCuotaModeradora.CuotaModeradora);
                Console.WriteLine("¿Desea registrar otra liquidación de cuota ? S/N");
                cadena = ValidarLetras("Error, tiene que ingresar S o N", "S", "N");
            } while (cadena == "S");
        }
        public static LiquidacionCuotaModeradora RecolectarDatos()
        {
            LiquidacionCuotaModeradora liquidacionCuotaModeradora;
            Console.WriteLine("¿Que tipo de afiliacion desea ingresar? liquidacion Contributiva ->(C)  liquidacion Subsidiada->(S)");
            string TipodeAfiliacion = ValidarLetras("Error, debe ingresar C o S", "C", "S");
            Console.WriteLine("Ingrese numero de liquidación de cuota :");
            int NumerodeLiquidacion = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el numero de identificacion del paciente:");
            string Identificacion = Console.ReadLine();
            decimal SalariodePaciente;
            Console.WriteLine("Ingrese el valor del servicio del hospital:");
            decimal ValordeServicio = decimal.Parse(Console.ReadLine());
            if (TipodeAfiliacion == "C")
            {
                Console.WriteLine("Ingrese el valor del salario  del paciente:");
                SalariodePaciente = decimal.Parse(Console.ReadLine());
                liquidacionCuotaModeradora = new LiquidacionContributiva(NumerodeLiquidacion, Identificacion, SalariodePaciente, ValordeServicio);
            }
            else
            {
                liquidacionCuotaModeradora = new LiquidacionSubsidiada(NumerodeLiquidacion, Identificacion, ValordeServicio);
            }
            return liquidacionCuotaModeradora;
        }
        public static void Eliminar()
        {
            string cadena;
            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el numero de la liquidación a borrar :");
                int NumerodeLiquidacion = int.Parse(Console.ReadLine());
                mensaje = liquidacionescuotasService.Eliminar(NumerodeLiquidacion);
                Console.WriteLine($"{mensaje}");
                Console.WriteLine("¿Desea borrar otra liquidación? S/N");
                cadena = ValidarLetras("Error, debe ingresar S o N", "S", "N");
            } while (cadena == "S");
        }
        public static void Buscar()
        {
            string cadena;
            do
            {
                Console.Clear();
                List<LiquidacionCuotaModeradora> liquidacionesCuotasModeradoras = new List<LiquidacionCuotaModeradora>();
                Console.WriteLine("Ingrese el numero de la liquidación a buscar:");
                int NumerodeLiquidacion = int.Parse(Console.ReadLine());
                LiquidacionCuotaModeradora liquidacionCuotaModeradora = liquidacionescuotasService.Buscar(NumerodeLiquidacion);
                if (liquidacionCuotaModeradora != null)
                {
                    Console.WriteLine("Liquidación encontrada\n\n");
                    liquidacionesCuotasModeradoras.Add(liquidacionCuotaModeradora);
                    liquidacionescuotasService.ImprimirLiquidaciones(liquidacionesCuotasModeradoras);
                }

                Console.WriteLine("¿Desea buscar otra liquidación? S/N");

                cadena = ValidarLetras("Error,  ingrese S o N", "S", "N");

            } while (cadena == "S");
        }
        public static void Modificar()
        {
            string respuesta;
            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el numero de la liquidacion a modificar:");
                int NumerodeLiquidacion = int.Parse(Console.ReadLine());
                LiquidacionCuotaModeradora liquidacioncuotamoderadora = liquidacionescuotasService.Buscar(NumerodeLiquidacion);
                if (liquidacioncuotamoderadora != null)
                {
                    Console.WriteLine("Ingrese el nuevo valor del servicio de hospitalizacion:");
                    liquidacioncuotamoderadora.ValordeServicio = decimal.Parse(Console.ReadLine());
                    liquidacioncuotamoderadora.CalcularCuota();
                    liquidacionescuotasService.Modificar(liquidacioncuotamoderadora);
                    Console.WriteLine($"{mensaje}");
                    Console.WriteLine("El nuevo valor de la cuota moderadora es: {0}", liquidacioncuotamoderadora.CuotaModeradora);
                }
                Console.WriteLine("¿Desea editar otra liquidación? S/N");
                respuesta = ValidarLetras("Error, debe ingresar S o N", "S", "N");
            } while (respuesta == "S");
        }
        public static void ConsultarListado()
        {
            Console.Clear();
            liquidacionescuotasService.Consultar();
            Console.ReadKey();
        }
        public static int ValidarNumeros(string cadena, int Variable1, int Variable2)
        {
            int opcion;
            do
            {
                opcion = int.Parse(Console.ReadLine());
                if (opcion < Variable1 || opcion > Variable2)
                {
                    Console.WriteLine(cadena);
                    Console.ReadKey();
                }
            } while (opcion < Variable1 && opcion > Variable2);
            return opcion;
        }
        public static string ValidarLetras(string cadena, string Variable1, string Variable2)
        {
            string opcion;
            do
            {
                opcion = Console.ReadLine().ToUpper();
                if (opcion != Variable1 && opcion != Variable2)
                {
                    Console.WriteLine(mensaje + "\n");
                    Console.ReadKey();
                }
            } while (opcion != Variable1 && opcion != Variable2);
            return opcion;
        }
    }
}