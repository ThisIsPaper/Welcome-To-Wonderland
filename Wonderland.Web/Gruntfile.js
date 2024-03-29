module.exports = function (grunt) {

    // Project configuration.
    grunt.initConfig({
                         pkg: grunt.file.readJSON('package.json'),
                         compass: {
                             dist: {
                                 options: {
                                     config: '.compass/config.rb'
                                 }
                             }
                         },
                         jshint: {
                             all: ['Gruntfile.js', '<%= pkg.jsPath %>app/**/*.js']
                         },
                         uglify: {
                             options: {
                                 mangle: false,
                                 beautify: false,
                                 banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n',
                                 compress: {
                                     drop_console: false
                                 }
                             },
                             base: {
                                 files: {
                                     '<%= pkg.jsPath %>all.min.js': [
                                         '<%= pkg.jsPath %>slimmage.min.js',
                                         '<%= pkg.jsPath %>vendor/modernizr-2.6.2.min.js',

                                         '<%= pkg.jsPath %>vendor/jquery-1.10.2.js',
//                                         '<%= pkg.jsPath %>bower_components/angular/angular.js',
                                         '<%= pkg.jsPath %>vendor/angular.ie7.js',
                                         '<%= pkg.jsPath %>bower_components/imagesloaded/imagesloaded.pkgd.js',
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

                                         '<%= pkg.jsPath %>app/directives/mAddressFinder.js',
                                         '<%= pkg.jsPath %>app/directives/mDateTimePicker.js',
                                         '<%= pkg.jsPath %>app/directives/mImageUpload.js',
                                         '<%= pkg.jsPath %>app/directives/mKeyEnterTrigger.js',
                                         '<%= pkg.jsPath %>app/directives/mFacebookLoginButton.js',
                                         '<%= pkg.jsPath %>app/directives/mFileSelected.js',
                                         '<%= pkg.jsPath %>app/directives/mFormTokenDispatcher.js',
                                         '<%= pkg.jsPath %>app/directives/mHeightMatchWidth.js',
                                         '<%= pkg.jsPath %>app/directives/mImageLoadedBroadcaster.js',
                                         '<%= pkg.jsPath %>app/directives/mImageLoadedNotifier.js',
                                         '<%= pkg.jsPath %>app/directives/mImgSvgReplacer.js',
                                         '<%= pkg.jsPath %>app/directives/mIosVersion.js',
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
                                         '<%= pkg.jsPath %>app/services/facebook.js',
                                         '<%= pkg.jsPath %>app/services/fileUpload.js',
                                         '<%= pkg.jsPath %>app/services/mHttp.js',
                                         '<%= pkg.jsPath %>app/services/paf.js',
                                         '<%= pkg.jsPath %>app/services/safeApply.js',
                                         '<%= pkg.jsPath %>app/services/uniqueId.js'

                                     ]
                                 }
                             },
                             base_above_ie7: {
                                 files: {
                                     '<%= pkg.jsPath %>base_above_ie7.min.js': [
                                         '<%= pkg.jsPath %>bower_components/masonry/dist/masonry.pkgd.js',
                                         '<%= pkg.jsPath %>app/directives/mAngularMasonry.js'
                                     ]
                                 }
                             },
                             ie8: {
                                 files: {
                                     '<%= pkg.jsPath %>ie8.min.js': [
                                         '<%= pkg.jsPath %>bower_components/es5-shim/es5-shim.min.js',
                                         '<%= pkg.jsPath %>bower_components/es5-shim/es5-sham.min.js'
//                                         '<%= pkg.jsPath %>bower_components/respond/dest/respond.min.js'
                                     ]
                                 }
                             },
                             ie7: {
                                 files: {
                                     '<%= pkg.jsPath %>ie7.min.js': [
//                                         '<%= pkg.jsPath %>bower_components/JSON-js/json2.js', // not sure of the difference between these two? i'll go 3 coz it's probabbly newer
                                         '<%= pkg.jsPath %>bower_components/json3/lib/json3.min.js',
                                         '<%= pkg.jsPath %>bower_components/8960575/querySelector.polyfill.js'
                                     ]
                                 }
                             },
                             ie7_after_base: {
                                 files: {
                                     '<%= pkg.jsPath %>ie7.after_base.min.js': [
                                         '<%= pkg.jsPath %>app/polyfills/masonry.js'
                                     ]
                                 }
                             }
                         },
                         pixrem: {
                             options: {
                                 rootvalue: "62.5%",
                                 replace: true
                             },
                             replaced_rem: {
                                 files: {
                                     '<%= pkg.cssPath %>ie/compiled/main_rem_fallback.css': ['<%= pkg.cssPath %>compiled/main.css'],
                                     '<%= pkg.cssPath %>ie/compiled/overrides_rem_fallback.css': ['<%= pkg.cssPath %>compiled/overrides.css'],
                                     '<%= pkg.cssPath %>ie/compiled/bower_rem_fallback.css': [
                                         '<%= pkg.jsPath %>bower_components/ocModal/dist/css/ocModal.full.css',
                                         '<%= pkg.jsPath %>bower_components/pikaday/css/pikaday.css',
                                         '<%= pkg.jsPath %>bower_components/angular-macgyver/lib/macgyver.css'
                                     ]
                                 }
                             }
                         },
                         cssmin: {
                             main: {
                                 options: {
                                     keepSpecialComments: 0,
                                     banner: '/* <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */'
                                 },
                                 files: {
                                     '<%= pkg.cssPath %>all.min.css': [
                                         '<%= pkg.cssPath %>compiled/main.css',
                                         '<%= pkg.jsPath %>bower_components/ocModal/dist/css/ocModal.full.css',
                                         '<%= pkg.jsPath %>bower_components/pikaday/css/pikaday.css',
                                         '<%= pkg.jsPath %>bower_components/angular-macgyver/lib/macgyver.css',
                                         '<%= pkg.cssPath %>compiled/overrides.css'
                                     ]
                                 }
                             },
                             ie: {
                                 options: {
                                     compatibility: 'ie7',
                                     keepSpecialComments: 0,
                                     banner: '/* <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */'
                                 },
                                 files: {
                                     '<%= pkg.cssPath %>all_ie.min.css': [
                                         '<%= pkg.cssPath %>ie/compiled/main_rem_fallback.css',
                                         '<%= pkg.cssPath %>ie/compiled/bower_rem_fallback.css',
                                         '<%= pkg.cssPath %>ie/compiled/overrides_rem_fallback.css',
                                         '<%= pkg.cssPath %>ie/ie8.css'
                                     ]
                                 }
                             },
                             ie7: {
                                 options: {
                                     compatibility: 'ie7',
                                     keepSpecialComments: 0,
                                     banner: '/* <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */'
                                 },
                                 files: {
                                     '<%= pkg.cssPath %>ie7.min.css': [
                                         '<%= pkg.cssPath %>ie/ie7.css'
                                     ]
                                 }
                             }
                         }
                     }
    )
    ;

// Load the plugin that provides the "uglify" task.
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-pixrem');
    grunt.loadNpmTasks('grunt-contrib-compass');
    grunt.loadNpmTasks('grunt-contrib-jshint');

// Default task(s).
    grunt.registerTask('js', ['jshint', 'uglify']);
    grunt.registerTask('css', ['compass', 'pixrem', 'cssmin']);
    grunt.registerTask('default', ['js', 'css']);

}
;
