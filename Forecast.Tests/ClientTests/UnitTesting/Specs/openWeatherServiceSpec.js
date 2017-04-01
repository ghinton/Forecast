/// <reference path="../../Scripts/jasmine/jasmine.js" />
/// <reference path="../../Scripts/angular-mocks.js" />
(function () {
    "use strict";

    describe("Unit: Testing Open Weather Services", function () {
        var $httpbackEnd;
        var weatherService;

        //initialize Angular
        beforeEach(angular.mock.module("forecast"));
        beforeEach(angular.mock.inject(function (_$httpbackend, _weatherService) {
            $httpbackEnd = _$httpbackEnd;
            weatherService = _weatherService;
            $httpbackEnd.whenGET("/api/forecast/*").respond(200, { result: true });
            $httpbackEnd.flush(); // flush the pending request
        }));

        afterEach(function () {
            $httpbackEnd.verifyNoOutstandingRequest();
            $httpbackEnd.verifyNoOutstandingExpectation();
        });

        it("forecast module should contain a weatherService Service", function () {
            angular.mock.inject(function (weatherService) {
                expect(weatherService).not.toEqual(null);
            })
        });

        describe("ForecastByCity Service", function () {
            var svc = null;

            var scope;
            beforeEach(angular.mock.inject(
                function ($controller, $rootScope) {
                    scope = $rootScope.$new();
                    var ctrl = $controller("forecastCtrl", { $scope: scope });
                }
            ));

            it("Initial Value is 5",
                function () {
                    expect(scope.value).toBe(5);
                }
            );

            it("sanity check", function () {
                expect(0).toBe(0);
            });
        });
    });



    /*(function () {
        "use strict";

        // OpenWeather API Service call
        angular
            .module("forecast") // get the module (not redefine it)
            .factory("weatherService", ["$http", weatherService]);

        function weatherService($http) {
            // Return the methods we want to expose
            return {
                // begin factory code
                getWeatherByCity: function (cityName) {
                    var url = "api/forecast/" + cityName;
                    return $http({
                        method: "GET",
                        url: url,
                        data: cityName
                    });
                }
            };
        }
    })();*/

    // describe = suite declaration
    // it = spec
    // expect = expectations, these are chained with matchers and can also be chained with .not. if the negative is desired
    // toBe (bool), toEqual (value), toMatch (regex), toBeDefined, toBeNull, toContain (str / array)
        // full list of matchers here https://jasmine.github.io/2.0/custom_matcher.html
})();