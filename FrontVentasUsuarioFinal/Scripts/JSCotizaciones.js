$(document).ready(function () {

    $('#RTable').hide();
    $('#BtnImportarExcel').hide();
    $('#IdCliente').attr("required", false);

  
 

    $(function () {
        //$("#FechaInicial").datepicker({ dateFormat: 'dd/mm/yy' });
        //$("#FechaFinal").datepicker({ dateFormat: 'dd/mm/yy' });      

        $("#FechaInicial").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#FechaFinal").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#Fecha").datepicker({ dateFormat: 'yy-mm-dd' }); 
        
    });


    $('#TablaCotizaciones').DataTable({
        language: {
            url: '/Scripts/spanish.json'
        }
        //"pageLength": 20
    });   


    //$('#TablaCotizaciones').DataTable({
    //    dom: 'Bfrtip',
    //    buttons: [
    //        'excel'
    //    ]
    //});
    
});


$("#BtnBuscarCotizaciones").click(function () {

    $('#RTable').show();
    $('#RAviso').hide();

    $("#FechaInicial").removeAttr('disabled');
    $("#FechaFinal").removeAttr('disabled');
    $("#Sucursal").removeAttr('disabled');
    $("#Estatus").removeAttr('disabled');
    $("#IDCotizacion").removeAttr('disabled');
    


    ValidarListaCotizaciones();

  
});


function ValidarListaCotizaciones() {
    
    var DatoCotizacion = $("#IDCotizacion").val();
    var DatoFechaInicial = $("#FechaInicial").val();
    var DatoFechaFinal = $("#FechaFinal").val();
    var DatoSucursal = $("#Sucursal").val();
    var DatoEstatus = $("#Estatus").val();

    if (DatoCotizacion != "") {

        $.ajax({
            url: '/Cotizaciones/MostrarCotizacionUnica',
            type: 'POST',
            data: {
                ClaveCotizacion: DatoCotizacion

            },
            dataType: 'json'

        }).done(function (Data) {


            if (Data.IdNoexiste == -1) {

                $("#MensajeError").append('<div class="alert alert-danger alert-dismissible" role="alert">\
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>'
                    + Data.MensajeNoExiste + ' </div>');
            }
            else {
                LlenarTablaCotizacionesUnico(Data);

                $('#BtnImportarExcel').show();
            }

            //alert(JSON.stringify(result));                 
                 

        });
    }
    else {
        //alert(DatoFechaInicial + " " + DatoFechaFinal + " " + DatoSucursal + " " + DatoEstatus)

        if (DatoFechaInicial == "" && DatoFechaFinal == "" && DatoSucursal == "" && DatoEstatus == "") {
            $.ajax({
                url: '/Cotizaciones/MostrarCotizaciones',
                type: 'POST',
                data: {
                    FechaInicial: '2000-01-01',
                    FechaFinal: '2000-01-01',
                    Sucursal: 0,
                    Estatus: 0
                },
                dataType: 'json'

            }).done(function (result) {

                //alert(result);

                LlenarTablaCotizaciones(result);
                $('#BtnImportarExcel').show();

            });

        }
        else if (DatoFechaInicial != "" && DatoFechaFinal != "" && DatoSucursal != "" && DatoEstatus != "") {
            $.ajax({
                url: '/Cotizaciones/MostrarCotizaciones',
                type: 'POST',
                data: {
                    FechaInicial: DatoFechaInicial,
                    FechaFinal: DatoFechaFinal,
                    Sucursal: DatoSucursal,
                    Estatus: DatoEstatus
                },
                dataType: 'json'

            }).done(function (result) {

              

                LlenarTablaCotizaciones(result);
                $('#BtnImportarExcel').show();

            });
        }
        else {
            alert("Por favor ingrese todos los parametros para realizar la nueva busqueda");
        }



    }   
}






//Llenado dinamico de tabla de cotizaciones
function LlenarTablaCotizaciones(coleccion) {

    var t = $('#TablaCotizaciones').DataTable();


    t.clear().draw();



    for (var i = 0; i < coleccion.length; i++) {

        if (coleccion[i].IdEstatus == 3) {
            t.row.add([
                coleccion[i].Fecha,
                coleccion[i].Sucursal,
                coleccion[i].Cliente,
                coleccion[i].DescripcionCotizacion,
                coleccion[i].Estatus,
                //'<a href=Cotizaciones/VerCotizacion?CotizacionId=' + coleccion[i].IdCotizacion + ' class="btn btn-info BtnVerCotizacion"><font face="Open Sans, Helvetica, Arial"> Ver </font></a>',
                '<button type="button" class="btn btn-info BtnVerCotizacion" id=' + coleccion[i].IdCotizacion + ' > Ver</button>',
                '<button type="button" class="btn btn-primary BtnPorCotizacion" id=' + coleccion[i].IdCotizacion + ' > Actualizar</button>',
            ]).draw(false);

        }
        else {
            t.row.add([
                coleccion[i].Fecha,
                coleccion[i].Sucursal,
                coleccion[i].Cliente,
                coleccion[i].DescripcionCotizacion,
                coleccion[i].Estatus,
                //'<a href=Cotizaciones/VerCotizacion?CotizacionId=' + coleccion[i].IdCotizacion + ' class="btn btn-info BtnVerCotizacion"><font face="Open Sans, Helvetica, Arial"> Ver </font></a>',
                '<button type="button" class="btn btn-info BtnVerCotizacion" id=' + coleccion[i].IdCotizacion + ' > Ver</button>',
                '<button type="button" class="btn btn-primary BtnPorCotizacion" id=' + coleccion[i].IdCotizacion + ' disabled> Actualizar</button>',
            ]).draw(false);
        }
       

    }
}

