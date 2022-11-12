using System.Net;
using DebitManagement.Base;
using DebitManagement.Repository.Migrations;
using DebitManagement.Service.Debit.Dto;

namespace DebitManagement.Service.Debit.Helpers;

public static class DebitHelper
{
    public static void CheckSentData(DebitCreateUpdateDto dto)
    {
        if (dto.ProductId == Guid.Empty || dto.UserId == Guid.Empty ||
            dto.Quantity == 0 || dto.ReturnDate <= DateTime.Today)
        {
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "There are problems with sent data. Please check it and try again.");
        }
    }

    public static Core.Entities.Debit UpdateModel(Core.Entities.Debit debit, DebitCreateUpdateDto dto)
    {
        debit.ProductId = dto.ProductId;
        debit.UserId = dto.UserId;
        debit.ReturnDate = new DateTime(dto.ReturnDate.Ticks);
        debit.Quantity = dto.Quantity;
        debit.Status = "Active";

        return debit;
    }
}