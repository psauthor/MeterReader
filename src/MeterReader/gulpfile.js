/// <binding Clean='minify' ProjectOpened='libs, minify' />
const { src, dest, series } = require('gulp');
const uglify = require('gulp-uglify');
const concat = require('gulp-concat');
const rimraf = require("rimraf");
const merge = require('merge-stream'); 

function minify() {
  return src("wwwroot/js/**/*.js")
    .pipe(uglify())
    .pipe(concat("reader.min.js"))
    .pipe(dest("wwwroot/dist"));

}

function clean(cb) {
  rimraf("wwwroot/lib/", cb);
  rimraf("wwwroot/app/", cb);
}

function libs() {

  var deps = {
    "jquery": {
      "dist/*": ""
    },
    "bootstrap": {
      "dist/**/*": ""
    },
    "jquery-validation": {
      "dist/**/*": ""
    },
    "jquery-validation-unobtrusive": {
      "dist/**/*": ""
    },
    "@fortawesome/fontawesome-free": {
      "js/**.*": "js",
      "svgs/**/*": "svgs"
    }
  };

  var streams = [];

  for (var prop in deps) {
    console.log("Prepping Scripts for: " + prop);
    for (var itemProp in deps[prop]) {
      streams.push(src("node_modules/" + prop + "/" + itemProp)
        .pipe(dest("wwwroot/lib/" + prop + "/" + deps[prop][itemProp])));
    }
  }

  return merge(streams);
}

exports.minify = minify;
exports.default = series(clean, minify, libs);
exports.libs = libs;
exports.clean = clean;