using CoreWCF;
using ventasSOAP.Models;

namespace ventasSOAP.Services;

[ServiceContract]
public interface IFACTURA
{
    [OperationContract]
    Factura RegistrarFactura(Factura factura);

    [OperationContract]
    List<Factura> ObtenerFacturas();
}