function LlenarTablaCotizacionesUnico(coleccion) {

    var t = $('#TablaCotizaciones').DataTable();


    t.clear().draw();
         
    if (coleccion.IdEstatus == 3) {
        t.row.add([
            coleccion.Fecha,
            coleccion.Sucursal,
            coleccion.Cliente,
            coleccion.DescripcionCotizacion,
            coleccion.Estatus,
            //'<a href=Cotizaciones/VerCotizacion?CotizacionId=' + coleccion.IdCotizacion + ' class="btn btn-info BtnVerCotizacion"><font face="Open Sans, Helvetica, Arial"> Ver </font></a>',
            '<button type="button" class="btn btn-info BtnVerCotizacion" id=' + coleccion.IdCotizacion + ' > Ver</button>',
            '<button type="button" class="btn btn-primary BtnPorCotizacion" id=' + coleccion.IdCotizacion + ' > Actualizar</button>',
            //'<a href=Cotizaciones/ActualizarCotizacion?CotizacionId=' + coleccion.IdCotizacion + ' class="btn btn-primary"><font face="Open Sans, Helvetica, Arial"> Editar</font></a>',
        ]).draw(false);
    } else {
        t.row.add([
            coleccion.Fecha,
            coleccion.Sucursal,
            coleccion.Cliente,
            coleccion.DescripcionCotizacion,
            coleccion.Estatus,
            //'<a href=Cotizaciones/VerCotizacion?CotizacionId=' + coleccion.IdCotizacion + ' class="btn btn-info BtnVerCotizacion"><font face="Open Sans, Helvetica, Arial"> Ver </font></a>',
            '<button type="button" class="btn btn-info BtnVerCotizacion" id=' + coleccion.IdCotizacion + ' > Ver</button>',
            '<button type="button" class="btn btn-primary  BtnPorCotizacion" id=' + coleccion.IdCotizacion + ' disabled> Actualizar</button>',
            //'<a href=Cotizaciones/ActualizarCotizacion?CotizacionId=' + coleccion.IdCotizacion + ' class="btn btn-primary"><font face="Open Sans, Helvetica, Arial"> Editar</font></a>',
        ]).draw(false);
    }
              

}


$("#BtnImportarExcel").click(function () {

    ValidarExportacionExcel();
});

function ValidarExportacionExcel() {

    var DatoFechaInicial = $("#FechaInicial").val();
    var DatoFechaFinal = $("#FechaFinal").val();
    var DatoSucursal = $("#Sucursal").val();
    var DatoEstatus = $("#Estatus").val();
    var DatoCotizacion = $("#IDCotizacion").val();

    //alert(DatoFechaInicial + " " + DatoFechaFinal + " " + DatoSucursal + " " + DatoEstatus);

   
    if (DatoFechaInicial == "" && DatoFechaFinal == "" && DatoSucursal == "" && DatoEstatus == "" && DatoCotizacion == "") {
        $.ajax({
            url: '/Cotizaciones/ObtenerDatosParaExcel',
            type: 'POST',
            data: {
                FechaInicial: '2000-01-01',
                FechaFinal: '2000-01-01',
                Sucursal: 0,
                Estatus: 0,
                Cotizacion: ''
            },
            dataType: 'json',
            success: function (data) {

                window.location = '/Cotizaciones/CrearExcel?FechaInicial=' + data.FInicial + '&FechaFinal=' + data.FFinal + '&Sucursal=' + data.Suc + '&Estatus=' + data.Est + '&Cotizacion=' + data.Cot;

            }

        });

    }
    else if (DatoFechaInicial != "" && DatoFechaFinal != "" && DatoSucursal != "" && DatoEstatus != "") {
        $.ajax({
            url: '/Cotizaciones/ObtenerDatosParaExcel',
            type: 'POST',
            data: {
                FechaInicial: DatoFechaInicial,
                FechaFinal: DatoFechaFinal,
                Sucursal: DatoSucursal,
                Estatus: DatoEstatus,
                Cotizacion: ''
            },
            dataType: 'json',
            success: function (data) {

               

                window.location = '/Cotizaciones/CrearExcel?FechaInicial=' + data.FInicial + '&FechaFinal=' + data.FFinal + '&Sucursal=' + data.Suc + '&Estatus=' + data.Est + '&Cotizacion=' + data.Cot;

        }

        });
    }
    else if (DatoCotizacion != "") {
        $.ajax({
            url: '/Cotizaciones/ObtenerDatosParaExcel',
            type: 'POST',
            data: {
                FechaInicial: '2000-01-01',
                FechaFinal: '2000-01-01',
                Sucursal: 0,
                Estatus: 0,
                Cotizacion: DatoCotizacion
            },
            dataType: 'json',
            success: function (data) {

                //alert(data.FInicial + " " + data.FFinal + " " + data.Suc + " " + data.Est);

                window.location = '/Cotizaciones/CrearExcel?FechaInicial=' + data.FInicial + '&FechaFinal=' + data.FFinal + '&Sucursal=' + data.Suc + '&Estatus=' + data.Est + '&Cotizacion=' + data.Cot;

            }

        });
    }
    else {
        alert("Por favor ingrese todos los parametros para realizar la nueva busqueda");
    }
    
}

