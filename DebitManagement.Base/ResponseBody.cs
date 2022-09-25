using System.ComponentModel;

namespace DebitManagement.Base;

public class ResponseBody
{
    [DisplayName("message")] public string Message { get; set; }

    [DisplayName("count")] public int Count { get; set; }

    [DisplayName("items")] public object[] Items { get; set; }
}