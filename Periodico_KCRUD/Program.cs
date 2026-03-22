using Periodico_KCRUD.Vistas; // <--- IMPORTANTE: Para que reconozca tus carpetas

namespace Periodico_KCRUD
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // CAMBIO AQUÍ: En lugar de Form1, abrimos el Home.
            // Le pasamos 'false' para que entre como Lector y salte el Timer.
            Application.Run(new FormHome(false));
        }
    }
}