using System.Collections.Concurrent;
using CoreWCF;
using ventasSOAP.Models;

namespace ventasSOAP.Services;

public class FacturaService : IFACTURA
{
    private static readonly ConcurrentDictionary<int, Factura> Facturas = new(
        Seed().Select(f => new KeyValuePair<int, Factura>(f.Id, f)));

    public Factura RegistrarFactura(Factura factura)
    {
        if (factura is null) throw new ArgumentNullException(nameof(factura));
        if (factura.Id <= 0) throw new FaultException("El Id es requerido y debe ser mayor que cero.");

        factura.Detalles ??= new List<Detalle>();
        CalcularTotales(factura);

        if (!Facturas.TryAdd(factura.Id, factura))
            throw new FaultException($"Ya existe una factura con Id {factura.Id}");

        return factura;
    }

    public List<Factura> ObtenerFacturas()
    {
        return Facturas.Values.ToList();
    }

    private static void CalcularTotales(Factura factura)
    {
        for (int i = 0; i < factura.Detalles.Count; i++)
        {
            var d = factura.Detalles[i];
            d.Id = i + 1;
            d.Subtotal = d.Cantidad * d.PrecioUnitario;
        }

        factura.Total = factura.Detalles.Sum(d => d.Subtotal);
    }

    private static IEnumerable<Factura> Seed()
    {
        var seed = new[]
        {
            new Factura
            {
                Id = 1,
                Numero = "F-2025-001",
                Fecha = new DateTime(2025, 12, 18),
                Cliente = "Acme Corp",
                Direccion = "Av. 1 y Calle A",
                Ruc = "1790010010001",
                Persona = "Contacto: John Doe",
                Telefono = "0991111111",
                Cedula = "0102030405",
                NombreComprador = "John Doe",
                Detalles = new List<Detalle>
                {
                    new() { Producto = "Laptop", Cantidad = 2, PrecioUnitario = 850m },
                    new() { Producto = "Mouse", Cantidad = 3, PrecioUnitario = 15.5m }
                }
            },
            new Factura
            {
                Id = 2,
                Numero = "F-2025-002",
                Fecha = new DateTime(2025, 12, 17),
                Cliente = "Globex",
                Direccion = "Calle 2 N45-10",
                Ruc = "1790010010002",
                Persona = "Contacto: Mary Roe",
                Telefono = "0992222222",
                Cedula = "0203040506",
                NombreComprador = "Mary Roe",
                Detalles = new List<Detalle>
                {
                    new() { Producto = "Servidor", Cantidad = 1, PrecioUnitario = 3200m },
                    new() { Producto = "SSD 1TB", Cantidad = 4, PrecioUnitario = 120m }
                }
            },
            new Factura
            {
                Id = 3,
                Numero = "F-2025-003",
                Fecha = new DateTime(2025, 12, 16),
                Cliente = "Initech",
                Direccion = "Parque Tec, Bloque B",
                Ruc = "1790010010003",
                Persona = "Contacto: Peter Gibbons",
                Telefono = "0993333333",
                Cedula = "0304050607",
                NombreComprador = "Peter Gibbons",
                Detalles = new List<Detalle>
                {
                    new() { Producto = "Licencia Office", Cantidad = 10, PrecioUnitario = 85m }
                }
            },
            new Factura
            {
                Id = 4,
                Numero = "F-2025-004",
                Fecha = new DateTime(2025, 12, 15),
                Cliente = "Stark Industries",
                Direccion = "Av. Industrial 500",
                Ruc = "1790010010004",
                Persona = "Contacto: Tony Stark",
                Telefono = "0994444444",
                Cedula = "0405060708",
                NombreComprador = "Tony Stark",
                Detalles = new List<Detalle>
                {
                    new() { Producto = "Dron", Cantidad = 5, PrecioUnitario = 450m },
                    new() { Producto = "Batería", Cantidad = 5, PrecioUnitario = 75m }
                }
            },
            new Factura
            {
                Id = 5,
                Numero = "F-2025-005",
                Fecha = new DateTime(2025, 12, 14),
                Cliente = "Wayne Enterprises",
                Direccion = "Gotham Center 101",
                Ruc = "1790010010005",
                Persona = "Contacto: Bruce Wayne",
                Telefono = "0995555555",
                Cedula = "0506070809",
                NombreComprador = "Bruce Wayne",
                Detalles = new List<Detalle>
                {
                    new() { Producto = "Router", Cantidad = 3, PrecioUnitario = 180m },
                    new() { Producto = "Switch 24p", Cantidad = 2, PrecioUnitario = 260m },
                    new() { Producto = "Patch Cord", Cantidad = 20, PrecioUnitario = 6.5m }
                }
            }
        };

        foreach (var f in seed)
        {
            f.Detalles ??= new List<Detalle>();
            CalcularTotales(f);
        }

        return seed;
    }
}
