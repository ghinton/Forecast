(function () {
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
})();