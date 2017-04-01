/// <reference path="..\angular.js" />
/// <reference path="..\Forecast3\Forecast\app\app.js" />
xdescribe("When initializing the forecastController", function () {
    //initialize Angular
    beforeEach(module("forecast"));

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