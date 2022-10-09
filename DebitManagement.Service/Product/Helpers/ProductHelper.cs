using System.Net;
using DebitManagement.Base;
using DebitManagement.Service.Product.Dto;

namespace DebitManagement.Service.Product.Helpers;

public static class ProductHelper
{
    public static void CheckSentData(ProductDto productDto)
    {
        if (string.IsNullOrEmpty(productDto.ProductCode) || string.IsNullOrEmpty(productDto.ProductDescription) ||
            productDto.ProductTypeId == Guid.Empty)
        {
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "There are problems with sent data. Please check it and try again.");
        }
    }

    public static Core.Entities.Product UpdateModel(Core.Entities.Product product, ProductDto productDto)
    {
        product.ProductCode = productDto.ProductCode;
        product.ProductDescription = productDto.ProductDescription;
        product.Price = productDto.Price;
        product.ProductTypeId = productDto.ProductTypeId;

        return product;
    }
}