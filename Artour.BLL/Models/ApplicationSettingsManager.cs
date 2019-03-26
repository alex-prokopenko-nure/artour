using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Models
{
    public static class ApplicationSettingsManager<T> where T : ApplicationSettings
    {
        public static T Settings { get; private set; }

        public static void BindToObject(IConfiguration configuration)
        {

            var type = typeof(T);

            var constructor = type.GetConstructor(new Type[0]);

            Settings = constructor.Invoke(new object[0]) as T;

            configuration.GetSection("AppSettings").Bind(Settings);
        }
    }

    public class ApplicationSettings
    {
        public String ImageDirectory { get; set; }
    }
}
