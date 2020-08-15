using AspNetCoreRequesLifeCycle.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreRequesLifeCycle.ModelBinder
{
    public class CSVModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(List<Product>))
            {
                return new CSVModelBinder();
            }

            return null;
        }
    }
}
