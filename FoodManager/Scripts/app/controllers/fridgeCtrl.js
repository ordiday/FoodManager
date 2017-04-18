app.controller('FridgeCtrl', ['$http', '$scope', function ($http, $scope) {
    $scope.searchText = "";
    $scope.searchProduct = function (text) {
        $http.get("/api/foodProduct/?amount=12&search=" + text).then(function (response) {
            $scope.foodProducts = response.data;
        },
            function (response) { });
    };

    $scope.getFoodProducts = function () {
        $http.get('/api/foodproduct/?amount=12')
            .then(function(response){
                $scope.foodProducts = response.data;
            },
            function (response) {

            });
    };

    $scope.addProductToFridge = function (product) {
        $http.post("/api/fridgeitem/", { FoodProductId: product.Id }).then(function (response) {
            $scope.fridgeItems.push(response.data);
            $scope.searchText = "";
            $scope.searchProduct("");

        }, function (response) {
        });
    };

    $scope.removeFromFridge = function (fridgeItem) {
        $http.delete("/api/fridgeitem/" + fridgeItem.Id).then(function (response) {
            var itemIndex = $scope.fridgeItems.indexOf(fridgeItem);
            $scope.fridgeItems.splice(itemIndex, 1);
        }, function (response) {
        });
    };

    $scope.getFridgeItems = function () {
        $http.get("/api/fridgeitem").then(function (response) {
            $scope.fridgeItems = response.data;
        }, function (response) {
        });
    };

    $scope.expiresSoon = function (fridgeItem) {
        if (fridgeItem.StoragePeriods != null) {
            for (var i = 0; i < fridgeItem.StoragePeriods.length; i++) {
                var storPer = fridgeItem.StoragePeriods[i];
                var from = storPer.FromTime;
                var curDate = new Date();
                var addDate = new Date(fridgeItem.AddDate);
                var expDate = addDate.clone().addMilliseconds(from);
                if (curDate.compareTo(expDate) == 1) {
                    return true;
                }
            }
        }

        return false;
    };

    $scope.getExpiresSoonTooltip = function (fridgeItem) {
        var tooltipText = "<div>Продукты группы \"" + fridgeItem.StoragePeriods[0].DerivedFromClass + "\" храняться: <br>"+
            "<ul>";

        for (var i = 0; i < fridgeItem.StoragePeriods.length; i++) {
            var storagePer = fridgeItem.StoragePeriods[i];

            tooltipText += "<li>";

            var msFrom = new Date(storagePer.FromTime).getTime();
            var msTo = new Date(storagePer.ToTime).getTime();

            if (msTo != msFrom) {
                tooltipText += "от ";

                var spFromDays = Math.floor(msFrom / (1000 * 60 * 60 * 24));
                msFrom -= spFromDays * (1000 * 60 * 60 * 24);
                var spFromHours = Math.floor(msFrom / (1000 * 60 * 60));

                if (spFromDays > 0) {
                    tooltipText += spFromDays + " дней ";
                }
                if (spFromHours > 0) {
                    tooltipText += spFromHours + " часов ";
                }
            }

            tooltipText += "до ";

            var spToDays = Math.floor(msTo / (1000 * 60 * 60 * 24));
            msTo -= spToDays * (1000 * 60 * 60 * 24);
            var spToHours = Math.floor(msTo / (1000 * 60 * 60));

            if (spToDays > 0) {
                tooltipText += spToDays + " дней ";
            }
            if (spToHours > 0) {
                tooltipText += spToHours + " часов ";
            }

            tooltipText += "при температуре от " + storagePer.FromTemperature + "&deg;C до " + storagePer.ToTemperature + "&deg;C";

            tooltipText += "</li>";
        }


        tooltipText += "</ul></div>";

        return tooltipText;
    };

    $scope.hasAdvice = function (fridgeItem) {
        return fridgeItem.StorageAdvice != null;
    };

    $scope.hasStoragePeriodInfo = function (fridgeItem) {
        return (fridgeItem.StoragePeriods != null && fridgeItem.StoragePeriods.length > 0);
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
                "background-position":"center"
            };
        }
    };
}]);