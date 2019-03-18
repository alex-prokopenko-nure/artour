using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Configurations
{
    public class FileOperationFilter : IOperationFilter
    {

        private readonly HashSet<String> _formFileFields;

        public FileOperationFilter()
        {
            var type = typeof(IFormFile);
            var fields = type.GetProperties()
                                .Select(x => x.Name)
                                .ToList();

            _formFileFields = new HashSet<string>(fields);
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ParameterDescriptions.Any(x => x.ModelMetadata != null && x.ModelMetadata.ContainerType == typeof(IFormFile)))
            {
                var paramtersToRemove = operation.Parameters.Where(x => _formFileFields.Contains(x.Name))
                                                            .ToList();
                foreach (var parameter in paramtersToRemove)
                {
                    operation.Parameters.Remove(parameter);
                }

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "fileToSave", // must match parameter name from controller method
                    In = "formData",
                    Description = "Upload file.",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("application/form-data");
            }
        }
    }
}
