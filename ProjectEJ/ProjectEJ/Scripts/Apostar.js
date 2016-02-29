$(document).ready(function () {
    $(".numero").click(function () {
        var numero = $(this).val();
        var sorteo = $('#sorteo').val();
        var monto = $('#monto').val();
        crearApuesta(numero, sorteo, monto);
    });
});

function crearApuesta(pNumero, pSorteo, pMonto) {
    $.post("/Apuestas/Create", { numero: pNumero, monto: pMonto, sorteo: pSorteo }, function (status) {
        if (status.Status == "1") {
            alert("Su apuesta a sido registrada al número: " + status.Numero + " con el monto: " + status.Monto + " colones");
        } else {
            alert("No se puede apostar esa cantidad de dinero el monto máximo de apuesta es: " + status.MontoSugerido + " colones");
        }
        
        window.location = '/Apuestas/Index'
    });
}