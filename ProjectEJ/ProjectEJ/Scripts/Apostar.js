$(document).ready(function () {
    $(".numero").click(function () {
        var numero = $(this).val();
        var sorteo = $('#sorteo').val();
        var monto = $('#monto').val();
        crearApuesta(numero, sorteo, monto);
    });
});

function crearApuesta(pNumero, pSorteo, pMonto) {
    $.post("/Apuestas/Create", { numero: pNumero, monto: pMonto, sorteo: pSorteo }, function () {
        console.log('completado!');
        window.location = '/Apuestas/Index'
    });
}