using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Shared.Infrastructure.Common.Utilities
{
    /// <summary>
    /// Configuration Utility class for getting config value, based on generics/type
    /// </summary>
    public static class ConfigurationUtility
    {
        /// <summary>
        /// Gets the configuration value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configKey">The config key.</param>
        /// <param name="mustNotBeBlank">if set to <c>true</c> [must not be blank].</param>
        /// <returns></returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">
        /// Configuration key  + configKey +  is NOT defined in Configuration file.
        /// or
        /// Configuration key  + configKey +  is defined with BLANK value in Configuration file.
        /// or
        /// Configuration key  + configKey +  has INVALID value in Configuration file.
        /// </exception>
        public static T GetValue<T>(string configKey, bool mustNotBeBlank = true)
        {
            var configValue = ConfigurationManager.AppSettings[configKey];

            if (configValue == null)
            {
                //// Throw exception if config. setting is not found.
                throw new ConfigurationErrorsException("Configuration key " + configKey + " is NOT defined in Configuration file.");
            }

            if (mustNotBeBlank && string.IsNullOrWhiteSpace(configValue))
            {
                //// Throw exception if config. setting is found but its value is blank.
                throw new ConfigurationErrorsException("Configuration key " + configKey + " is defined with BLANK value in Configuration file.");
            }

            T result;

            try
            {
                result = (T)Convert.ChangeType(configValue, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                //// Throw exception if config. setting's value cannot be converted to desired data type.
                throw new ConfigurationErrorsException("Configuration key " + configKey + " has INVALID value in Configuration file.", ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the configuration value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configKey">The config key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration key  + configKey +  has INVALID value in Configuration file.</exception>
        public static T GetValue<T>(string configKey, T defaultValue)
        {
            var configValue = ConfigurationManager.AppSettings[configKey];
            T result;

            if (configValue == null)
            {
                return defaultValue;
            }

            try
            {
                result = (T)Convert.ChangeType(configValue, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                //// Throw exception if config. setting's value cannot be converted to desired data type.
                throw new ConfigurationErrorsException("Configuration key " + configKey + " has INVALID value in Configuration file.", ex);
            }

            return result;
        }
    }
}
