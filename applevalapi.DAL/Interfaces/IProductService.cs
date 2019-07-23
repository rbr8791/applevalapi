using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAllSortedByNameAndLikes();
        Product GetById(int id);
        Product Delete(int id, User user);
        Product FindByOrdinal(int id);
        IEnumerable<Product> FindByName(string productName);
        Product FindByOrdinalAsNoTracking(int id);
        Product Create(Product product, User user);
        Product Update(Product product, User user);
        Product LikeProductById(Product product, User user);
        Product ChangePriceById(Product product, User user);

        /*
         * 
         * 
         * 
         * 
        public int Id { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool Status { get; set; } 
         
        */
    }
}