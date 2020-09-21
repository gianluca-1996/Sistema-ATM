//representa el dispensador de efectivo del ATM

public class DispensadorEfectivo
{
    //el numero inicial predeterminado de billetes en el dispensador de efectivo
    private const int CUENTA_INICIAL = 500;
    private int cuentaBilletes; //numero restantes de billetes de $20

    //constructor
    public DispensadorEfectivo()
    {
        cuentaBilletes = CUENTA_INICIAL;
    }

    //simula dispensar el monto especificado de efectivo
    public void DispensarEfectivo(decimal monto)
    {
        //numero requerido de billetes de 20
        int billetesRequeridos = ((int)monto) / 20;
        cuentaBilletes -= billetesRequeridos;
    }

    //indica si el dispensador de efectivo puede dispensar el monto deseado
    public bool HaySuficienteEfectivoDisponible(decimal monto)
    {
        //numero requerido de billetes de 20
        int billetesRequeridos = ((int)monto) / 20;

        //devuelve si hay suficientes billetes disponibles
        return (cuentaBilletes >= billetesRequeridos);
    }
}