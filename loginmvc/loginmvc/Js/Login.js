var app = angular.module("myApp", []);
app.controller("myCtrl", function($scope, $http) {
    $scope.Login = function () {
        debugger;
        var User = {};
        User = {
            Email: $scope.Email,
            Password: $scope.Password
        };
        $.post('/Login/Authorise', User).then(function (res) {
            debugger;
            //window.location.pathname = "/Home"
        });
    }
    $scope.reg = function () {
        window.location.pathname = "/Register"
    }
})

app.controller("regCtrl", function ($scope, $http) {
    //debugger;
    $scope.Register = function () {
        var User = {};
        User = {
            Email: $scope.Email,
            Password: $scope.Password,
            UserName: $scope.UserName,
            Mobile: $scope.Mobile
        }
        $.post('/Register/create', User).then(function (res) {
            //debugger;
            window.location.pathname = "/Home"
        })
    }
    $scope.loginButton = function () {
        window.location.pathname = "/Login"

    }
})

app.controller("fgPCtrlr", function ($scope, $http) {
    //debugger;
    $scope.genOtp = function () {
        var User = {}
        User = {
            Email: $scope.Email,
        }
        $.post('/ForgetPassword/generateOTP', User).then(function (res) {
            //debugger;
        })
    }
})
app.controller("changePasswordController",function($scope,$http){
    $scope.changePassword = function () {
        var User = {}
        User = {
            otp: $scope.otp,
            Password: $scope.Password,
            confirmPassword:$scope.confirmPassword
        }
        $.post("/ChangePassword/changePasswordMethod", User).then(function (res) {

        })
    }
})