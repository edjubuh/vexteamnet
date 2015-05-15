var gulp = require('gulp');
var bower = require('gulp-bower');

gulp.task("install", function () {
    return bower({ layout: "byComponent" })
        .pipe(gulp.dest('wwwroot/lib'));
});