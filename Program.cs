namespace ChatServer
{

    /// <summary> 
    /// Author:    Griffin Shannon and Matthew Schnitter
    /// Date:      4/11/2022 
    /// Course:    CS 3500, University of Utah, School of Computing 
    /// Copyright: CS 3500, Griffin Shannon, and Matthew Schnitter - This work may not be copied for use in Academic Coursework. 
    /// 
    /// I, Matthew Schnitter, and I, Griffin Shannon, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source.  All references used in the completion of the assignment are cited in my README file. 
    /// 
    /// File Contents 
    /// Startup for ChatServer
    ///  
    /// </summary>

    using ChatServerModel;
    using Communications;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using FileLogger;

    /// <summary>
    /// Startup. Uses dependencty injection for logger
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection(); // Create  new services
            ConfigureServices(services);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            var gui = serviceProvider.GetRequiredService<ChatServer>(); // Get the logger services for ChatServer via dependency injection
            Application.Run(gui);

        }

        /// <summary>
        /// Method to add services to the service collection.
        /// Adds logging as well as a provider for the CustomFileLogger, so services can
        /// provide it when necessary.
        /// </summary>
        /// <param name="services"> the service collection for the GUI </param>
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.AddDebug();
                configure.AddProvider(new CustomFileLogProvider()); // Adds a provider for the logger to services
                configure.AddEventLog();
                configure.SetMinimumLevel(LogLevel.Debug);
            });
            services.AddScoped<ChatServer>();
        }
    }
}