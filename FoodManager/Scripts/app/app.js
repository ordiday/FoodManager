var app = angular.module('FoodManagerApp', ['ngRoute', 'ngAnimate', 'ngSanitize', 'ui.bootstrap']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/fridge', {
        templateUrl: 'App/Fridge',
        controller: 'FridgeCtrl'
    });

    $routeProvider.when('/shoppingLists', {
        templateUrl: 'App/ShoppingLists',
        controller: 'ShoppingListsCtrl'
    });

    $routeProvider.when('/recipes', {
        templateUrl: 'App/Recipes',
        controller: 'RecipesCtrl'
    });

    $routeProvider.otherwise({
        redirectTo: '/fridge'
    });;
}]);