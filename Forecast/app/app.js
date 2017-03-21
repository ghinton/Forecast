(function () {
    'use strict';
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
            //console.log(vm.isoCountryList.filter(function (country) { return country.code == vm.selectedCountryCode }));
            var sc = $scope.countryOptions.country;
            if (sc.length === 2 && vm.isoCountryList.filter(function (country) { return country.code === sc; }).length > 0) {
                //if (vm.selectedCountryCode.length == 2 && vm.isoCountryList.filter(function (country) { return country.code == vm.selectedCountryCode }).length > 0) {
                return true;
            }
            return false;
        }

        function forecastLoaded(forecastObj) {
            vm.cityForecast = forecastObj.data;
            // City class plus Days dictionary keyed by date in dd-MMM-yyyy format; each day has a dictionary of 8 ForecastDay3HourSlice objects
            if (!$scope.$$phase) {
                $scope.$apply();
                //buildForecastTable();
            }
        }

        function onFailure(err) {
            var result = "Forecast Retrieval failed with the following errors:";
            angular.forEach(err.data.errorMessages, function (value) {
                result += "<li>" + value + "</li>";
            });
            $('#mdlBody').html(result);
            $('#mdlTitle').text("Forecast Retrieval Failure");
            $('#mdlMessages').modal('show');
        }

        /*function buildForecastTable() {
            var dayIterator = vm.cityForecast.days.values();
            var firstDay = dayIterator.next();

            var tableBuilder = "<table class=\"table table-bordered table-striped table-condensed table-responsive\"><tr class=\row\"><td class=\"col-md-2\">";
            for (var day in vm.cityForecast.days) {
                // Write the dates
                tableBuilder += "<td class=\"col-md-2\">" + day.date + "</td>";
            }
            tableBuilder += "</tr>";
            // Now build the remaining rows using an array to jump around
            var rowCol = [];
            for (var ts in firstDay.timeslices) {
                // Add the time period as the first column for all rows
                rowCol.push("<tr class=\"row\"><td class=\"col-md-2\">" + ts.period + "</td>");
            }

            // Now loop through all rows and process the remaining data for all the rest of the days
            for (var day in vm.cityForecast.days) {
                for(var ts in day.timeslices) {
                    tableBuilder += "<td><img class=\"img-responsive\" src=\"" + ts.icon + "\" alt=\"" + ts.description + ", cloud cover " + ts.cloudCoverPercentage + "\" /></td>";
                }
        }*/
    }
})();