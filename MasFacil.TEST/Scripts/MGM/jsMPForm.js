
//ESTA FUNCION ENTRA EN EL RADMENU DESDE LA PAGINA DE INICIO , SE TOMA EL VALOR QUE ARROJA EL VALUE DE CADA UNA DE LAS OPCIONES YA SE (opc, cia)
// Y MANDA LLAMAR EL RADMENU CON EL NOMBRE CORRESPONDIENTE
function copyPasterItemClicked(sender, args) {
    var itemValue = args.get_item().get_value()

    switch (itemValue) {
        case "opc":

            var rop = radopen("", "RADOPC", 600, 600);

            rop;
            return

            break;

        case "cia":
            var opcia = radopen("", "RADCIA", 380, 180);
            opcia;
            return;
            break;
        case "ses":
            window.location.href = '/Login.aspx';
            break;
    }
}