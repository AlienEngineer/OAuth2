

(function() {

    var app = angular.module('app', []);

    app.constant('GITHUB_API', 'https://api.github.com/');


    app.directive('githubIssuesList', ['$http', 'GITHUB_API', 'toaster', function ($http, GITHUB_API, toaster) {
        return {
            templateUrl: 'Directives/github-issues-list.html',
            restrict: 'E',
            link: function(scope) {

                $http.get(GITHUB_API + 'issues')
                    .success(function(data) {
                        scope.issues = data;
                    })
                    .error(function() {
                        toaster.pop('error', 'Não foi possível recolher a lista de issues.')
                    });

            }
        };
    }]);


})();
