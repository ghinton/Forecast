/// <reference path="../../Scripts/jasmine/jasmine.js" />
/// <reference path="../../Scripts/angular-mocks.js" />
"use strict";

describe("Unit: Testing forecastController", function () {
    var log;
    var scope;
    var weatherService;
    var resources;
    var controller;

    //initialize Angular
    beforeEach(module("forecast"));

    beforeEach(inject(
        // inject a new instance of rootscope
        function (_$controller_, _$rootScope_, _$log_) {
            log = _$log_;
            scope = _$rootScope_.$new();
            controller = _$controller_("forecastCtrl", { $scope: scope });
        }
    ));

    // Write any angular debug messages to the console as by default Karma won't spit them out
    afterEach(function () {
        console.log(log.debug.logs);
    });

    it("default properties and methods defined", function () {
        console.info("checking properties and methods exist");
        expect(controller.isoCountryList).toBeDefined();
        expect(controller.countryAndCitySet).toBeDefined();
        expect(controller.selectedCountryCodeSet).toBeDefined();
        expect(controller.getForecast).toBeDefined();
        expect(controller.forecastFetched).toBeDefined();
        expect(controller.cityForecast).toBeDefined();

        console.info("checking default property values");
        expect(controller.forecastFetched).toBe(false);

        expect(scope).toBeDefined();
        expect(scope.countryResult).toBeDefined();
        expect(scope.countryOptions).toBeDefined();
        expect(scope.countryDetails).toBeDefined();

        console.info("default properties and methods defined - SUCCESS");
    });

    it("calls weather service when calling controller", function () {
        // TBC
    });

    it("sets forecastFetched to true and calls the digest cycle of rootscope when weather service returns successfully", function () {
        // TBC
    });

    it("sets forecastFetched to false when weather service encounters an exception", function () {
        // TBC
    });
});