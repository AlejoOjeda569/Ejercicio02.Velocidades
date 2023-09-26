using ConsoleTables;
using System;

namespace Ejercicio02.Velocidades;
internal class Program
{
    const double Max_velocidad=300;
    const double Min_velocidad=100;
    static void Main(string[] args)
    {
        double[] velocidad = new double[10];
        bool seguir = true;
        do
        {
            MostarMenu();
            int opciones = PedirIntEnRango("selecciones: ",1,6);
           
            switch (opciones)
            {
                case 1:
                    GenerarVelocidades(velocidad); 
                   break;
                case 2:
                    ModificarDatos(velocidad);
                    break;
                case 3:
                    ListarVelocidad(velocidad);
                    break;
                case 4:
                    DatosEstadisticos(velocidad);
                        break;
                case 5:
                    MostrarPromedio(velocidad);
                    break;
                case 6:
                    seguir = false;
                    break;

            }

        } while (seguir);
        TareaFinalizda("Fin de la Aplicacion");
    }

    private static void MostrarPromedio(double[] velocidad)
    {

        var promedio = velocidad.Average();
        Console.Clear();
        Console.WriteLine("Mostrar Inferiores y Superior al Promedio");
        Console.WriteLine($"Promedio={promedio.ToString("N2")}");
        var tabla = new ConsoleTable("Kilometros","Valor de los promedios");
        foreach (var velocidadEnArray in velocidad)
        {
            if (velocidadEnArray < promedio)
            { 

                tabla.AddRow(velocidadEnArray,"Inferior al promedio");
            }
            else
            {
                tabla.AddRow(velocidadEnArray, "Superior al promedio");
            }
        }
        Console.WriteLine(tabla.ToString());
        TareaFinalizda("Valores mostrados");
       
    }

    private static void DatosEstadisticos(double[] velocidad)
    {
        ListarVelocidad(velocidad);
        var maxVelocidad = velocidad.Max();
        var minVelocidad = velocidad.Min();
        var promedio = velocidad.Average();
        Console.WriteLine($"Mayor temperatura={maxVelocidad}");
        Console.WriteLine($"Menor temperatura={minVelocidad}");
        Console.WriteLine($"Promedio temperatura={promedio.ToString("N2")}");
        Console.WriteLine();
        TareaFinalizda("Datos Estadísticos...");
    }

    private static void ModificarDatos(double[] velocidad)
    {
        do
        {
            Console.Clear();
            Console.Write("Modificación de datos");
            ListarVelocidad(velocidad);

            var index =PedirDoubleEnRango("Ingrese el indice para modificar:", 1, velocidad.Length);
            Console.WriteLine($"Valor anterior:{velocidad[(int)(index - 1)]} ");
            double nuevaVelocidad;
            do
            {
                nuevaVelocidad =PedirDoubleEnRango("Ingrese una nueva temperatura:", Min_velocidad, Max_velocidad);

                if (Existe(nuevaVelocidad, velocidad))
                {
                    Console.WriteLine("La velocidad ya existe");
                }
                else
                {
                    break;
                }

            } while (true);
            velocidad[(int)(index - 1)] = nuevaVelocidad;
            var siguirModificando =PedirCharEnRango("Desea seguir Modificando S/N", 's', 'n');
        } while (true);
        TareaFinalizda("Modificacion finalizada");
    }

    private static object PedirCharEnRango(string mensaje, char char1, char char2)
    {

        char cX;
        do
        {

            Console.Write(mensaje);
            var tecla = Console.ReadKey();
            if (tecla.KeyChar.ToString().ToUpper() == char1.ToString().ToUpper()
                || tecla.KeyChar.ToString().ToUpper() == char2.ToString().ToUpper())
            {
                cX = tecla.KeyChar;
                break;
            }
            Console.WriteLine("No ingresó nada por la consola");
        } while (true);
        return cX.ToString().ToUpper();
    }

