//representa la transaccion de deposito en el ATM

public class Deposito : Transaccion
{
    private decimal monto; //monto a depositar
    private Teclado teclado; //referencia al teclado
    private RanuraDeposito ranuraDeposito; //referencia a la ranura de deposito
    private const int CANCELO = 0; //constante que representa la opcion cancelar

    //constructor
    public Deposito(int numeroCuentaUsuario, Pantalla pantallaATM,
        BaseDatosBanco baseDatosBancoATM, Teclado tecladoATM,
        RanuraDeposito ranuraDepositoATM) : base(numeroCuentaUsuario,
            pantallaATM, baseDatosBancoATM)
    {
        //inicializa las referencias al teclado y a la ranura depostio
        teclado = tecladoATM;
        ranuraDeposito = ranuraDepositoATM;
    }

    //realiza una transaccion
    public override void Ejecutar()
    {
        PantallaUsuario.BorrarPantalla();
        monto = PedirMontoADepositar(); //obtiene el monto a depositar del usuario

        //comprueba si el usuario introdujo un monto a depositar especificado
        if (monto != CANCELO)
        {
            //solicita un sobre de deposito que contega el monto especificado
            PantallaUsuario.MostrarMensaje("\nIntroduzca un sobre de deposito que contenga ");
            PantallaUsuario.MostrarMontoEnDolares(monto);
            PantallaUsuario.MostrarLineaMensaje(" en la ranura para depositos.");

            //obtiene el sobre de deposito
            bool sobreRecibido = ranuraDeposito.SeRecibioSobreDeposito();

            //comprueba so se recibio el sobre
            if (sobreRecibido)
            {
                PantallaUsuario.BorrarPantalla();
                PantallaUsuario.MostrarLineaMensaje("\nSe recibio su sobre.\n" +
                    "El dinero que acaba de despositar no estara disponible " +
                    "hasta que verifiquemos el monto del efectivo dentro del" +
                    "sobre, y que se haya verificado cualquier cheque incluido.");

                //abona a la cuenta para reflejar el deposito
                BaseDatos.Abonar(NumeroCuenta, monto);
            }

            else
                PantallaUsuario.MostrarLineaMensaje("\nNo se inserto un sobre, el ATM" +
                    "ha cancelado su transaccion.");
        }

        else
            PantallaUsuario.MostrarLineaMensaje("\nCancelando la transaccion...");
    }

    private decimal PedirMontoADepositar()
    {
        PantallaUsuario.BorrarPantalla();
        //muestra el indicador y recibe la entrada
        PantallaUsuario.MostrarMensaje("\nIntroduzca un monto a depositar en CENTAVOS" +
            "(o 0 para CANCELAR): ");
        int entrada = teclado.ObtenerEntrada();

        //comprueba si el usuario cancelo o introdujo un monto valido
        if (entrada == CANCELO)
            return CANCELO;

        else
            return entrada / 100.00M;
    }
}