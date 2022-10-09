using System.Net;
using DebitManagement.Base;
using DebitManagement.Service.ProductType.Dto;

namespace DebitManagement.Service.ProductType;

public static class ProductTypeHelper
{
    public static void CheckSentData(ProductTypeDto productTypeDto)
    {
        if (string.IsNullOrEmpty(productTypeDto.ProductTypeCode) || string.IsNullOrEmpty(productTypeDto.ProductTypeDescription))
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "There are problems with sent data. Please check it and try again.");
    }

    public static Core.Entities.ProductType UpdateModel(Core.Entities.ProductType productType,
        ProductTypeDto productTypeDto)
    {
        productType.ProductTypeCode = productTypeDto.ProductTypeCode;
        productType.ProductTypeDescription = productTypeDto.ProductTypeDescription;

        return productType;
    }
}