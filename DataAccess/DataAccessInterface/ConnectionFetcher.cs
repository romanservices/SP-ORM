using System.Configuration;
namespace DataAccess.DataAccessInterface
{
    class ConnectionFetcher
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static string Get()
        {
            return ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Database"]].ToString();
        }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        public static string GetEnvironment(string environment)
        {
            return ConfigurationManager.ConnectionStrings[environment].ToString();
        }
    }
}
