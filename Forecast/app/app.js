(function () {
    "use strict";
    angular
        .module("forecast", ["ngAutocomplete", "ngSanitize"]) // preload Google places autocomplete
        .controller("forecastCtrl", ["$scope", "weatherService", "resources", forecastController]);

    function forecastController($scope, weatherService, resources) {
        var vm = this; // view model

        // View Model Controller functions
        vm.isoCountryList = resources.ISOCOUNTRYLIST;
        vm.countryAndCitySet = countryAndCitySet;
        vm.selectedCountryCodeSet = selectedCountryCodeSet;
        vm.getForecast = getForecast;
        vm.forecastFetched = false;

        // Google Autocomplete
        $scope.countryResult = "";
        $scope.countryOptions = {
            country: "",
            types: "(cities)"
        };
        $scope.countryDetails = ""; // returns the geocode place


        vm.cityForecast = [];

        function getForecast() {
            var selectedCountry = $scope.countryOptions.country.trim();
            var selectedCity = $scope.countryDetails.name.trim();
            // Now call the .Net webservice via angular to retrieve the 5 day forecast JSON
            weatherService.getWeatherByCity(selectedCity + "," + selectedCountry).then(forecastLoaded, onFailure);
        }

        function countryAndCitySet() {
            return selectedCountryCodeSet && txtCity.value !== "";
        }

        function selectedCountryCodeSet() {
            var sc = $scope.countryOptions.country;
            if (sc.length === 2 && vm.isoCountryList.filter(function (country) { return country.code === sc; }).length > 0) {
                return true;
            }
            return false;
        }

        function forecastLoaded(forecastObj) {
            vm.cityForecast = forecastObj.data;
            vm.forecastFetched = true;
            // City class plus Days dictionary keyed by date in dd-MMM-yyyy format; each day has a dictionary of 8 ForecastDay3HourSlice objects
            $rootScope.$apply();
        }

        function onFailure(err) {
            var result = "Forecast Retrieval failed with the following errors:";
            angular.forEach(err.data.errorMessages, function (value) {
                result += "<li>" + value + "</li>";
            });
            $("#mdlBody").html(result);
            $("#mdlTitle").text("Forecast Retrieval Failure");
            $("#mdlMessages").modal("show");
            vm.forecastFetched = false;
        }
    }
})();