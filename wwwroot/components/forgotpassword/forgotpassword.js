
angular.module('myApp.forgotpassword', ['ngRoute', 'services', 'toastr', 'directives'])
    .controller('forgotpasswordCtrl', ['$scope', '$location', 'AuthenticationService', 'toastr',
        function ($scope, $location, AuthenticationService, toastr) {

            $scope.forgotPassword = function (ev) {
                $scope.dataLoading = true;
                AuthenticationService.ForgotPass($scope.email, function (response) {

                    if (response.data.responseCode === 200) {
                        toastr.success('Your new Password was sent', 'Check email!');
                        $location.path('/');
                    }
                    else {
                        toastr.error(response.data.message, "Title", {
                            "timeOut": "3000",
                            "extendedTImeout": "0"
                        });
                    }
                });
                $scope.dataLoading = false;
            }

            $scope.resendActivationCode = function (ev) {
                $scope.dataLoading = true;
                AuthenticationService.ResendActivationCode($scope.email, function (response) {
                    toastr.success('Your new Activation Code was sent', 'Check email!');
                    $location.path('/');
                });
                $scope.dataLoading = false;
            }
        }]);

