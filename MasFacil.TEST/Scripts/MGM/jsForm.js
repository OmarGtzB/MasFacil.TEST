
function OnClientClic_ConfirmOK(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (argument) {
        if (argument) {
            this.click();
        }
    });
    var text = "Esta seguro que desea realizar la accion?";
    radconfirm(text, callBackFunction, 300, 100, null, "Confirmación");
    args.set_cancel(true);
}

function OnClientClic_ConfirmOKImpChe(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (argument) {
        if (argument) {
            this.click();
        }
    });
    var text = "Esta seguro que desea imprimir la póliza cheque?";
    radconfirm(text, callBackFunction, 300, 100, null, "Confirmación");
    args.set_cancel(true);
}

function OnClientClic_ConfirmCancel(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (argument) {
        if (argument) {
            this.click();
        }
    });
    var text = "Esta seguro que quiere cancelar la acción?";
    radconfirm(text, callBackFunction, 300, 100, null, "Confirmación");
    args.set_cancel(true);
}


function OnClientClic_ConfirmOK2(sender, args) {
    var callBackFunction = Function.createDelegate(sender, function (argument) {
        if (argument) {
              FNSeleccionRegistroApartadoEstandar();
        }
    });
    var text = "Esta seguro que desea realizar la accion?";
    radconfirm(text, callBackFunction, 300, 100, null, "Confirmación");
    args.set_cancel(true);
}


//FORMA MANUAL
function openRadWin(URLS, counter) {

    var input = URLS;
    var fields = input.split('|');

    for (i = 0; i < counter ; i++) {

        window.open("..\\Temp\\" + fields[i] + "", "RadWindow" + i + "", "width=720px, height=1024px, resizable");
    }

}


//ReportePolizas
function openRadWinRptPol(URL) {
    window.open("..\\Temp\\" + URL + "", "RadWindowRptPol", "width=720px, height=512px, resizable");
}


//function openRadWin(URLS, counter, tres) {

//    var input = URLS;
//    var fields = input.split('|');

//    for (i = 0; i < counter ; i++) {

//        window.open(tres + fields[i] + "", "RadWindow" + i + "", "width=720px, height=1024px, resizable");
//    }

//}