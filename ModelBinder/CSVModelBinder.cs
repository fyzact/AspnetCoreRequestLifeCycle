using AspNetCoreRequesLifeCycle.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreRequesLifeCycle.ModelBinder
{
    public class CSVModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var rawCSV = bindingContext.ValueProvider.GetValue("csv").ToString();

            var orderListCSV = rawCSV.Split(Environment.NewLine.ToCharArray());

            var orderList = new List<Product>();

            foreach (var item in orderListCSV)
            {
                var orderCSV = item.Split(",");
                Product order = new Product()
                {
                    ProductName = orderCSV[0],
                    Count = Convert.ToInt32(orderCSV[1]),
                    Description = orderCSV[2].ToString()
                };
                orderList.Add(order);
            }

            bindingContext.Result = ModelBindingResult.Success(orderList);


            return Task.CompletedTask;
        }
    }
}
