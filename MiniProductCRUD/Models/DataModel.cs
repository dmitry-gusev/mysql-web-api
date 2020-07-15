using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace MiniProductCRUD.Models
{
    public static class Extentions
    {
        public static IEnumerable<ProductViewModel> ToViewModels(this IEnumerable<Product> input)
        {
            var res = input.ToList().Select(x => (ProductViewModel) x);
            return res;
        }
    }

    /// <summary>
    /// Products view model
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert ProductViewModel to Product
        /// </summary>
        /// <param name="input">Item</param>
        public static implicit operator ProductViewModel(Product input)
        {
            if (input!=null)
            {
                return new ProductViewModel()
                {
                    Id = input.Id,
                    Name = input.Name
                };
            }
            return new ProductViewModel();
        }

        /// <summary>
        /// Convert Product to ProductViewModel
        /// </summary>
        /// <param name="input">Item</param>
        public static implicit operator Product(ProductViewModel input)
        {
            if (input!=null)
            {
                return new Product()
                {
                    Id = input.Id,
                    Name = input.Name
                };
            }
            return new Product();
        }

    }

    /// <summary>
    /// Web api operation result
    /// </summary>
    /// <typeparam name="T">Type of operation data</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// Operation result code
        /// </summary>
        public ResultCodes Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Operation data
        /// </summary>
        public T Data { get; set; }
    }

    
}
