//clase abstracta que representa una transaccion

using System;

public abstract class Transaccion
{
    private int numeroCuenta; //cuenta involucrada en la transaccion
    private Pantalla pantallaUsuario; //referencia a la pantalla del ATM
    private BaseDatosBanco baseDatos; //referencia a la base de datos de informacion de cuentas

    //constructor de tres parametros invocado por las clases derivadas
    public Transaccion(int cuentaUsuario, Pantalla laPantalla,
        BaseDatosBanco laBaseDatos)
    {
        numeroCuenta = cuentaUsuario;
        pantallaUsuario = laPantalla;
        baseDatos = laBaseDatos;
    }

    //propiedad de solo lectura
    public int NumeroCuenta
    {
        get
        {
            return numeroCuenta;
        }
    }

    //operaciones
    public Pantalla PantallaUsuario
    {
        get
        {
            return pantallaUsuario;
        }
    }

    public BaseDatosBanco BaseDatos
    {
        get
        {
            return baseDatos;
        }
    }

    //metodo abstracto que sera implementado por las clases derivadas
    public abstract void Ejecutar();
}