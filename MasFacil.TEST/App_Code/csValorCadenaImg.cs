using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for csValorCadenaImg
/// </summary>
public class csValorCadenaImg
{
    private static String cadenaimagen = "";

    // Declare a Name property of type string:
    public static String CadenaImagen
    {
        get
        {
            return cadenaimagen;
        }
        set
        {
            cadenaimagen = value;
        }
    }
}