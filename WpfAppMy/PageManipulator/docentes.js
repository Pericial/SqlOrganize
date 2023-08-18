var h1 = document.getElementById('subcategory').getElementsByTagName("h1");
var h4 = document.getElementById('subcategory').getElementsByTagName("h1");

var inputList = Array.prototype.slice.call(h1);

var response = [];

inputList.forEach((docente) => {
    var r = {}
    var docente_array = docente.textContent.split("DNI:");
    r["numero_documento"] = docente_array[1].trim();
    var nombre_completo = docente_array[0].split(",");
    r["nombres"] = nombre_completo[1].trim();
    r["apellidos"] = nombre_completo[0].trim();

    var info_docente = docente.nextElementSibling;

    /*
    Se crea un array con la siguiente estructura
    ['Dirección: 39 #978  La Plata', '\t Teléfono Celular: 221 4550932', '     Email: gabzeballosrodriguez@abc.gob.ar']
    https://stackoverflow.com/questions/21711768/split-string-in-javascript-and-detect-line-break
    */
    var info_docente_split = info_docente.textContent.split(/\r?\n|\r|\n/g);
    r["domicilio"] = info_docente_split[0].split(":")[1].trim();
    let matches = info_docente_split[1].split(":")[1].trim().replaceAll(/\s/g, '').match(/(\d+)/);
    r["movil"] = (matches) ? matches[0] : null;
    r["email"] = info_docente_split[2].split(":")[1].trim();
    r["cargos"] = [];


    /*
    info_cargos posee  la siguiente estructura
    ['Dirección: 39 #978  La Plata', '\t Teléfono Celular: 221 4550932', '     Email: gabzeballosrodriguez@abc.gob.ar']
    https://stackoverflow.com/questions/21711768/split-string-in-javascript-and-detect-line-break
    */
    var info_cargos = info_docente.nextElementSibling.nextElementSibling;

    while (info_cargos && info_cargos.textContent.includes("Comisión")) {
        info_cargos_array = info_cargos.textContent.split("Comisión");
        r["cargos"].push({
            "comision": info_cargos_array[1].trim(),
            "codigo": info_cargos_array[0].substring(
                info_cargos_array[0].indexOf("(") + 1,
                info_cargos_array[0].lastIndexOf(")")
            )
        })

        info_cargos = info_cargos.nextElementSibling;
    }

    console.log(r);
    response.push(r);
});