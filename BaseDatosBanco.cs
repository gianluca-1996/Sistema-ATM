//representa la base de datos de informacion de las cuentas bancarias

public class BaseDatosBanco
{
    private Cuenta[] cuentas; //arreglo de cuentas bancarias

    //constructor
    public BaseDatosBanco()
    {
        //crea dos objetos Cuenta para prueba y los coloca en el arreglo
        cuentas = new Cuenta[2]; //crea el arreglo cuentas
        cuentas[0] = new Cuenta(12345, 54321, 1000.00M, 1200.00M);
        cuentas[1] = new Cuenta(98765, 56789, 200.00M, 200.00M);
    }

    //obtiene un objeto Cuenta que contiene el numero de cuenta especificado
    private Cuenta ObtenerCuenta(int numeroCuenta)
    {
        //itera a traves de cuentas, buscando un numero de cuenta que coincida
        foreach(Cuenta cuentaActual in cuentas)
        {
            if (cuentaActual.NumeroCuenta == numeroCuenta)
                return cuentaActual;
        }

        //no se encontro la cuenta
        return null;
    }

    //determina si el numero de cuenta en el NIP especificados por el usuario
    //coinciden con los de una cuenta en la base de datos
    public bool AutenticarUsuario(int numeroCuentaUsuario, int NIPUsuario)
    {
        //trata de obtener la cuenta con el numero de cuenta
        Cuenta cuentausuario = ObtenerCuenta(numeroCuentaUsuario);

        //si la cuenta existe devuelve el resultado de la funcion ValidarNIP de Cuenta
        if (cuentausuario != null)
            return cuentausuario.ValidarNIP(NIPUsuario);

        else
            return false;
    }

    //devuelve el saldo disponible de la Cuenta con el numero de cuenta especificado
    public decimal ObtenerSaldoDisponible(int numeroCuentaUsuario)
    {
        Cuenta cuentaUsuario = ObtenerCuenta(numeroCuentaUsuario);
        return cuentaUsuario.SaldoDisponible;
    }

    //devuelve el saldo total de la Cuenta con el numero de cuenta especificado
    public decimal ObtenerSaldoTotal(int numeroCuentaUsuario)
    {
        Cuenta cuentaUsuario = ObtenerCuenta(numeroCuentaUsuario);
        return cuentaUsuario.SaldoTotal;
    }

    //abona a la Cuenta con el numero de cuenta especificado
    public void Abonar(int numeroCuentaUsuario, decimal monto)
    {
        Cuenta cuentaUsuario = ObtenerCuenta(numeroCuentaUsuario);
        cuentaUsuario.Abonar(monto);
    }

    //carga a la Cuenta con el numero de cuenta especificado
    public void Cargar(int numeroCuentaUsuario, decimal monto)
    {
        Cuenta cuentaUsuario = ObtenerCuenta(numeroCuentaUsuario);
        cuentaUsuario.Cargar(monto);
    }
}