/// <reference path="../../Scripts/jasmine/jasmine.js" />
/// <reference path="../../Scripts/angular-mocks.js" />
"use strict";

describe("Unit: Testing Open Weather Services", function() {
    var httpbackEnd;
    var _weatherService;

    //initialize Angular
    beforeEach(module("forecast"));
    // Use dependency injection to inject the $httpBackend instead of $http when instantiating the factory
    beforeEach(inject(function(_$httpBackend_, weatherService) {
        httpbackEnd = _$httpBackend_;
        _weatherService = weatherService;
    }));

    afterEach(function() {
        httpbackEnd.verifyNoOutstandingRequest();
        httpbackEnd.verifyNoOutstandingExpectation();
    });

    // Check weatherService service is returned by the forecast module
    it("forecast module should instantiate a weatherService Service with a getWeatherByCity function", function () {
        expect(_weatherService).not.toEqual(null);
        expect(_weatherService.getWeatherByCity).toBeDefined();
        console.info("forecast module should contain a weatherService Service - SUCCESS");
    });

    it("forecastByCity method should call a known URL", function () {
        httpbackEnd.expect("GET", "api/forecast/London,GB").respond(200, { data: "passed" }); // when the API is called with London,GB return expected result data
        // Now verify that the weather service returns the expected result when the method for the service is called
        _weatherService.getWeatherByCity("London,GB").then(function (result) {
            expect(result.status).toEqual(200);
            expect(result.data).toEqual({ data: "passed" });
            console.info("forecastByCity method should exist and return an HTTP 200 - SUCCESS");
        });
        httpbackEnd.flush(); // force any pending requests to be executed
    });
});

// describe = suite declaration
// it = spec
// expect = expectations, these are chained with matchers and can also be chained with .not. if the negative is desired
// toBe (bool), toEqual (value), toMatch (regex), toBeDefined, toBeNull, toContain (str / array)
// full list of matchers here https://jasmine.github.io/2.0/custom_matcher.html