'use strict';


angular.module('app', ['toaster', 'ngCookies', 'angular-loading-bar'])
  .constant('GITHUB_API', 'https://api.github.com/')
  .service('github_token', ['$cookies', function($cookies) {
    return {
      get: function() {
        return angular.fromJson($cookies.Github).Access_Token;
      }
    }
  }])
  .directive('githubIssuesList', ['$http', 'GITHUB_API', 'toaster', 'github_token', function ($http, GITHUB_API, toaster, github_token) {
        return {
            templateUrl: 'Directives/github-issues-list.html',
            restrict: 'E',
            link: function(scope) {

                $http.get(GITHUB_API + 'issues', {
                      headers: {
                        authorization: 'token ' + github_token.get()
                      }
                    })
                    .success(function(data) {
                        scope.issues = data;
                    })
                    .error(function() {
                        toaster.pop('error', 'Não foi possível recolher a lista de issues.')
                    });

            }
        };
    }])
