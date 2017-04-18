app.controller('RecipesCtrl', ['$http', '$scope', '$uibModal', '$document', '$window', function ($http, $scope, $uibModal, $document, $window) {
    $scope.getRecipes = function () {
        $http.get("/api/recipe/").then(function (response) {
            $scope.recipes = response.data;
        },
            function (response) { });
    };

    $scope.openRecipe = function (recipe) {
        var parentElem = angular.element($document[0].querySelector('.container'));

        $scope.currentRecipe = recipe;

        if ($scope.currentRecipe.IsYummly) {
            $http.get("/api/recipe/?yummlyId=" + $scope.currentRecipe.YummlyId).then(function (response) {
                    $window.open(response.data.SourceUrl, '_blank');
                },
                function (response) { });
        }
        else {
            var modalInstance = $uibModal.open({
                controller: 'ModalInstanceCtrl',
                templateUrl: "recipeContentTemplate",
                appendTo: parentElem,
                controllerAs: '$ctrl',
                size: 'lg',
                resolve: {
                    currentRecipe: function () {
                        return $scope.currentRecipe;
                    }
                }
            });
        }
    };

    $scope.getStyleForRecipe = function (recipe) {
        if (recipe.ImgUrl != null) {
            return {
                "background-image": "linear-gradient( rgba(0, 0, 0, 0.3), rgba(0, 0, 0, 0.3) ), url(" + recipe.ImgUrl + ")",
                "background-size": "cover",
                "background-repeat": "no-repeat",
                "background-position": "center"
            };
        }
        else {
            return {
                "background-image": "linear-gradient( rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6),rgba(0, 0, 0, 0.4) ), url('Content/images/defaultRecipeImage.jpg')",
                "background-size": "cover",
                "background-repeat": "no-repeat",
                "background-position": "center"
            };
        }
    };
}]);