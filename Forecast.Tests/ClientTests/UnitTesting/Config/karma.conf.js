// Karma configuration
// Generated on Sat Apr 01 2017 12:06:22 GMT+0100 (GMT Summer Time)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '../',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
        // Angular Framework files
        "../../../Forecast/Scripts/jquery-3.1.1.js",
        "../../../Forecast/Scripts/angular.js",
        "../../../Forecast/Scripts/angular-sanitize.js",
        "../../../Forecast/Scripts/bootstrap.js",
        "../../Scripts/angular-mocks.js", // use version shipped with Jasmine

        // All app js files from the web project
        '../../../Forecast/app/**/*.js',

        // Test Data files
        //'TestData/**/*.js',

        // Unit Test files
        'Specs/**/*.js'

    ],


    // list of files to exclude
    exclude: [
    ],

    // Loading external plugins to extend Karma
    plugins:[
        //By default, Karma loads all sibling NPM modules which have a name starting with karma-*. - NOTE THAT IF THEY'RE NOT ALL PREFIXED KARMA THEY WON'T WORK
        "karma-jasmine",
        "karma-phantomjs-launcher",
        "karma-chrome-launcher"
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {

    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['dots','progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: false,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['Chrome'], // 'PhantomJS'


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: true,

    // Concurrency level
    // how many browser should be started simultaneous
    concurrency: 1,

    // Level of logging
    logLevel: config.LOG_INFO
  })
}
