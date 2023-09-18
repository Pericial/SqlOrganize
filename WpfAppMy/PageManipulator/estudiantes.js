//https://es.javascript.info/formdata
///inicial/index4.php?a=7&b=1 Carga adicional no guarda al alumno, sirve para verificar en que comision esta
///inicial/index4.php?a=7&b=2 Registro de alumno, si no se registra dispara Error 92

var data = [
	{
		"apellido": "BOTTEGA",
		"nombre": "ANDRES",
		"cuil1": "",
		"dni_cargar": "44851339",
		"cuil2": "",
		"sexo": "1",
		"dia_nac": "15/5/2003",
		"mes_nac": "15 de mayo",
		"ano_nac": "2003",
		"category": "1",
		"subcategory": "10066",
		"verifica_session": "0"
	},
	{
		"apellido": "Gutierrez",
		"nombre": "Vanessa elizabeth",
		"cuil1": "",
		"dni_cargar": "26708464",
		"cuil2": "",
		"sexo": "2",
		"dia_nac": "30/6/1978",
		"mes_nac": "30 de junio",
		"ano_nac": "1978",
		"category": "1",
		"subcategory": "10066",
		"verifica_session": "0"
	}
];

var formData = new FormData();


for (ii in data) { 
	try { throw ii }
	catch (i) {

		formData.set("apellido", data[i]["apellido"]);
		formData.set("nombre", data[i]["nombre"]);
		formData.set("cuil1", data[i]["cuil1"]);
		formData.set("dni_cargar", data[i]["dni_cargar"]);
		formData.set("cuil2", data[i]["cuil2"]);
		formData.set("sexo", data[i]["sexo"]);
		formData.set("dia_nac", data[i]["dia_nac"]);
		formData.set("mes_nac", data[i]["mes_nac"]);
		formData.set("ano_nac", data[i]["ano_nac"]);
		formData.set("category", data[i]["category"]);
		formData.set("subcategory", data[i]["subcategory"]);
		formData.set("verifica_session", data[i]["verifica_session"]);

		/*for (const pair of formData.entries()) {
			console.log(`${pair[0]}, ${pair[1]}`);
		}
		continue;*/


		fetch('/inicial/index4.php?a=7&b=1', {
			method: 'POST',
			body: formData
		}).then(function (response) {
			// The API call was successful!
			return response.text();

		}).then(function (html) {
			// This is the HTML from our response as a text string
			if (html.includes("Ha ocurrido un error 192")) {
				console.log(data[i]["dni_cargar"] + ": Error 192.");
			} else if (html.includes("fue inscripto")) {
				console.log(data[i]["dni_cargar"] + ": Fue inscripto.");
			} else if (html.includes("Ya existe Estudiante")) {
				var description = html.substring(
					html.indexOf("DNI en la Comisión ") + 19, 
					html.lastIndexOf("!</h2>")
				);
				console.log(data[i]["dni_cargar"] + ": " + data[i]["subcategory"] + " > " + description);

			} else {
				console.log(data[i]["dni_cargar"] + ": No especificado");

			}
		}).catch(function (err) {
			// There was an error
			console.warn('Something went wrong.', err);
		});
	}
}
