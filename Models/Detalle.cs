using System.Runtime.Serialization;

namespace ventasSOAP.Models;

[DataContract]
public class Detalle
{
    [DataMember(Order = 1)]
    public int Id { get; set; }

    [DataMember(Order = 2)]
    public string Producto { get; set; } = string.Empty;

    [DataMember(Order = 3)]
    public int Cantidad { get; set; }

    [DataMember(Order = 4)]
    public decimal PrecioUnitario { get; set; }

    [DataMember(Order = 5)]
    public decimal Subtotal { get; set; }
}
