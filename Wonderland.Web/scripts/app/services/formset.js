wonderlandApp.factory('formset', function() {
    function formset(prefix, initialForms, maxNumForms, forms) {
        var formData = {};

        formData[prefix + '-TOTAL_FORMS'] = forms.length;
        formData[prefix + '-INITIAL_FORMS'] = initialForms;
        formData[prefix + '-MAX_NUM_FORMS'] = maxNumForms;

        angular.forEach(forms, function(form, index) {
            angular.forEach(form, function(val, key) {
                if (key.charAt(0) !== '$' && val !== null && val !== undefined && val !== false) {
                    if (val === true) {
                        val = 'on';
                    }
                    formData[prefix + '-' + index + '-' + key] = val;
                }
            });
        });

        return formData;
    }

    formset.fromJsonForm = function(jsonForm) {
        return formset(jsonForm.prefix, jsonForm.INITIAL_FORMS, jsonForm.MAX_NUM_FORMS, jsonForm.forms);
    };

    return formset;
});
