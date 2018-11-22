//ESTA FUNCION SOLO PERMITE ESCRIBIR LETRAS EN LOS TEXTBOX  onkeypress="return sololetras(event)"
function sololetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[A-Za-z]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}


//ESTA FUNCION SOLO PERMITE ESCRIBIR NUMEROS EN LOS TEXTBOX  onkeypress="return solonumeros(event)"
//ESTA FUNCION SOLO PERMITE ESCRIBIR NUMEROS EN LOS TEXTBOX  onkeypress="return solonumeros(event)"

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

//NUMERO Y UN PUNTO DECIMAL

function onKeyDecimal(e, thix) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if (document.getElementById(thix.id).value.indexOf('.') != -1 && keynum == 46)
        return false;
    if ((keynum == 8 || keynum == 48 || keynum == 46))
        return true;
    if (keynum <= 47 || keynum >= 58) return false;
    return /\d/.test(String.fromCharCode(keynum));
}
