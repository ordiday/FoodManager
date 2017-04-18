app.controller('ShoppingListsCtrl', ['$http', '$scope', function ($http, $scope) {
    $scope.productIsInCurrentShoppingList = function (foodProduct) {
        for (var i = 0; i < $scope.currentShoppingList.Products.length; i++) {
            if ($scope.currentShoppingList.Products[i].Id == foodProduct.Id) {
                return true;
            }
        }

        return false;
    };

    $scope.searchProduct = function (text) {
        $http.get("/api/foodProduct/?amount=12&search=" + text).then(function (response) {
            $scope.foodProducts = response.data;
        },
            function (response) { });
    };

    $scope.getFoodProducts = function () {
        $http.get('/api/foodproduct/?amount=12')
            .then(function (response) {
                $scope.foodProducts = response.data;
            },
            function (response) {

            });
    };

    $scope.getCurrentShoppingList = function () {
        $http.get('/api/currentshoppingList/')
            .then(function (response) {
                $scope.currentShoppingList = response.data;
            },
            function (response) {

            });
    };

    $scope.addProductToShoppingList = function (foodProduct) {
        $http.post("/api/currentshoppinglist/", foodProduct).then(function (response) {
            $scope.currentShoppingList.Products.push(foodProduct);
        }, function (response) {
        });
    }

    $scope.removeFromCurrentShoppingList = function (product) {
        $http.delete("/api/currentshoppinglist/" + product.Id).then(function (response) {
            var itemIndex = $scope.currentShoppingList.Products.indexOf(product);
            $scope.currentShoppingList.Products.splice(itemIndex, 1);
        }, function (response) {
        });
    };

    $scope.getStyleForFoodProduct = function (foodProduct) {
        if (foodProduct.Img != null) {
            return {
                "background-image": "linear-gradient( rgba(0, 0, 0, 0.3), rgba(0, 0, 0, 0.3) ), url(" + foodProduct.Img + ")",
                "background-size": "cover",
                "background-repeat": "no-repeat",
                "background-position": "center"
            };
        }
        else {
            return {
                "background-image": "linear-gradient( rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6),rgba(0, 0, 0, 0.4) ), url('Content/images/defaultProductImage.png')",
                "background-size": "contain",
                "background-repeat": "no-repeat",
                "background-position": "center"
            };
        }
    };
}]);