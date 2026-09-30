using System;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;

// 1. Crear Auto con Motor por composicion.
// 2. Crear Computadora con CPU y RAM por composicion.
// 3. Demostrar que Motor no tiene sentido sin Auto.
// 4. Crear Colectivo con Conductor por agregacion.
// 5. Demostrar que Conductor existe sin Colectivo.
// 6. Reasignar conductor de un colectivo a otro.
// 7. Crear Equipo con lista de Jugadores (agregacion).
// 8. Crear Factura con lista de Lineas (composicion).
// 9. Usar ?. para acceder de forma segura a Conductor?.Nombre.
// 10. Documentar con comentarios la diferencia entre ambas relaciones.
public class Motor
{
    public int cilindros {get; }
    public  Motor  (int c)=> cilindros = c;
    public void arrancar() => Console.WriteLine("Vroom");
} 
public class Auto
{
    public string Marca { get; }
    public Motor Motor { get; }
    public Auto (string marca, int cil)
    {
        Marca = marca;
        Motor = new Motor (cil);
    }
    public void Arrancar()
    {
        Console.WriteLine($"Arrancando el {Marca}...");
        Motor.arrancar();
    }
}  
public class CPU
{
    public string Modelo { get; }
    public double FrecuenciaGHz { get; }

    public CPU(string modelo, double frecuenciaGHz)
    {
        Modelo = modelo;
        FrecuenciaGHz = frecuenciaGHz;
    }

    public void Procesar() => Console.WriteLine($"Procesando datos con {Modelo} a {FrecuenciaGHz} GHz...");
}
public class RAM
{
    public int CapacidadGB { get; }
    public string Tipo { get; }
    public RAM(int capacidadGB, string tipo)
    {
        CapacidadGB = capacidadGB;
        Tipo = tipo;
    }
    public void CargarDatos() => Console.WriteLine($"Cargando datos en RAM {Tipo} de {CapacidadGB} GB...");
}
public class Computadora
{
    public string Marca { get; }
    public CPU Cpu { get; }
    public RAM Ram { get; }
    public Computadora(string marca, string modeloCpu, double GHz, int gbRam, string tipoRam)
    {
        Marca = marca;
        Cpu = new CPU(modeloCpu, GHz);
        Ram = new RAM(gbRam, tipoRam);
    }
    public void Encender()
    {
    Console.WriteLine($"=== Encendiendo computadora {Marca} ===");
    Ram.CargarDatos();
    Cpu.Procesar();
    Console.WriteLine("¡Computadora lista para usar!\n");
    }
}


public class Conductor
{
    public string Nombre { get; set; }
    public Conductor(string nombre)
    {
        Nombre = nombre;
    }
}   
public class Colectivo
{   
    public string linea="";
    public Conductor Conductor { get; private set; }
    public Colectivo(Conductor conductor, string Linea)
    {
        linea=Linea;
        Conductor = conductor;
    }
    public Colectivo()
    {
        Conductor = null;
    }

    public void AsignarConductor(Conductor c) => Conductor = c;
    public void Desvincular() => Conductor = null;
}




public class Jugador
{
    public string Nombre { get; set; }
    public string Posicion { get; set; }

    public Jugador(string nombre, string posicion)
    {
        Nombre = nombre;
        Posicion = posicion;
    }
}
public class Equipo
{
    public string NombreEquipo { get; set; }
    public List<Jugador> Jugadores { get; private set; }
    public Equipo(string nombreEquipo, List<Jugador> jugadoresIniciales)
    {
        NombreEquipo = nombreEquipo;
        Jugadores = jugadoresIniciales ?? new List<Jugador>();
    }
    public Equipo(string nombreEquipo)
    {
        NombreEquipo = nombreEquipo;
        Jugadores = new List<Jugador>();
    }
    public void AgregarJugador(Jugador j)
    {
        if (j != null && !Jugadores.Contains(j))
        {
            Jugadores.Add(j);
        }
    }

    public void RemoverJugador(Jugador j)
    {
        Jugadores.Remove(j);
    }

