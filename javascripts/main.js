;"use strict";
(function($){
	var getSha = function() {
		var url = "https://api.github.com/repos/aliencube/CryptoService/git/refs/heads/gh-pages";
		$.ajax({
		    type: "GET",
		    url: url,
			dataType: "json"
		})
		.done(function(data) {
			getBuilds(data.object.sha);
		});
	};
	
	var getBuilds = function(sha) {
		// var url = "";
		// $.ajax({
		    // type: "GET",
		    // url: url,
			// dataType: "json"
		// })
		// .done(function(data) {
			// getBuilds(data.object.sha);
		// });
	};

	$(document).ready(function() {
		getSha();
	
		$("#latest").html("<li>HelloWorld</li>");
		$("#archived").html("<li>Archived</li>");
	});
})(jQuery);
