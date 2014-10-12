module.exports = function (grunt) {

    // Project configuration.
    grunt.initConfig({
                         pkg: grunt.file.readJSON('package.json'),
                         uglify: {
                             options: {
                                 mangle: false,
                                 banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n',
                                 compress: {
                                     drop_console: false
                                 }
                             },
                             all: {
                                 files: {
                                     '<%= pkg.jsPath %>all.min.js': [
                                         '<%= pkg.jsPath %>slimmage.min.js',
                                         '<%= pkg.jsPath %>vendor/modernizr-2.6.2.min.js',

                                         '<%= pkg.jsPath %>vendor/jquery-1.10.2.js',
                                         '<%= pkg.jsPath %>bower_components/masonry/dist/masonry.pkgd.js',
                                         '<%= pkg.jsPath %>bower_components/imagesloaded/imagesloaded.pkgd.js',
                                         '<%= pkg.jsPath %>bower_components/angular/angular.js',
                                         '<%= pkg.jsPath %>bower_components/ocModal/dist/ocModal.js',
                                         '<%= pkg.jsPath %>bower_components/moment/moment.js',
                                         '<%= pkg.jsPath %>bower_components/pikaday/pikaday.js',
                                         '<%= pkg.jsPath %>bower_components/angular-macgyver/lib/macgyver-core.js',

                                         '<%= pkg.jsPath %>app/app.js',

                                         '<%= pkg.jsPath %>app/controllers/faqsCtrl.js',
                                         '<%= pkg.jsPath %>app/controllers/pageCtrl.js',
                                         '<%= pkg.jsPath %>app/controllers/partyCtrl.js',
                                         '<%= pkg.jsPath %>app/controllers/partyDetailsFormCtrl.js',
                                         '<%= pkg.jsPath %>app/controllers/profileCtrl.js',
                                         '<%= pkg.jsPath %>app/controllers/suggestedDonationFormCtrl.js',
                                         '<%= pkg.jsPath %>app/controllers/wallCtrl.js',

                                         '<%= pkg.jsPath %>app/directives/mAngularMasonry.js',
                                         '<%= pkg.jsPath %>app/directives/mAddressFinder.js',
                                         '<%= pkg.jsPath %>app/directives/mDateTimePicker.js',
                                         '<%= pkg.jsPath %>app/directives/mImageUpload.js',
                                         '<%= pkg.jsPath %>app/directives/mKeyEnterTrigger.js',
                                         '<%= pkg.jsPath %>app/directives/mFileSelected.js',
                                         '<%= pkg.jsPath %>app/directives/mHeightMatchWidth.js',
                                         '<%= pkg.jsPath %>app/directives/mImageLoadedBroadcaster.js',
                                         '<%= pkg.jsPath %>app/directives/mImageLoadedNotifier.js',
                                         '<%= pkg.jsPath %>app/directives/mPostOnSubmit.js',
                                         '<%= pkg.jsPath %>app/directives/mProfileImage.js',
                                         '<%= pkg.jsPath %>app/directives/mRadiusPicker.js',
                                         '<%= pkg.jsPath %>app/directives/mRotate.js',
                                         '<%= pkg.jsPath %>app/directives/mSelectOnClick.js',
                                         '<%= pkg.jsPath %>app/directives/mTotaliser.js',
                                         '<%= pkg.jsPath %>app/directives/mValidnumber.js',
                                         '<%= pkg.jsPath %>app/directives/mValueMatcher.js',
                                         '<%= pkg.jsPath %>app/directives/mVerticalAlign.js',
                                         '<%= pkg.jsPath %>app/directives/mWindowClickToggle.js',

                                         '<%= pkg.jsPath %>app/filters/all.js',

                                         '<%= pkg.jsPath %>app/services/debounce.js',
                                         '<%= pkg.jsPath %>app/services/fileUpload.js',
                                         '<%= pkg.jsPath %>app/services/mHttp.js',
                                         '<%= pkg.jsPath %>app/services/paf.js',
                                         '<%= pkg.jsPath %>app/services/safeApply.js',
                                         '<%= pkg.jsPath %>app/services/uniqueId.js'

                                     ]
                                 }
                             }
                         },
                         cssmin: {
                             add_banner_and_min: {
                                 options: {
                                     keepSpecialComments: 0,
                                     banner: '/* <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */'
                                 },
                                 files: {
                                     '<%= pkg.cssPath %>all.min.css': [
                                         '<%= pkg.cssPath %>main.css',
                                         '<%= pkg.jsPath %>bower_components/ocModal/dist/css/ocModal.full.css',
                                         '<%= pkg.jsPath %>bower_components/pikaday/css/pikaday.css',
                                         '<%= pkg.jsPath %>bower_components/angular-macgyver/lib/macgyver.css',
                                         '<%= pkg.cssPath %>overrides.css'
                                     ]
                                 }
                             }
                         }
                     });

    // Load the plugin that provides the "uglify" task.
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-cssmin');

    // Default task(s).
    grunt.registerTask('default', ['uglify']);

};