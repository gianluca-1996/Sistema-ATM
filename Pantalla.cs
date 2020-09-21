//representa la pantalla del ATM
using System;

public class Pantalla
{
    //muestra un mensaje sin un retorno de carro al final
    public void MostrarMensaje(string mensaje)
    {
        Console.Write(mensaje);
    }

    //muestra un mensaje con retorno de carro al final
    public void MostrarLineaMensaje(string mensaje)
    {
        Console.WriteLine(mensaje);
    }

    //muestra un monto en dolares
    public void MostrarMontoEnDolares( decimal monto)
    {
        Console.Write("{0:C}", monto);
    }

    public void BorrarPantalla()
    {
        Console.Clear();
    }
}