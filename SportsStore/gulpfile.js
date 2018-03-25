"use strict";

var gulp = require("gulp");

gulp.task(
    "copy:bootstrap",
    function () {
        var path = "bootstrap/dist";

        return gulp.src(["node_modules/" + path + "/**/*.min.*s", "!node_modules/" + path + "/**/bootstrap.bundle.min.js"])
            .pipe(gulp.dest("wwwroot/lib/" + path));
    }
);
gulp.task(
    "copy:jquery",
    function () {
        var path = "jquery/dist";

        return gulp.src("node_modules/" + path + "/jquery.min.js")
            .pipe(gulp.dest("wwwroot/lib/" + path));
    }
);
gulp.task(
    "copy:jquery-validation",
    function () {
        var path = "jquery-validation/dist";

        return gulp.src(["node_modules/" + path + "/**/*.js", "!node_modules/" + path + "/additional-methods.js", "!node_modules/" + path + "/jquery.validate.js"])
            .pipe(gulp.dest("wwwroot/lib/" + path));
    }
);
gulp.task(
    "copy:jquery-validation-unobtrusive",
    function () {
        var path = "jquery-validation-unobtrusive/dist";

        return gulp.src("node_modules/" + path + "/jquery.validate.unobtrusive.min.js")
            .pipe(gulp.dest("wwwroot/lib/" + path));
    }
);
gulp.task(
    "copy:popper.js",
    function () {
        var path = "popper.js/dist/umd";

        return gulp.src("node_modules/" + path + "/*.min.js")
            .pipe(gulp.dest("wwwroot/lib/" + path));
    }
);
gulp.task(
    "copy:fontawesome",
    function () {
        var path = "@fortawesome/fontawesome-free-webfonts";

        return gulp.src(["node_modules/" + path + "/**/*", "!node_modules/" + path + "/less/*", "!node_modules/" + path + "/scss/*", "!node_modules/" + path + "/*"])
            .pipe(gulp.dest("wwwroot/lib/fontawesome"));
    }
);

gulp.task(
    "copy",
    ["copy:bootstrap", "copy:jquery", "copy:jquery-validation", "copy:jquery-validation-unobtrusive", "copy:popper.js", "copy:fontawesome"]
);
