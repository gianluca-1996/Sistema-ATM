//Representa a un cajero automatico

public class ATM
{
    private bool usuarioAutenticado; //verdadero si el usuario es autenticado
    private int numeroCuentaActual; //numero de cuenta del usuario
    private Pantalla pantalla; //referencia a la pantalla del ATM
    private Teclado teclado; //referencia a el teclado del ATM
    private DispensadorEfectivo dispensadorEfectivo; //referencia al dispensador de efectivo del ATM
    private RanuraDeposito ranuraDeposito; //referencia a la ranura de deposito del ATM
    private BaseDatosBanco baseDatosBanco; //referencia a la informacion de la base de datos de cuentas

    //enumeracion que representa las opciones del menu principal
    private enum OpcionMenu
    {
        SOLICITUD_SALDO = 1,
        RETIRO = 2,
        DEPOSITO = 3,
        SALIR_ATM = 4
    }

    //constructor 
    public ATM()
    {
        usuarioAutenticado = false; //al principio el usuario no esta autenticado
        numeroCuentaActual = 0; //al principio no hay numero de cuenta actual
        pantalla = new Pantalla(); //crea la pantalla
        teclado = new Teclado(); //crea el teclado
        dispensadorEfectivo = new DispensadorEfectivo(); //crea el dispensador de efectivo
        ranuraDeposito = new RanuraDeposito(); //crea la ranura de deposito
        baseDatosBanco = new BaseDatosBanco(); //crea la base de datos de info de cuentas
    }

    //INICIA EL ATM
    public void Ejecutar()
    {
        //bienvenida y autenticacion
        while(true)
        {
            //itera mientas el usuario no sea autenticado
            while(!usuarioAutenticado)
            {
                pantalla.MostrarLineaMensaje("\n¡Bienvenido!");
                AutenticarUsuario(); //autentica al usuario
                pantalla.BorrarPantalla();
            }

            RealizarTransacciones(); //para el usuario autenticado
            usuarioAutenticado = false; //se restablece antes de la siguiente sesion con el ATM
            numeroCuentaActual = 0;
            pantalla.MostrarLineaMensaje("\n¡Gracias!, ¡adios!\n");
        }
    }

    //trata de autenticar al usuario con la base de datos
    private void AutenticarUsuario()
    {
        //pide el numero de cuenta
        pantalla.MostrarMensaje("\nIntroduzca su numero de cuenta:  ");
        int numeroCuenta = teclado.ObtenerEntrada();

        //pide el NIP
        pantalla.MostrarMensaje("\nIntroduzca su NIP:  ");
        int pin = teclado.ObtenerEntrada();

        //establece usuarioAutenticado al valor booleano devuelto por la base de datos
        usuarioAutenticado = baseDatosBanco.AutenticarUsuario(numeroCuenta, pin);

        //verifica si se realizo la autenticacion con exito
        if (usuarioAutenticado)
            numeroCuentaActual = numeroCuenta; //guarda el # de cuenta del usuario

        else
        {
            pantalla.MostrarLineaMensaje("Numero de cuenta o NIP invalido. Intente nuevamente.\n");
            teclado.ApretarTecla();
        }
        
        
    }

    //muestra el menu principal y realiza las transacciones
    private void RealizarTransacciones()
    {
        Transaccion transaccionActual; //la transaccion que se esta procesando
        bool usuarioSalio = false; //el usuario no ha elegido salir

        //itera mientras el usuario no elija la opcion para salir
        while(!usuarioSalio)
        {
            //muestra el menu principal y obtiene la seleccion del usuario
            int seleccionMenuPrincipal = MostrarMenuPrincipal();

            //decide como proceder en base a la seleccion del menu
            switch((OpcionMenu) seleccionMenuPrincipal)
            {
                //el usuario elije realizar una de las tres transacciones
                case OpcionMenu.SOLICITUD_SALDO:
                case OpcionMenu.RETIRO:
                case OpcionMenu.DEPOSITO:
                    //se inicializa como nuevo objeto del tipo elegido
                    transaccionActual = CrearTransaccion(seleccionMenuPrincipal);
                    transaccionActual.Ejecutar(); //ejecuta la transaccion
                    break;

                case OpcionMenu.SALIR_ATM: //el usuario eligio terminar la sesion
                    pantalla.MostrarLineaMensaje("\nSaliendo del sistema...");
                    usuarioSalio = true;
                    break;

                default: //el ususario no introdujo un entero del 1 al 4
                    pantalla.MostrarLineaMensaje("\nNo introdujo una seleccion valida." +
                        "Intente nuevamente.");
                    break;
            }
        }
    }

    //muestra el menu principal y devuelve una seleccion de entrada
    private int MostrarMenuPrincipal()
    {
        pantalla.MostrarLineaMensaje("\nMenu principal:");
        pantalla.MostrarLineaMensaje("1 - Ver mi saldo");
        pantalla.MostrarLineaMensaje("2 - Retirar efectivo");
        pantalla.MostrarLineaMensaje("3 - Depositar fondos");
        pantalla.MostrarLineaMensaje("4 - Salir\n");
        pantalla.MostrarLineaMensaje("Introduzca una opcion: ");
        return teclado.ObtenerEntrada(); //devuelve la seleccion del usuario
    }

    //devuelve un objeto de la clase especificada, derivada de la clase Transaccion
    private Transaccion CrearTransaccion(int tipo)
    {
        Transaccion temp = null; //referencia Transaccion nula

        //determina el tipo de transaccion que va a crear
        switch((OpcionMenu) tipo)
        {
            case OpcionMenu.SOLICITUD_SALDO:
                temp = new SolicitudSaldo(numeroCuentaActual, pantalla, baseDatosBanco);
                break;

            case OpcionMenu.RETIRO:
                temp = new Retiro(numeroCuentaActual, pantalla, baseDatosBanco, teclado,
                    dispensadorEfectivo);
                break;

            case OpcionMenu.DEPOSITO:
                temp = new Deposito(numeroCuentaActual, pantalla, baseDatosBanco, teclado,
                    ranuraDeposito);
                break;
        }

        return temp;
    }
}