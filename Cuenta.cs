//La clase Cuenta representa una cuenta de usuario del ATM

public class Cuenta
{
    private int numeroCuenta;
    private int nip;
    private decimal saldoDisponible;
    private decimal saldoTotal; //monto disponible + deposito pendiente

    public Cuenta(int elNumeroCuenta, int elNIP, decimal elSaldoDisponible, decimal elSaldoTotal)
    {
        numeroCuenta = elNumeroCuenta;
        nip = elNIP;
        saldoDisponible = elSaldoDisponible;
        saldoTotal = elSaldoTotal;
    }

    //propiedades de solo lectura
    public int NumeroCuenta
    {
        get
        {
            return numeroCuenta;
        }
    }

    public decimal SaldoDisponible
    {
        get
        {
            return saldoDisponible;
        }
    }

    public decimal SaldoTotal
    {
        get
        {
            return saldoTotal;
        }
    }

    //operaciones

    //determina si un NIP especificado por el usuario coincide con un NIP en Cuenta
    public bool ValidarNIP(int NIPUsuario)
    {
        return (NIPUsuario == nip);
    }
    
    //abona a la cuenta (los fondos no se verificaron todavia)
    public void Abonar(decimal monto)
    {
        saldoTotal += monto; //lo suma al saldo total
    }

    //carga la cuenta
    public void Cargar(decimal monto)
    {
        saldoDisponible -= monto;
        saldoTotal -= monto;
    }
}