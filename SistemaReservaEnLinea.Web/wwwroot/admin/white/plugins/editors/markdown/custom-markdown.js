// Basic
var simplemde = new SimpleMDE({
	element: document.getElementById("txtDescripcion"),
	spellChecker: false,
	autosave: {
		enabled: true,
		unique_id: "txtDescripcion",
		delay: 500
	}
});

simplemde.codemirror.on("change", function () {
	console.log(simplemde.value());
	$("#txtDescripcion").val(simplemde.value());
});

// Autosaving
//new SimpleMDE({
//	element: document.getElementById("demo2"),
//	spellChecker: false,
//	autosave: {
//		enabled: true,
//		unique_id: "demo2",
//	},
//});