    private static void ListarVelocidad(double[] velocidad)
    {

        Console.Clear();
        Console.WriteLine("Listados de Velocidades");
        var tabla = new ConsoleTable("Kilometros", "Millas");
        foreach (double VelocidadArray in velocidad)
        {
            var Millas = convertToMillas(VelocidadArray);
            tabla.AddRow(VelocidadArray,Millas);
        }
        Console.WriteLine(tabla.ToString());
        TareaFinalizda("Listado finalizado");
    }

    private static object convertToMillas(double velocidadArray) =>velocidadArray * 0.621371; 
   
    private static void GenerarVelocidades(double[] velocidad)
    {
        Console.Clear();
       
        Console.WriteLine("Ingreso de Temperaturas");
        for (int i = 0; i < velocidad.Length; i++)
        {
            double VelocidadIngresad;
            do
            {
                VelocidadIngresad=PedirDoubleEnRango("Ingrese las temperatuas:", Min_velocidad, Max_velocidad);


                if (Existe(VelocidadIngresad, velocidad))
                {
                    Console.WriteLine("Esa temperatura ya existe");
                }
                else
                {
                    break;
                }

            } while (true);
            velocidad[i] = VelocidadIngresad;
        }
        
    }

    private static bool Existe(double velocidadIngresad, double[] velocidad)
    {
        foreach (double VelocidadArray in velocidad)
        {
            if (velocidadIngresad == VelocidadArray)
            {
                return true;
            }
            continue;
        }
        return false;
    }

    private static double PedirDoubleEnRango(string mensaje, double valorMenor, double valorMayor)
    {
        bool error = true;
        double valorDouble;
        string? cX;
        do
        {
            cX = PedirString(mensaje);
            if (!double.TryParse(cX, out valorDouble))
            {
                Console.WriteLine("Error al intentar ingresar un valor entero");
            }
            else if (valorDouble < valorMenor || valorDouble > valorMayor)
            {
                Console.WriteLine($"ERROR valor fuera del rango permitido {valorMenor} y {valorMayor}");
            }
            else
            {
                error = false;
            }
        } while (error);

        return valorDouble;
    }

  private static void TareaFinalizda(string mensaje)
    {
        Console.WriteLine($" { mensaje} ENTER para continuar");

        Console.ReadLine();
    }

    private static void MostarMenu()
    {
        Console.Clear();
        Console.WriteLine("1-Ingresar datos");
        Console.WriteLine("2-Modificar datos");
        Console.WriteLine("3-Listar velocidades con sus equivalentes");
        Console.WriteLine("4-Datos estadisticos");
        Console.WriteLine("5-Ver superiores e inferiores al promedio");
        Console.WriteLine("6-Salir");

    }
    private static int PedirInt(string mensaje)
    {
        int nro;
        string cX;
        do
        {
            cX = PedirString(mensaje);
            if (int.TryParse(cX, out nro))
            {
                break;
            }
            Console.WriteLine("Número no válido");
        } while (true);
        return nro;
    }
    private static string PedirString(string mensaje)
    {
        string? cX;
        do
        {

            Console.Write(mensaje);
            cX = Console.ReadLine();
            if (!string.IsNullOrEmpty(cX) || !string.IsNullOrWhiteSpace(cX))
            {
                break;
            }
            Console.WriteLine("No ingresó nada por la consola");
        } while (true);
        return cX;
    }
    private static int PedirIntEnRango(string mensaje, int valorMenor, int valorMayor)
    {
        bool error = true;
        int valorInt;
        string? cX;
        do
        {
            cX =PedirString (mensaje);
            if (!int.TryParse(cX, out valorInt))
            {
                Console.WriteLine("Error al intentar ingresar un valor entero");
            }
            else if (valorInt < valorMenor || valorInt > valorMayor)
            {
                Console.WriteLine($"ERROR valor fuera del rango permitido {valorMenor} y {valorMayor}");
            }
            else
            {
                error = false;
            }
        } while (error);

        return valorInt;
    }




}

  

