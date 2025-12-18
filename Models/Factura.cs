using System.Runtime.Serialization;

namespace ventasSOAP.Models;

[DataContract]
public class Factura
{
    [DataMember(Order = 1)]
    public int Id { get; set; }

    [DataMember(Order = 2)]
    public string Numero { get; set; } = string.Empty;

    [DataMember(Order = 3)]
    public DateTime Fecha { get; set; }

    [DataMember(Order = 4)]
    public string Cliente { get; set; } = string.Empty;

    [DataMember(Order = 5)]
    public decimal Total { get; set; }

    [DataMember(Order = 6)]
    public List<Detalle> Detalles { get; set; } = new();

    //Direccion, RUC, Persona, ID, Telefono
    //Cedula, Nombre del Comprador

    [DataMember(Order = 7)]
    public string Direccion { get; set; } = string.Empty;

    [DataMember(Order = 8)]
    public string Ruc { get; set; } = string.Empty;

    [DataMember(Order = 9)]
    public string Persona { get; set; } = string.Empty;

    [DataMember(Order = 10)]
    public string Telefono { get; set; } = string.Empty;

    [DataMember(Order = 11)]
    public string Cedula { get; set; } = string.Empty;

    [DataMember(Order = 12)]
    public string NombreComprador { get; set; } = string.Empty;
}