    public void MostrarPlantel()
    {
        Console.WriteLine($"=== Plantel de {NombreEquipo} ===");
        if (Jugadores.Count == 0)
        {
            Console.WriteLine("No hay jugadores en el equipo.");
            return;
        }
        foreach (var j in Jugadores)
        {
            Console.WriteLine($"- {j.Nombre} ({j.Posicion})");
        }
    }
}
public class LineaFactura
{
    public string Producto { get; }
    public int Cantidad { get; }
    public decimal PrecioUnitario { get; }

    public decimal Subtotal => Cantidad * PrecioUnitario;
    public LineaFactura(string producto, int cantidad, decimal precioUnitario)
    {
        Producto = producto;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }
}

public class Factura
{
    public int Numero { get; }
    public string Fecha { get; }
    
    private readonly List<LineaFactura> _lineas;
    public IReadOnlyList<LineaFactura> Lineas => _lineas.AsReadOnly();

    public Factura(int numero, string fecha)
    {
        Numero = numero;
        Fecha = fecha;
        _lineas = new List<LineaFactura>();
    }
    public void AgregarLinea(string producto, int cantidad, decimal precioUnitario)
    {
        LineaFactura nuevaLinea = new LineaFactura(producto, cantidad, precioUnitario);
        _lineas.Add(nuevaLinea);
    }

    public decimal CalcularTotal()
    {
        decimal total = 0;
        foreach (var linea in _lineas)
        {
            total += linea.Subtotal;
        }
        return total;
    }

    public void MostrarFactura()
    {
        Console.WriteLine($"=== FACTURA N° {Numero} ({Fecha:dd/MM/yyyy}) ===");
        foreach (var linea in _lineas)
        {
            Console.WriteLine($"- {linea.Producto} x{linea.Cantidad} | ${linea.PrecioUnitario:F2} = ${linea.Subtotal:F2}");
        }
        Console.WriteLine($"TOTAL: ${CalcularTotal():F2}\n");
    }
}





















class Program
{
    static void Main()
    {
        Auto Ciromovil= new Auto("Mustang",10);
        Console.WriteLine($"Marca: {Ciromovil.Marca}");
        Console.WriteLine($"Cilindros: {Ciromovil.Motor.cilindros}");

        Computadora SuperTostadoraDeBauti= new Computadora("Asus", "Ryzen 5 8600G", 4.5, 36 ,"DDR5");
        SuperTostadoraDeBauti.Encender();
        // lo dejo como comentario pq no me deja probar el resto del codigo
        // Ciromovil=null; 
        if(Ciromovil==null){
        Console.WriteLine("Se borro el auto");
        //Aca sale un error pq se borro el auto y no se puede acceder al motor se elimino con el auto
        Console.WriteLine(Ciromovil.Motor);}

        Conductor chofer = new Conductor("Andre Nina");
        Console.WriteLine($"Conductor creado independientemente: {chofer.Nombre}");
        Colectivo linea60 = new Colectivo(chofer,"Linea 60");
        Console.WriteLine($"Conductor {linea60.Conductor?.Nombre} asignado al Colectivo: {linea60.linea}");
        linea60.Desvincular();
        Console.WriteLine($"Conductor despues de desvincular el colectivo: {chofer.Nombre}");
        Colectivo linea31 = new Colectivo(chofer, "Linea 31");
        Console.WriteLine($"Conductor {linea31.Conductor?.Nombre} asignado al Colectivo: {linea31.linea}");

        Jugador j1 = new Jugador("Lionel Messi", "Delantero");
        Jugador j2 = new Jugador("Rodrigo De Paul", "Mediocampista");

        Equipo seleccion = new Equipo("Argentina");
        seleccion.AgregarJugador(j1);
        seleccion.AgregarJugador(j2);
        seleccion.MostrarPlantel();


        Factura factura = new Factura(1001,"28/09/2026");
        factura.AgregarLinea("Notebook",2,1222.19m);
        factura.AgregarLinea("Mouse Inalambrico", 2, 25.50m);
        factura.MostrarFactura();

    }   

}



