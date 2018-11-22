using System;
using System.IO;
using Microsoft.VisualBasic;

using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for csValores
/// </summary>
namespace MGMValores {

    public class Constantes {
       public  enum  btnAccion {
             Limpiar = 0
            ,Nuevo = 1
            ,Modificar = 2
            ,Eliminar = 3
            ,Autorizado = 4
            ,Procesado = 5
            ,Cancelado = 6
            ,Visualizar = 7
            ,Copiar = 8
            ,Imprimir = 9
            ,Rastreo = 10
            ,Validacion = 11
            ,VerError =12
            ,Genera = 13
            ,Aplica = 14
            ,PolizaEdit =15
            ,PolizaImp = 16
            ,Seguridad = 17
            ,Descuento = 18
            ,ListaPrecios = 19
            ,ModificarDet = 20
            ,Impuesto = 21
            ,Articulo= 22
            ,Documento = 23
            ,Agrupaciones = 24
            ,CFDITimbre = 30
            ,SaldoMensual = 40
            ,SaldoMov = 41
            ,SaldoAntig = 42
            ,SaldoInteg = 43

            //se agregan valores para cheque
            ,GeneraCheque = 44
            ,PolizaCheImp = 45
            ,ReversaCheque = 46
            ,CancelaCheque = 47 
        };

    }

}