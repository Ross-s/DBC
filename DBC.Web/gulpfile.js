const gulp = require('gulp');
const webpack = require('webpack-stream');
const path = require('path');
var postcss = require('gulp-postcss');




function buildJs () {
    return gulp.src("wwwroot/js/**/*.tsx")
        .pipe(webpack({
            mode: "development",
            entry: {
                app: path.resolve(__dirname, "wwwroot/js/app.tsx"),
            },
            module: {
                rules: [
                    {
                        test: /\.(js|ts)x?$/,
                        exclude: /node_modules/,
                        use: ["babel-loader"],
                    },
                    {
                        test: /\.css$/,
                        exclude: /node_modules/,
                        use: [
                            "style-loader",
                            "css-loader",
                            "postcss-loader",
                        ]
                    },
                    {
                        test: /\.(png|svg|jpg|gif)$/,
                        exclude: /node_modules/,
                        use: ["file-loader"]
                    },
                ],
            },
            output: {
                path: path.resolve(__dirname, "wwwroot/dist"),
                filename: '[name].js',
            },
            resolve: {
                modules: [__dirname, "src", "node_modules"],
                extensions: ["*", ".js", ".jsx", ".tsx", ".ts", ".css"],
            },
        }))
        .pipe(gulp.dest('wwwroot/dist/'));
}
function buildCss() {
    return gulp.src("wwwroot/main.css")
        .pipe(postcss())
        .pipe(gulp.dest('wwwroot/dist/'));
}

exports.buildCss = buildCss;
exports.buildJs = buildJs;
exports.default = gulp.parallel(buildJs, buildCss);
exports.watch = function () {
    gulp.watch('wwwroot/js/**/*.tsx', gulp.parallel(buildJs, buildCss));
}