using OptikShop.Business.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Business.Service
{
    public interface IProductService
    {
        bool AddProduct(AddProductDto addProductDto);

        List<ListProductDto> GetProducts();

        EditProductDto GetProductById(int id);

        void EditProduct(EditProductDto editProductDto);

        void DeleteProduct(int id);

        List<ListProductDto> GetProductsByCategoryId(int? categoryId);



    }
}
