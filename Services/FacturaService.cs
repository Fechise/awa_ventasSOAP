using ventasSOAP.Models;

namespace ventasSOAP.Services;

public class FacturaService : IFACTURA
{
    private static readonly List<Factura> Facturas = new();
    private static readonly object SyncRoot = new();

    static FacturaService()
    {
        lock (SyncRoot)
        {
            var seed = new[]
            {
                new Factura
                {
                    Numero = "F-2025-001",
                    Fecha = new DateTime(2025, 12, 18),
                    Cliente = "Acme Corp",
                    Detalles = new List<Detalle>
                    {
                        new() { Producto = "Laptop", Cantidad = 2, PrecioUnitario = 850m },
                        new() { Producto = "Mouse", Cantidad = 3, PrecioUnitario = 15.5m }
                    }
                },
                new Factura
                {
                    Numero = "F-2025-002",
                    Fecha = new DateTime(2025, 12, 17),
                    Cliente = "Globex",
                    Detalles = new List<Detalle>
                    {
                        new() { Producto = "Servidor", Cantidad = 1, PrecioUnitario = 3200m },
                        new() { Producto = "SSD 1TB", Cantidad = 4, PrecioUnitario = 120m }
                    }
                },
                new Factura
                {
                    Numero = "F-2025-003",
                    Fecha = new DateTime(2025, 12, 16),
                    Cliente = "Initech",
                    Detalles = new List<Detalle>
                    {
                        new() { Producto = "Licencia Office", Cantidad = 10, PrecioUnitario = 85m }
                    }
                },
                new Factura
                {
                    Numero = "F-2025-004",
                    Fecha = new DateTime(2025, 12, 15),
                    Cliente = "Stark Industries",
                    Detalles = new List<Detalle>
                    {
                        new() { Producto = "Dron", Cantidad = 5, PrecioUnitario = 450m },
                        new() { Producto = "Batería", Cantidad = 5, PrecioUnitario = 75m }
                    }
                },
                new Factura
                {
                    Numero = "F-2025-005",
                    Fecha = new DateTime(2025, 12, 14),
                    Cliente = "Wayne Enterprises",
                    Detalles = new List<Detalle>
                    {
                        new() { Producto = "Router", Cantidad = 3, PrecioUnitario = 180m },
                        new() { Producto = "Switch 24p", Cantidad = 2, PrecioUnitario = 260m },
                        new() { Producto = "Patch Cord", Cantidad = 20, PrecioUnitario = 6.5m }
                    }
                }
            };

            int nextFacturaId = 1;
            foreach (var f in seed)
            {
                CalcularYAsignarIds(f, nextFacturaId++);
                Facturas.Add(Clone(f));
            }
        }
    }

    public Factura RegistrarFactura(Factura factura)
    {
        if (factura == null)
        {
            throw new ArgumentNullException(nameof(factura));
        }

        lock (SyncRoot)
        {
            factura.Detalles ??= new List<Detalle>();
            CalcularYAsignarIds(factura, Facturas.Count == 0 ? 1 : Facturas.Max(f => f.Id) + 1);

            Facturas.Add(Clone(factura));

            return Clone(factura);
        }
    }

    public List<Factura> ObtenerFacturas()
    {
        lock (SyncRoot)
        {
            return Facturas.Select(Clone).ToList();
        }
    }

    private static void CalcularYAsignarIds(Factura factura, int facturaId)
    {
        factura.Id = facturaId;

        for (int i = 0; i < factura.Detalles.Count; i++)
        {
            var detalle = factura.Detalles[i];
            detalle.Id = i + 1;
            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
        }

        factura.Total = factura.Detalles.Sum(d => d.Subtotal);
    }

    private static Factura Clone(Factura factura)
    {
        return new Factura
        {
            Id = factura.Id,
            Numero = factura.Numero,
            Fecha = factura.Fecha,
            Cliente = factura.Cliente,
            Total = factura.Total,
            Detalles = factura.Detalles
                .Select(d => new Detalle
                {
                    Id = d.Id,
                    Producto = d.Producto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                })
                .ToList()
        };
    }
}
