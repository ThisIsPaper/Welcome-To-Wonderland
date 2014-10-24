wonderlandApp.directive('mTemplateCreator', [function () {

    return {

        scope: {},
        link: function (scope, element, attrs) {
            scope.$emit('newTemplatePartial', attrs.mTemplateCreator, $(element).html());
        }

    };

}]);