wonderlandApp.controller('SuggestedDonationFormCtrl', ['$scope', function ($scope) {

    $scope.hardcodedDonationSuggestions = [
        { amount: 10, label: 'Cheesy tunes' },
        { amount: 15, label: 'Deck the halls' },
        { amount: 20, label: 'Sauerkraut sizzle' },
        { amount: 25, label: 'Snowball bash' }
    ];

}]);