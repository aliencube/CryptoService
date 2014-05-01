;"use strict";

(function ($) {
    // Gets the README.md.
    var getReadme = function() {
        var url = "https://api.github.com/repos/aliencube/CryptoService/readme";
        $.ajax({
                type: "GET",
                url: url,
                dataType: "json"
            })
            .done(function(data) {
                var decoded = atob(data.content);
                markdownToHtml(decoded);
            });
    };

    // Converts the README.md markdown to HTML and put them into the HTML element.
    var markdownToHtml = function(markdown) {
        var url = "https://api.github.com/markdown";
        var params = {
            "mode": "gfm",
            "text": markdown
        };
        $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(params),
                dataType: "html"
            })
            .done(function(data) {
                $("#main_content").html(data);
            });
    };

    // Gets the latest commit ID of the gh-pages branch.
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

    // Getst the list of release files and put them into HTML elements.
    var getBuilds = function(sha) {
        var url = "https://api.github.com/repos/aliencube/CryptoService/git/trees/" + sha;
        $.ajax({
                type: "GET",
                url: url,
                data: { "recursive": 1 },
                dataType: "json"
            })
            .done(function(data) {
                var libraries = Enumerable.From(data.tree)
                    .Where("$.type == 'blob'")
                    .OrderByDescending("$.path")
                    .Select("$.path")
                    .Where(function(p) {
                        return p.match(/^downloads\/CryptoService\-\d+\.\d+\.\d+\.\d+\.zip$/i)
                    })
                    .ToArray();
                var apps = Enumerable.From(data.tree)
                    .Where("$.type == 'blob'")
                    .OrderByDescending("$.path")
                    .Select("$.path")
                    .Where(function(p) {
                        return p.match(/^downloads\/CryptoService\.ConsoleApp\-\d+\.\d+\.\d+\.\d+\.zip$/i)
                    })
                    .ToArray();

                $("#latest-library").append(
                    $("<li></li>").append(
                        $("<a></a>").attr("href", libraries[0]).text(libraries[0].replace("downloads/", ""))
                    ));
                $("#latest-app").append(
                    $("<li></li>").append(
                        $("<a></a>").attr("href", apps[0]).text(apps[0].replace("downloads/", ""))
                    ));

                Enumerable.From(libraries).Skip(1).ForEach(function(library, index) {
                    $("#archived-library").append(
                        $("<li></li>").append(
                            $("<a></a>").attr("href", library).text(library.replace("downloads/", ""))
                        ));
                });
                Enumerable.From(apps).Skip(1).ForEach(function(app, index) {
                    $("#archived-app").append(
                        $("<li></li>").append(
                            $("<a></a>").attr("href", app).text(app.replace("downloads/", ""))
                        ));
                });
            });
    };

    $(document).ready(function () {
        if ($("#main_content").hasClass("index")) {
            getReadme();
        } else if ($("#main_content").hasClass("downloads")) {
            getSha();
        }
    });
})(jQuery);
