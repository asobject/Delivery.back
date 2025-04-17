

using BuildingBlocks.Enums;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Mappings;

public static class OrderMapper
{

    public static OrderDTO MapOrder(Order order, Dictionary<int, string> addressLookup)
    {
        return new OrderDTO(
            OrderId: order.Id,
            Tracker: order.Tracker,
            CurrentPointId:order.CurrentPointId,
            SenderAddress: GetAddress(order.SenderDeliveryPoint, addressLookup),
            ReceiverAddress: GetAddress(order.ReceiverDeliveryPoint, addressLookup),
            Status: order.OrderStatus
        );
    }

    private static string GetAddress(DeliveryPoint point, Dictionary<int, string> addressLookup)
    {
        return point.Method switch
        {
            DeliveryMethod.CourierCall =>
                point.CustomPoint?.Address ?? "Адрес не указан",

            DeliveryMethod.PickupPoint when point.CompanyPointId.HasValue =>
                addressLookup.TryGetValue(point.CompanyPointId.Value, out var addr)
                    ? addr
                    : "Адрес пункта выдачи недоступен",

            _ => "Неизвестный метод доставки"
        };
    }
    //public static async Task<(OrderDTO[] dtos, GetCompanyPointInfosRequest request)> PrepareOrdersData(
    //    IEnumerable<Order> orders)
    //{
    //    var companyPointIds = orders
    //        .SelectMany(o => new[]
    //        {
    //            o.SenderDeliveryPoint.CompanyPointId,
    //            o.ReceiverDeliveryPoint.CompanyPointId
    //        })
    //        .Where(id => id.HasValue)
    //        .Select(id => id!.Value)
    //        .Distinct()
    //        .ToArray();

    //    var request = new GetCompanyPointInfosRequest
    //    {
    //        RequestId = Guid.NewGuid(),
    //        CompanyPointIds = companyPointIds
    //    };

    //    var dtos = orders.Select(o => MapToDto(o)).ToArray();

    //    return (dtos, request);
    //}

    //public static OrderDTO[] FinalizeMapping(
    //    OrderDTO[] initialDtos,
    //    GetCompanyPointInfosResponse response)
    //{
    //    var addressCache = response.Points
    //        .ToDictionary(p => p.CompanyPointId, p => p.Address);

    //    return [.. initialDtos.Select(dto => dto with
    //    {
    //        SenderAddress = ResolveAddress(dto.SenderAddress, addressCache),
    //        ReceiverAddress = ResolveAddress(dto.ReceiverAddress, addressCache)
    //    })];
    //}

    //private static OrderDTO MapToDto(Order order)
    //{
    //    return new OrderDTO(
    //        OrderId: order.Id,
    //        Tracker: order.Tracker,
    //        SenderAddress: GetPointAddressTemplate(order.SenderDeliveryPoint),
    //        ReceiverAddress: GetPointAddressTemplate(order.ReceiverDeliveryPoint),
    //        Status: order.OrderStatus
    //    );
    //}

    //private static string GetPointAddressTemplate(DeliveryPoint deliveryPoint)
    //{
    //    return deliveryPoint.Method switch
    //    {
    //        DeliveryMethod.CourierCall => deliveryPoint.CustomPoint?.Address
    //            ?? "custom_address:unknown",

    //        DeliveryMethod.PickupPoint => $"company_point:{deliveryPoint.CompanyPointId}",

    //        _ => "unknown_method"
    //    };
    //}

    //private static string ResolveAddress(
    //    string template,
    //    Dictionary<int, string?> addressCache)
    //{
    //    if (template.StartsWith("company_point:"))
    //    {
    //        var idPart = template.Split(':')[1];
    //        if (int.TryParse(idPart, out var companyPointId)
    //            && addressCache.TryGetValue(companyPointId, out var address))
    //        {
    //            return address ?? "Адрес недоступен";
    //        }
    //        return "Пункт выдачи не найден";
    //    }
    //    return template.Replace("custom_address:", "");
    //}
}