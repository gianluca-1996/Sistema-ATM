//representa una transaccion de solicitud de saldo en el ATM

public class SolicitudSaldo : Transaccion
{
    //el constructor inicializa las variables de la clas base
    public SolicitudSaldo(int numeroCuentaUsuario, Pantalla pantallaATM,
        BaseDatosBanco baseDatosBancoATM) : base(numeroCuentaUsuario,
            pantallaATM, baseDatosBancoATM)
    { }

    //realiza una transaccion; redefine el metodo abstracto de la clase base
    public override void Ejecutar()
    {
        //obtiene el saldo disponible para la cuenta del usuario actual
        decimal saldoDisponible = BaseDatos.ObtenerSaldoDisponible(NumeroCuenta);

        //obtiene el saldo total de la cuenta del usuario actual
        decimal saldoTotal = BaseDatos.ObtenerSaldoTotal(NumeroCuenta);

        //muestra la informacion del saldo en la pantalla
        PantallaUsuario.MostrarLineaMensaje("\nInformacion del saldo: ");
        PantallaUsuario.MostrarMensaje(" - Saldo disponible: ");
        PantallaUsuario.MostrarMontoEnDolares(saldoDisponible);
        PantallaUsuario.MostrarMensaje("\n - Saldo total: ");
        PantallaUsuario.MostrarMontoEnDolares(saldoTotal);
        PantallaUsuario.MostrarLineaMensaje("");
    }
}