//La clase Retiro representa una transaccion de retiro del ATM
public class Retiro : Transaccion
{
    private decimal monto; //monto a retirar
    private Teclado teclado; //referencia al teclado
    private DispensadorEfectivo dispensadorEfectivo; //referencia al dispensador de efectivo
    private const int CANCELO = 6; //constante que corresponde a la opcion de menu para cancelar

    //constructor
    public Retiro(int numeroCuentaUsuario,  Pantalla pantallaATM,
        BaseDatosBanco baseDatosBancoATM, Teclado tecladoATM,
        DispensadorEfectivo dispensadorEfectivoATM) : base(numeroCuentaUsuario,
            pantallaATM, baseDatosBancoATM)
    {
        //inicializa las referencias a teclado y al dispensador de efectivo
        teclado = tecladoATM;
        dispensadorEfectivo = dispensadorEfectivoATM;
    }
     
    //operaciones
    
    //redefine el metodo de la clase base
    public override void Ejecutar() //realiza una transaccion
    {
        bool efectivoDispensado = false; //el efectivo no se ha dispensado aun

        //la transaccion no se ha cancelado aun
        bool transaccionCancelada = false;

        //itera hasta que se dispense efectivo o el usuario cancele
        do
        {
            //obtiene el monto elegido por el usuario
            int seleccion = MostrarMenuDeMontos();

            //comprueba si el usuario eligio un monto de retiro o cancelo
            if(seleccion != CANCELO)
            {
                //establece monto al monto seleccionado en dolares
                monto = seleccion;

                //obtiene el saldo disponible en la cuenta involucrada
                decimal saldoDisponible = BaseDatos.ObtenerSaldoDisponible(NumeroCuenta);

                //comprueba si el usuario tiene suficiente dinero
                if(monto <= saldoDisponible)
                {
                    //comprueba si el dispensador de efectivo tiene suficiente dinero
                    if (dispensadorEfectivo.HaySuficienteEfectivoDisponible(monto))
                    {
                        //carga a la cuenta para reflejar el retiro
                        BaseDatos.Cargar(NumeroCuenta, monto);

                        dispensadorEfectivo.DispensarEfectivo(monto); //dispensa efectivo
                        efectivoDispensado = true; //se dispenso efectivo

                        //instruye al usuario para que tome el efectivo
                        PantallaUsuario.MostrarLineaMensaje("\nPor favor tome su efectivo " +
                            "del dispensador.");
                    }

                    else //el dispensador no tiene suficiente efectivo
                        PantallaUsuario.MostrarLineaMensaje("\nNo hay suficiente dinero " +
                            "disponible en el ATM\n\nPor favor elija un monto mas pequeño.");
                }

                else //no hay suficiente dinero disponible en la cuenta del usuario
                {
                    PantallaUsuario.MostrarLineaMensaje("\nNo hay suficiente dinero disponible" +
                        "en su cuenta\n\nPor favor elija un monto mas pequeño.");
                }
            }

            else
            {
                PantallaUsuario.MostrarLineaMensaje("\nCancelando la transaccion...");
                transaccionCancelada = true; //el usuario cancelo la transaccion
            }

        } while ((!efectivoDispensado) && (!transaccionCancelada));
    }

    //muestra un menu de montos para retirar y la opcion para cancelar
    //devuelve el monto elegido o 6 si el usuario elije cancelar
    private int MostrarMenuDeMontos()
    {
        int eleccionUsuario = 0; //almacena el valor devuelto

        //arreglo de montos que corresponden a los numeros del menu
        int[] montos = { 0, 20, 40, 60, 100, 200 };

        //itera mientras no se haya realizado una seleccion valida
        while(eleccionUsuario == 0)
        {
            PantallaUsuario.MostrarLineaMensaje("\nOpciones de retiro:");
            PantallaUsuario.MostrarLineaMensaje("1 - $20");
            PantallaUsuario.MostrarLineaMensaje("2 - $40");
            PantallaUsuario.MostrarLineaMensaje("3 - $60");
            PantallaUsuario.MostrarLineaMensaje("4 - $100");
            PantallaUsuario.MostrarLineaMensaje("5 - $200");
            PantallaUsuario.MostrarLineaMensaje("6 - Cancelar la transaccion");
            PantallaUsuario.MostrarLineaMensaje("\nElija una opcion de retiro (1-6): ");

            //obtiene la entrada de usuario
            int entrada = teclado.ObtenerEntrada();

            //determina como proceder con base en el valor de entrada
            switch(entrada)
            {
                //si el usuario eligio un monto de retiro (osea una opcion del 1 - 5) devuelve 
                //el monto correspondiente del arreglo montos
                case 1: case 2: case 3: case 4: case 5:
                    eleccionUsuario = montos[entrada]; //guarda la eleccion del usuario
                    break;

                case CANCELO:
                    eleccionUsuario = CANCELO; //guarda la eleccion del usuario
                    break;

                default:
                    PantallaUsuario.MostrarLineaMensaje("\nSeleccion invalida. Intente nuevamente.");
                    teclado.ApretarTecla();
                    break;
            }

        }

        return eleccionUsuario;
    }
}