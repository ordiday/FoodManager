angular.module('FoodManagerApp').controller('ModalInstanceCtrl', function ($uibModalInstance, currentRecipe) {
    var $ctrl = this;
    $ctrl.currentRecipe = currentRecipe;
});