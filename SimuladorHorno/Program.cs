using System;

internal class Program
{
    class Horno
    {
        private static Random random = new Random();
        private static int temperatura = 0;
        private static string material;
        private static int incrementoTemperatura;
        private static bool hornoEncendido = true;
        private static bool hornoApagandose = false;

        static void Main(string[] args)
        {
            Console.Title = "Simulador de Horno";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            MostrarMensaje("Bienvenido al Simulador de Horno", ConsoleColor.Green);
            SeleccionarMaterial();
            EncenderHorno();
        }

        private static void MostrarMensaje(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        private static void SeleccionarMaterial()
        {
            MostrarMensaje("Seleccione el material para encender el horno:", ConsoleColor.Yellow);
            MostrarMensaje("1. Madera", ConsoleColor.Cyan);
            MostrarMensaje("2. Carbón", ConsoleColor.Cyan);
            MostrarMensaje("3. Paja", ConsoleColor.Cyan);

            int eleccion;
            while (!int.TryParse(Console.ReadLine(), out eleccion) || eleccion < 1 || eleccion > 3)
            {
                MostrarMensaje("Opción no válida. Por favor, seleccione 1, 2 o 3.", ConsoleColor.Red);
            }

            switch (eleccion)
            {
                case 1:
                    material = "Madera";
                    incrementoTemperatura = 5;
                    break;
                case 2:
                    material = "Carbón";
                    incrementoTemperatura = 10;
                    break;
                case 3:
                    material = "Paja";
                    incrementoTemperatura = 3;
                    break;
            }

            MostrarMensaje($"Has seleccionado {material}. El horno se está encendiendo...", ConsoleColor.Green);
        }

        private static void EncenderHorno()
        {
            while (hornoEncendido)
            {
                System.Threading.Thread.Sleep(500); // Esperar 0.5 segundos

                if (hornoApagandose)
                {
                    DisminuirTemperatura();
                    if (temperatura <= 0)
                    {
                        MostrarMensaje("El horno se ha apagado completamente.", ConsoleColor.Blue);
                        hornoEncendido = false;
                    }
                }
                else
                {
                    AumentarTemperatura();

                    if (temperatura >= 100)
                    {
                        MostrarMensaje("El horno ha alcanzado su temperatura limite. Deseas agregar mas material? (s/n)", ConsoleColor.Magenta);
                        string respuesta = Console.ReadLine();
                        if (respuesta.ToLower() == "s")
                        {
                            AgregarMaterial();
                        }
                        else
                        {
                            hornoApagandose = true;
                            MostrarMensaje("La temperatura esta disminuyendo...", ConsoleColor.DarkYellow);
                        }
                    }
                }
            }
        }

        private static void AumentarTemperatura()
        {
            int aumentoAleatorio = random.Next(1, incrementoTemperatura);
            temperatura += aumentoAleatorio;
            MostrarMensaje($"Temperatura del horno es: {temperatura}°C", ConsoleColor.White);
        }

        private static void DisminuirTemperatura()
        {
            int disminucionAleatoria = random.Next(1, incrementoTemperatura);
            temperatura -= disminucionAleatoria;
            if (temperatura < 0) temperatura = 0;
            MostrarMensaje($"Temperatura del horno es: {temperatura}°C", ConsoleColor.Gray);
        }

        private static void AgregarMaterial()
        {
            MostrarMensaje("Agregando más material...", ConsoleColor.Green);
            temperatura += incrementoTemperatura;
        }
    }
}