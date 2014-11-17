'use strict';


angular.module('app', ['toaster', 'ngCookies', 'angular-loading-bar'])
    .constant('GITHUB_API', 'https://api.github.com/')
    .constant('GOOGLE_API', 'https://www.googleapis.com/')
    .service('tokens', [
        '$cookies', function ($cookies) {
            return {
                GitHub: function () {
                    return angular.fromJson($cookies.Github).Access_Token;
                },
                Google: function () {
                    return angular.fromJson($cookies.Google).Access_Token;
                }
            }
        }
    ])
    .directive('githubIssuesList', [
        '$http', 'GITHUB_API', 'toaster', 'tokens', function ($http, GITHUB_API, toaster, tokens) {
            return {
                templateUrl: 'Directives/github-issues-list.html',
                restrict: 'E',
                link: function (scope) {

                    $http.get(GITHUB_API + 'issues', {
                        headers: {
                            authorization: 'token ' + tokens.GitHub()
                        }
                    })
                    .success(function (data) {
                        scope.issues = data;
                    })
                    .error(function () {
                        toaster.pop('error', 'Não foi possível recolher a lista de issues.');
                    });

                }
            };
        }])
    .factory('lists', function () {
        return {
            list: {
                title: ''
            }
        };
    })
    .directive('googleTaskLists', ['lists', 'GOOGLE_API', '$http', 'toaster', 'tokens', function (lists, GOOGLE_API, $http, toaster, tokens) {
        return {
            templateUrl: 'Directives/google-task-lists.html',
            restrict: 'E',
            scope: {
                issue: '='
            },
            link: function (scope) {
                $http.get(GOOGLE_API + 'tasks/v1/users/@me/lists', {
                    headers: {
                        authorization: 'Bearer ' + tokens.Google()
                    }
                })
                 .success(function (data) {
                     scope.lists = data.items;
                 })
                 .error(function () {
                     toaster.pop('error', 'Não foi possível recolher dados da google.');
                 });

                scope.list = lists.list;

                scope.select = function (list) {

                    scope.list.active = false;

                    scope.list = lists.list = list;

                    scope.list.active = true;
                };
            }
        };
    }])
    .directive('copyToGoogle', ['lists', 'GOOGLE_API', '$http', 'toaster', 'tokens', function (lists, GOOGLE_API, $http, toaster, tokens) {
        return {
            templateUrl: 'Directives/copy-to-google.html',
            restrict: 'E',
            scope: {
                issue: '='
            },
            link: function (scope) {
                scope.copy = function() {
                    $http.post(GOOGLE_API + 'tasks/v1/lists/' + lists.list.id + '/tasks',
                        {
                            title: scope.issue.title,
                            notes: scope.issue.body,
                            status: 'needsAction'
                        },
                        {
                            headers: {
                                authorization: 'Bearer ' + tokens.Google()
                            }
                        })
                        .success(function(data) {
                            toaster.pop('success', 'Tarefa copiada para a lista da google.');
                        })
                        .error(function() {
                            toaster.pop('error', 'Não foi possível recolher dados da google.');
                        });
                };
            }
        };
    }])

;