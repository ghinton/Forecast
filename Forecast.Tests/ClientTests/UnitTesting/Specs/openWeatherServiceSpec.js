/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/../../Forecast3/Forecast/app/services/openWeather.js" />
describe("Unit: Testing Open Weather Services", function () {
    //initialize Angular
    beforeEach(function () {
        module("forecast");
    });

    it("forecast module should contain a weatherService Service",
        inject(function(weatherService) {
            expect(weatherService).not.toEqual(null);
        })
    );

    describe("ForecastByCity Service", function () {
        var svc = null;

        var scope;
        beforeEach(inject(
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