$("#IDCotizacion").click(function () {

    $("#FechaInicial").val("");
    $("#FechaFinal").val("");
    $("#Sucursal").val("");
    $("#Estatus").val("");

    $("#FechaInicial").attr('disabled', 'disabled');
    $("#FechaFinal").attr('disabled', 'disabled');
    $("#Sucursal").attr('disabled', 'disabled');
    $("#Estatus").attr('disabled', 'disabled');

    $('#BtnImportarExcel').hide();
});


$("#FechaInicial").click(function () {
    $("#IDCotizacion").val("");   
    $("#IDCotizacion").attr('disabled', 'disabled');
    $('#BtnImportarExcel').hide();
});

$("#FechaFinal").click(function () {
    $("#IDCotizacion").val("");
    $("#IDCotizacion").attr('disabled', 'disabled');
    $('#BtnImportarExcel').hide();
});

$("#Sucursal").click(function () {
    $("#IDCotizacion").val("");
    $("#IDCotizacion").attr('disabled', 'disabled');
    $('#BtnImportarExcel').hide();
});

$("#Estatus").click(function () {
    $("#IDCotizacion").val("");
    $("#IDCotizacion").attr('disabled', 'disabled');
    $('#BtnImportarExcel').hide();
});


function ConfirmSave() {
    var respuesta = confirm("¿Esta seguro de guardar una nueva cotización?")
    if (respuesta == true) {
        return true;
    }
    else {
        return false;
    }
}


function ConfirmUpdate() {
    var respuesta = confirm("Esta a punto de actualizar el estaus de una cotización ¿desea continuar?")
    if (respuesta == true) {
        return true;
    }
    else {
        return false;
    }
}

function ConfirmCancel() {
    var respuesta = confirm("Los datos aún no ha sido guardados, ¿Esta seguro de cancelar la captura?")
    if (respuesta == true) {
        return true;
    }
    else {
        return false;
    }
}

function ConfirmCancelUpdate() {
  alert("La cotización no ha sido actualizada")
    //if (respuesta == true) {
    //    return true;
    //}
    //else {
    //    return false;
    //}
}

$(document).on('click', '.BtnPorCotizacion', function () {

   
    var IdSeleccionado = this.id;


    $("#ContenidoActualizarCotizacion").empty();

    //Cotizaciones / ActualizarCotizacion ? CotizacionId = '

    $.ajax({
        url: '/Cotizaciones/ActualizarCotizacion',
        type: 'GET',
        dataType: 'html',
        data: {
            CotizacionId: IdSeleccionado           
        }
    }).done(function (data) {

        $("#ContenidoActualizarCotizacion").html(data);

    });

    $("#ModalActualizar").modal("show");
});

$(document).on('click', '.BtnVerCotizacion', function () {


    var IdSeleccionado = this.id;


    $("#ContenidoVerCotizacion").empty();

    //Cotizaciones / ActualizarCotizacion ? CotizacionId = '

    $.ajax({
        url: '/Cotizaciones/VerCotizacion',
        type: 'GET',
        dataType: 'html',
        data: {
            CotizacionId: IdSeleccionado
        }
    }).done(function (data) {

        

        $("#ContenidoVerCotizacion").html(data);

    });

    $("#ModalVer").modal("show");
});


$('#ModalActualizar').on('change', '#IdEstatus', function () {

    var seleccion = $("#IdEstatus").val();

    if (seleccion == 2) {
        $('#ModalActualizar').find('#MotivoRechazo').attr("required", true);
        $('#ModalActualizar').find('#MotivoRechazo').attr('readonly', false);
        
    }
    else {
        $('#ModalActualizar').find('#MotivoRechazo').attr("required", false);
        $('#ModalActualizar').find("#MotivoRechazo").val("");
        $('#ModalActualizar').find('#MotivoRechazo').attr('readonly', true);
        
    }

});


